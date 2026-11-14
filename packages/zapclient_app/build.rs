fn main() {
    #[cfg(windows)]
    {
        let mut res = winresource::WindowsResource::new();
        res.set_icon("icons/icon.ico");

        let version = env!("CARGO_PKG_VERSION");
        res.set("CompanyName", "Zap.cc");
        res.set("ProductName", "ZapClient");
        res.set("FileDescription", "ZapClient");
        res.set("LegalCopyright", "© 2026 Zap.cc");
        res.set("OriginalFilename", "zapclient_app.exe");
        res.set("InternalName", "zapclient_app");
        res.set("FileVersion", version);
        res.set("ProductVersion", version);

        res.compile()
            .expect("failed to embed Windows icon resource");
    }
}
