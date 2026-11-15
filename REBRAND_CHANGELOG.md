# REBRAND CHANGELOG : Zap Client -> Zenith Client

## [Unreleased] - visual-refresh-zenith

### Phase 7 : Isolation Complète des Profils (Système type `.fastclient`)
- **Architecture de Répertoires Dédiés par Profil** :
  - Adoption intégrale du paradigme d'isolation inspiré de `%APPDATA%\.fastclient`.
  - Chaque profil/instance (`clusters/<Nom>` ou via le point de jonction `profiles/<Nom>`) possède désormais son propre dossier de jeu isolé.
  - Séparation stricte par profil :
    - `resourcepacks/` dédié (les packs de texture activés dans une version n'interfèrent pas avec les autres).
    - `shaderpacks/` dédié (shaders indépendants par profil).
    - `mods/` dédié.
    - `config/` dédié (configurations de mods spécifiques à la version).
    - `saves/` et `worlds/` dédiés.
    - `options.txt` et `servers.dat` indépendants par profil.
  - Les assets globaux immuables (sons vanilla, textures Mojang, bibliothèques Java) restent mutualisés dans `metadata/` pour éviter de télécharger inutilement des gigaoctets.
- **Marqueur & Intégration Native Rust** :
  - Déploiement automatique du marqueur `.dedicated_directory` sur l'ensemble des profils existants (`26.3 Fabric`, `1.21.1 Fabric`, `1.21.10 Fabric`, `1.21.11 Fabric`, `1.8.9 Fabric`, `26.1.2 Fabric`, `26.2 Fabric`).
  - `DirTarget::default()` modifié vers `DirTarget::Dedicated` dans `zenithclient_cluster/src/cluster.rs`.
  - Modal de création d'instance (`create.rs` et `steps.rs`) calibré par défaut sur `FolderChoice::Dedicated` avec mention "(Recommandé)".
  - Arguments de lancement Minecraft : `--gameDir` pointe directement sur le répertoire du profil sélectionné.

### Phase Visual Refresh : Design Tokens & Motion Engine Zenith v2
- **Tokens Centralisés (`tokens.rs`)** :
  - Palette officielle Zenith v2 : Bleu nuit `#060B1F` / `#0A1128`, Bleu profond `#101B3D`, Accent Bleu ciel `#38BDF8`, Ice `#BAE6FD`.
  - Système d'élévation, rayons d'arrondis (`RADIUS_*`), lueurs (glow) et constantes d'ombres.
  - Tokens de mouvement : courbes d'animation `EASE_OUT_CUBIC`, `EASE_IN_OUT_SPRING` et paliers de durée (120ms à 500ms).
- **Rapports d'Audit Visuel** :
  - Cartographie exhaustive dans `VISUAL_AUDIT.md`.
  - Comparatif visuel avant/après documenté dans `VISUAL_BEFORE_AFTER.md`.

## [2.10.1] - rebrand-zenith

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
- **Catalogue d'Icônes macOS (`ZenithClient.icon`)** :
  - `Assets/zenithclient-mark.svg` régénéré avec le blason vectoriel Zenith et les dégradés officiel Bleu ciel `#38BDF8` et Bleu royal `#1E40AF`.
  - `icon.json` mis à jour pour référencer `zenithclient-mark.svg` et le calque `zenithclient-mark`.
- **Suite Complète d'Icônes Launcher (`packages/zenithclient_app/icons/`)** :
  - Déploiement direct de toutes les résolutions : `16x16`, `32x32`, `64x64`, `128x128`, `128x128@2x` (256), `512x512`, `icon.png`, `StoreLogo.png`, `tray.png`, `icon.ico`.
  - Génération de l'archive binaire Apple ICNS `icon.icns` (298 KB) encapsulant les blocs `ic07`, `ic08`, `ic09`, `ic10`.
  - Icône pixel vectorielle : `packages/zenithclient_app/assets/icons/pixel/icon-logo.svg` mise à jour avec l'apex Zenith.
- **Packaging & Médias Distribution** :
  - Arrière-plans DMG macOS : `dmg-background.png`, `dmg-background@2x.png` et `dmg-background.tiff` créés aux couleurs Bleu nuit `#0A1128` avec logo Zenith et flèche d'installation vers Applications.
  - Bannière GitHub officielle : `.github/media/RepoBanner.png` (3000x1000) générée avec typographie Poppins, lueur d'apex et palette officielle.
  - Animation logo : `.github/media/zenith-logo.gif` déployée et référencée dans `README.md`.
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
