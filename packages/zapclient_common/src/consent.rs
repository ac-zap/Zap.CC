use std::sync::atomic::{AtomicBool, Ordering};

/// Hosts that are out of bounds for someone who didn't accept TOS
const GATED_DOMAINS: &[&str] = &["zap.cc", "sentry.io"];

/// Hosts that are allowed even if someone didn't accept TOS
const ALLOWED_HOSTS: &[&str] = &[
    "zapcc.dieulucas144.workers.dev",
    "data-v2.polyfrost.org",
    "meta.polyfrost.org",
    "status.polyfrost.org",
    "data-v2.zap.cc",
    "meta.zap.cc",
    "status.zap.cc",
];

static DECLINED: AtomicBool = AtomicBool::new(false);

pub fn init(declined: bool) {
    DECLINED.store(declined, Ordering::SeqCst);
}

pub fn decline() {
    DECLINED.store(true, Ordering::SeqCst);
}

#[must_use]
pub fn declined() -> bool {
    DECLINED.load(Ordering::Relaxed)
}

#[must_use]
pub fn blocks_url(url: &str) -> bool {
    declined() && is_gated_url(url)
}

#[must_use]
pub fn is_gated_url(url: &str) -> bool {
    url::Url::parse(url)
        .ok()
        .and_then(|parsed| parsed.host_str().map(is_gated_host))
        .unwrap_or(true)
}

#[must_use]
pub fn is_gated_host(host: &str) -> bool {
    let host = host.trim_end_matches('.').to_ascii_lowercase();

    if ALLOWED_HOSTS.contains(&host.as_str()) {
        return false;
    }

    GATED_DOMAINS
        .iter()
        .any(|domain| host == *domain || host.ends_with(&format!(".{domain}")))
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn gates_ZAP_services_and_the_crash_reporter() {
        for host in [
            "zap.cc",
            "plus.zap.cc",
            "PLUS.ZAP.CC",
            "plus.zap.cc.",
            "o4511714343124992.ingest.us.sentry.io",
        ] {
            assert!(is_gated_host(host), "expected {host} to be gated");
        }
    }

    #[test]
    fn leaves_everything_else_alone() {
        for host in [
            "api.modrinth.com",
            "api.curseforge.com",
            "sessionserver.mojang.com",
            "api.minecraftservices.com",
            "www.gstatic.com",
            "github.com",
            "notzap.cc",
            "zap.cc.attacker.com",
            "evil-sentry.io.attacker.com",
        ] {
            assert!(!is_gated_host(host), "unexpected gate on {host}");
        }
    }

    #[test]
    fn lets_the_exempt_hosts_through() {
        for url in [
            "https://data-v2.zap.cc/zapclient/bundles/metadata.json",
            "https://data-v2.zap.cc/zapclient/versions/art/Tricky_Trials.jpg",
            "https://data-v2.zap.cc/zapclient/tos.json",
            "https://meta.zap.cc/fabric/v2/manifest.json",
            "https://status.zap.cc/index.json",
            "https://META.ZAP.CC./minecraft/v1/manifest.json",
        ] {
            assert!(!is_gated_url(url), "unexpected gate on {url}");
        }
    }

    #[test]
    fn the_exemption_is_the_exact_host_and_nothing_near_it() {
        for host in [
            "plus.zap.cc",
            "evil.data-v2.zap.cc",
            "meta.zap.cc.evil.zap.cc",
            "status.zap.cc.evil.zap.cc",
        ] {
            assert!(is_gated_host(host), "expected {host} to stay gated");
        }
    }

    #[test]
    fn gates_urls_by_their_host() {
        assert!(is_gated_url("https://plus.zap.cc/account/login"));
        assert!(is_gated_url("wss://plus.zap.cc/websocket"));
        assert!(!is_gated_url("https://api.modrinth.com/v2/search"));
    }

    #[test]
    fn unparseable_urls_fail_closed() {
        assert!(is_gated_url("not a url"));
        assert!(is_gated_url(""));
        assert!(is_gated_url("file:///etc/hosts"));
    }
}
