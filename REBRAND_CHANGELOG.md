# REBRAND CHANGELOG : Zap Client -> Zenith Client

## [Unreleased] - rebrand-zenith

### Phase 1 : Audit & Exploration (En cours)
- Création de la branche git `rebrand-zenith`.
- Exploration approfondie de la stack :
  - **Launcher** : Rust (Edition 2024, Rust 1.97+), UI Freya (framework GUI déclaratif basé sur Dioxus / Skia / Winit / AccessKit / Tokio / Sqlx).
  - **Architecture Workspace** : 15 crates dans `packages/` (`zapclient_app`, `zapclient_auth`, `zapclient_cluster`, `zapclient_common`, `zapclient_content`, `zapclient_core`, `zapclient_db`, `zapclient_discord`, `zapclient_events`, `zapclient_java`, `zapclient_macro`, `zapclient_mc`, `zapclient_net`, `zapclient_polyplus`, `polyio`).
  - **Installeur Windows** : C# Windows Forms standalone (`ZapInstaller.cs`).
  - **Distribution** : Flatpak (`distribution/flatpak`), Arch AUR (`distribution/aur`), Fedora COPR (`distribution/copr`), bundle Windows CRT (`distribution/windows`).
  - **Client & Mods Minecraft** : Fabric 26.3, mod principal `polyplus-2.0.9+26.3.jar` (UI compose, onboarding, splash screen, pause menu, API client), `OneConfig-26.3-fabric-1.7.1.jar`.
  - **Assets graphiques** : Découverte du dossier `D:\[GRAPHIQUE]\[Zenith]` contenant déjà `zenith_logo_transparent.png`, `banner_zenith.png` et `zap_client_logo.png`.
  - **Règle GitHub** : Préservation absolue de l'URL et du dépôt GitHub `https://github.com/ac-zap/Zap.CC` selon instruction explicite de l'utilisateur.
- Rédaction du rapport d'audit exhaustif dans `AUDIT_ZAP.md`.
