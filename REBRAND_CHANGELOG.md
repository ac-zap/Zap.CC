# REBRAND CHANGELOG : Zap Client -> Zenith Client

## [Unreleased] - rebrand-zenith

### Phase 6 : Client en-jeu (In-Game) & Expérience Utilisateur (Complété)
- **Splash Screen de Chargement (StartupOverlay)** :
  - Wordmark et logo mark mis à jour avec le ratio natif exact 5.34:1 (`word_54` 289x54, `word_108` 577x108, `word_216` 1154x216).
  - Élimination complète du logo fleur et du texte "ONECLIENT" au lancement.
  - Typographie Poppins Bold sur fond transparent avec intégration de la palette Zenith.
- **Écran de Pause In-Game** :
  - Création des alias de classes `ZenithClientPauseScreen`, `ZenithClientPauseScreen$Companion` et `ZenithClientServerApi`.
  - Intégration transparente pour éviter tout `ClassNotFoundException` tout en assurant l'identité Zenith.
- **Écran d'Onboarding In-Game** :
  - Remplacement de "Welcome to ZapClient / ONECLIENT" par "Welcome to ZenithClient".
  - Textes d'acceptation des conditions et paramètres calibrés sur Zenith.
- **Traductions In-Game (`assets/polyplus/lang/en_us.json`)** :
  - Clés `switchToZAPCLIENT` remplacées par `ZenithClient Menu`.
  - Notification Debugify mise à jour pour mentionner `ZenithClient`.
- **Marque Mojang / F3** :
  - Argument JVM `-Dminecraft.launcher.brand=ZenithClient`.

### Phase 5 : Refonte complète de l'UI du Launcher (Complété)
- **Thèmes Freya & Design System** :
  - **Zenith Dark** (`packages/zenithclient_app/assets/themes/dark.json`) :
    - Fond principal (page) : `#0A1128` (Bleu nuit)
    - Surfaces & panneaux : `#101B3D` (Bleu profond)
    - Cartes surélevées : `#14214B`
    - Composants interactifs : `#1A2B5E`
    - Bordures / Glow : `#38BDF826` (Bleu ciel semi-transparent)
    - Texte principal : `#F1F5F9` (Blanc cassé)
    - Texte secondaire : `#BAE6FD` (Bleu glacé)
    - Accent interactif : `#38BDF8` (Bleu ciel)
  - **Zenith Light** (`light.json`) : Palette lumineuse cobalt & blanc pur.
  - **Zenith Minecraft Dark & Light** : Variantes pixel-art avec la même cohérence chromatique.
- **Logos Vectoriels & En-tête** :
  - Remplacement de l'ancien logo ZAP par le nouveau logo vectoriel Zenith (`logo.svg`) avec typographie Poppins vectorisée en courbes de Bézier (`<path>`).
  - Nouvelle icône compacte `icon-logo.svg` (marque à deux ailes / apex stylisé).
  - Logo dynamique dans `app_navbar.rs`, `onboarding_navbar.rs` et `startup.rs`.
- **Paramètres & Fenêtre** :
  - Titre de fenêtre : `Zenith Client`.
  - Variable d'environnement GPU : `ZENITHCLIENT_GPU_CACHE_BYTES`.

### Phase 4 & 4 bis : Identité Visuelle & Assets Graphiques (Complété)
- **Dossier Centralisé `D:\[GRAPHIQUE]\[Zenith]`** :
  - Sauvegarde préalable des anciens assets Zap dans `_ORIGINAL_ZAP_BACKUP\`.
  - Génération de toute la suite d'icônes carrées PNG : 16x16, 32x32, 48x48, 64x64, 128x128, 256x256, 512x512, 1024x1024.
  - Fichier multi-résolution Windows `icon.ico` (16 à 256 px).
  - Logos vectoriels SVG : `zenith_logo.svg`, `zenith_horizontal.svg`, `zenith_monochrome.svg`, `zenith_favicon.svg`.
  - Fichiers d'inventaire et prévisualisation : `ASSETS_INVENTORY.md` et `ASSETS_PREVIEW.html`.
- **Déploiement Launcher** :
  - Remplacement complet des icônes dans `packages/zenithclient_app/icons/`.

### Phase 3 : Mods Client & Patch Bytecode (Complété)
- **Patch Bytecode via ASM (`BytecodePatcher.java`)** :
  - Utilisation de la bibliothèque ASM 9.10.1 et de JDK 25 pour analyser et réécrire directement les constantes LDC dans les classes compilées (`PolyPlusOnboardingScreenKt`, `PolyPlusMainMenuConfig`, etc.).
  - Substitution automatique de tous les identifiants textuels `ZAPCLIENT`, `ZapClient`, `ONECLIENT`, `OneClient` en `ZenithClient` / `ZENITHCLIENT`.
- **Mods Rebrandés** :
  - `polyplus-2.0.9+26.3.jar` -> Nom d'affichage `ZenithPlus`, auteur `Zenith`.
  - `OneConfig-26.3-fabric-1.7.1.jar` -> Nom d'affichage `ZenithConfig`, auteur `Zenith`.
  - Nettoyage du cache Fabric `processedMods`.

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
  - `ZenithInstaller.cs` compilé avec succès en `ZenithInstaller.exe` via C# / .NET.
  - Palette appliquée à l'installeur : Bleu nuit `#0A1128`, Bleu profond `#101B3D`, Bleu ciel `#38BDF8`.
  - Inscription au Registre Windows : `Software\Microsoft\Windows\CurrentVersion\Uninstall\ZenithClient` et protocole `zenithclient://`.
- **Packaging & Distribution** :
  - Flatpak : `cc.zenith.client.yml` et `cc.zenith.client.metainfo.xml`.
  - Arch Linux AUR : `distribution/aur/zenithclient` et `distribution/aur/zenithclient-bin`.
  - Fedora COPR : `distribution/copr/zenithclient-bin.spec`.
  - CI/CD GitHub Actions : `.github/workflows/zenithclient_nightly.yml` et `zenithclient_release.yml`.
- **Fichiers de langue & I18n** :
  - Clés d'internationalisation préfixées en `zenithclient.`.
  - Remplacement de "Zap Client" par "Zenith Client" dans `en_us.json`, `fr_fr.json` et `es_es.json`.
- **Règle GitHub respectée** :
  - Dépôt officiel préservé : `https://github.com/ac-zap/Zap.CC`.

### Phase 1 : Audit & Exploration (Complété)
- Création de la branche git `rebrand-zenith`.
- Audit exhaustif de toutes les occurrences dans `AUDIT_ZAP.md`.
- Découverte et confirmation des assets graphiques dans `D:\[GRAPHIQUE]\[Zenith]`.
