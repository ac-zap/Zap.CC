//! Environment variable helpers with legacy fallback.

use std::ffi::OsString;

pub fn var(suffix: &str) -> Result<String, std::env::VarError> {
    let new_key = format!("ZAPCLIENT_{suffix}");
    if let Ok(val) = std::env::var(&new_key) {
        return Ok(val);
    }
    let old_key = format!("zenithclient_{suffix}");
    if let Ok(val) = std::env::var(&old_key) {
        tracing::warn!(
            "Environment variable {} is deprecated. Please use {} instead.",
            old_key,
            new_key
        );
        return Ok(val);
    }
    let old_key2 = format!("ZapClient_{suffix}");
    if let Ok(val) = std::env::var(&old_key2) {
        tracing::warn!(
            "Environment variable {} is deprecated. Please use {} instead.",
            old_key2,
            new_key
        );
        return Ok(val);
    }
    std::env::var(new_key)
}

pub fn var_os(suffix: &str) -> Option<OsString> {
    let new_key = format!("ZAPCLIENT_{suffix}");
    if let Some(val) = std::env::var_os(&new_key) {
        return Some(val);
    }
    let old_key = format!("zenithclient_{suffix}");
    if let Some(val) = std::env::var_os(&old_key) {
        tracing::warn!(
            "Environment variable {} is deprecated. Please use {} instead.",
            old_key,
            new_key
        );
        return Some(val);
    }
    let old_key2 = format!("ZapClient_{suffix}");
    if let Some(val) = std::env::var_os(&old_key2) {
        tracing::warn!(
            "Environment variable {} is deprecated. Please use {} instead.",
            old_key2,
            new_key
        );
        return Some(val);
    }
    None
}
