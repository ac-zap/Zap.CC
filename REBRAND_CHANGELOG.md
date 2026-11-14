# REBRAND CHANGELOG : Zap Client -> Zenith Client

## [Unreleased] - rebrand-zenith

### Phase 2 : Renommage technique & Migration automatique (Complété)
- **Crates et Workspace** :
  - Renommage des 14 crates de `packages/zapclient_*` vers `packages/zenithclient_*`.
  - Mise à jour du `Cargo.toml` racine et de tous les `Cargo.toml` des packages.
  - Renommage des binaires d'application : `zenithclient_app` et `ZenithClient_app`.
  - Mise à jour de tous les imports (`use zenithclient_*::`) et identifiants dans 417 fichiers Rust.
- **Migration automatique des profils et données utilisateur** (`paths.rs`) :
  - L'application pointe désormais vers `%APPDATA%\Zenith\ZenithClient` (organisation: `Zenith`, application: `ZenithClient`).
  - Implémentation de la migration automatique depuis `%APPDATA%\Zap\ZapClient` (et anciens dossiers `.zapclient`, `OneClient`) vers `%APPDATA%\Zenith\ZenithClient`.
  - Vos comptes (`auth.json`), instances, sauvegardes, mondes, serveurs et configurations (`settings.json`, `user_data.db`) sont conservés sans aucune perte.
- **Installeur Windows** :
  - `ZapInstaller.cs` renommé en `ZenithInstaller.cs`.
  - Nouveau titre : `Zenith Client - Assistant d'installation`.
  - Palette appliquée à l'installeur : Bleu nuit `#0A1128`, Bleu profond `#101B3D`, Bleu ciel `#38BDF8`.
  - Raccourcis Bureau et Menu Démarrer : `Zenith Client.lnk`.
  - Inscription au Registre Windows : `Software\Microsoft\Windows\CurrentVersion\Uninstall\ZenithClient` et protocole `zenithclient://`. Nettoyage des anciennes clés ZapClient.
- **Packaging & Distribution** :
  - Flatpak : `cc.zenith.client.yml` et `cc.zenith.client.metainfo.xml`.
  - Arch Linux AUR : `distribution/aur/zenithclient` et `distribution/aur/zenithclient-bin`.
  - Fedora COPR : `distribution/copr/zenithclient-bin.spec`.
  - SignPath : `.signpath/zenithclient.artifact-configuration.xml` et `.signpath/zenithclient-binary.artifact-configuration.xml`.
  - CI/CD GitHub Actions : `.github/workflows/zenithclient_nightly.yml` et `zenithclient_release.yml`.
- **Fichiers de langue & I18n** :
  - Clés d'internationalisation préfixées en `zenithclient.`.
  - Remplacement de "Zap Client" par "Zenith Client" dans `en_us.json`, `fr_fr.json` et `es_es.json`.
  - Fallback insensible à la casse dans `i18n.rs`.
- **Marque Minecraft** :
  - Argument de lancement : `-Dminecraft.launcher.brand=ZenithClient`.
- **Règle GitHub respectée** :
  - Dépôt officiel préservé : `https://github.com/ac-zap/Zap.CC`.

### Phase 1 : Audit & Exploration (Complété)
- Création de la branche git `rebrand-zenith`.
- Audit exhaustif de toutes les occurrences dans `AUDIT_ZAP.md`.
- Découverte et confirmation des assets graphiques dans `D:\[GRAPHIQUE]\[Zenith]`.
