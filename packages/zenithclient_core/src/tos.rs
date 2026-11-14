use serde::{Deserialize, Serialize};

use crate::{LauncherError, LauncherResult};
use zenithclient_common::constants;
use zenithclient_common::paths;
use zenithclient_net::RequestClient;
use zenithclient_net::{EtagPolicy, fetch_cached};

#[derive(Debug, Clone, PartialEq, Eq, Serialize, Deserialize)]
pub struct TermsDocument {
    pub version: u32,
    #[serde(default)]
    pub privacy_version: Option<u32>,
    #[serde(default)]
    pub updated_at: Option<String>,
    pub terms: String,
    #[serde(default)]
    pub privacy: Option<String>,
    #[serde(default)]
    pub terms_url: Option<String>,
    #[serde(default)]
    pub privacy_url: Option<String>,
}

impl TermsDocument {
    pub fn terms_url(&self) -> &str {
        self.terms_url
            .as_deref()
            .filter(|url| !url.trim().is_empty())
            .unwrap_or(constants::TOS_URL)
    }

    pub fn privacy_url(&self) -> &str {
        self.privacy_url
            .as_deref()
            .filter(|url| !url.trim().is_empty())
            .unwrap_or(constants::PRIVACY_URL)
    }

    pub fn privacy_version(&self) -> u32 {
        self.privacy_version.unwrap_or(self.version)
    }

    pub fn privacy_body(&self) -> Option<&str> {
        self.privacy
            .as_deref()
            .filter(|body| !body.trim().is_empty())
    }
}

#[tracing::instrument(level = "debug", skip(net))]
pub async fn fetch_terms(net: &RequestClient) -> LauncherResult<TermsDocument> {
    let url = format!("{}/oneclient/tos.json", net.config().meta_url_base);
    let cache_path = paths::caches_dir()?.join("TERMS.json");

    match fetch_cached(net, &url, &cache_path, EtagPolicy::CommitNow).await {
        Ok(Some(fetched)) => {
            if let Ok(doc) = fetched.json::<TermsDocument>() {
                return Ok(doc);
            }
        }
        _ => {}
    }

    Ok(TermsDocument {
        version: 1,
        privacy_version: Some(1),
        updated_at: Some("2026-11-14".to_string()),
        terms: "Bienvenue sur Zap Client.\n\nEn utilisant Zap Client, vous acceptez de vous conformer aux Conditions d'Utilisation de Minecraft et de Mojang Studios, ainsi qu'aux règles des serveurs sur lesquels vous jouez.\n\nZap Client est fourni tel quel pour améliorer votre expérience de jeu.".to_string(),
        privacy: Some("Zap Client respecte la confidentialité de vos données.\n\nAucune donnée personnelle n'est enregistrée sans votre accord explicite. Vos identifiants de compte restent chiffrés localement sur votre ordinateur.".to_string()),
        terms_url: Some(constants::TOS_URL.to_string()),
        privacy_url: Some(constants::PRIVACY_URL.to_string()),
    })
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn parses_minimal_document() {
        let raw = br###"{"version":2,"terms":"## Terms\n\nBe nice."}"###;
        let document: TermsDocument = serde_json::from_slice(raw).unwrap();

        assert_eq!(document.version, 2);
        assert_eq!(document.terms, "## Terms\n\nBe nice.");
        assert_eq!(document.terms_url(), constants::TOS_URL);
        assert_eq!(document.privacy_url(), constants::PRIVACY_URL);
    }

    #[test]
    fn privacy_version_falls_back_to_terms_version() {
        let raw = br#"{"version":4,"terms":"x"}"#;
        let document: TermsDocument = serde_json::from_slice(raw).unwrap();

        assert_eq!(document.privacy_version(), 4);
        assert_eq!(document.privacy_body(), None);
    }

    #[test]
    fn privacy_version_is_independent_when_published() {
        let raw = br###"{"version":4,"privacy_version":2,"terms":"x","privacy":"## Privacy"}"###;
        let document: TermsDocument = serde_json::from_slice(raw).unwrap();

        assert_eq!(document.version, 4);
        assert_eq!(document.privacy_version(), 2);
        assert_eq!(document.privacy_body(), Some("## Privacy"));
    }

    #[test]
    fn blank_privacy_body_is_treated_as_absent() {
        let raw = br#"{"version":1,"terms":"x","privacy":"   "}"#;
        let document: TermsDocument = serde_json::from_slice(raw).unwrap();

        assert_eq!(document.privacy_body(), None);
    }

    #[test]
    fn document_urls_override_constants() {
        let raw = br#"{"version":1,"terms":"x","terms_url":"https://example.com/tos","privacy_url":"  "}"#;
        let document: TermsDocument = serde_json::from_slice(raw).unwrap();

        assert_eq!(document.terms_url(), "https://example.com/tos");
        assert_eq!(document.privacy_url(), constants::PRIVACY_URL);
    }
}
