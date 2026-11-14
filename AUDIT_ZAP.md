# Audit Exhaustif des Occurrences "Zap" dans le Projet

> **Projet** : Zap Client -> Zenith Client  
> **Branche de travail** : `rebrand-zenith`  
> **Date de l'audit** : 15/11/2026  
> **Règle stricte** : Le dépôt GitHub (`https://github.com/ac-zap/Zap.CC`) reste inchangé conformément à la consigne de l'utilisateur.

---

## 1. Stack Technique et Architecture du Projet

* **Langage & Runtime** : Rust (Edition 2024, resolver v3)
* **Framework GUI** : Freya (Skia + Dioxus core + Winit + AccessKit)
* **Système de build** : Cargo Workspace
* **Nombre de crates internes** : 15 packages dans `packages/`
  * `polyio` (I/O, cache et décompression)
  * `zapclient_app` (Application GUI Freya, vues, composants, thèmes, i18n, routage)
  * `zapclient_auth` (Gestion des comptes Microsoft OAuth2, device code, offline/crack)
  * `zapclient_cluster` (Gestion des instances Minecraft, clusters, configs)
  * `zapclient_common` (Constantes, URLs, chemins de données, OS, architecture)
  * `zapclient_content` (Téléchargement et gestion des mods, versions, Modrinth, CurseForge)
  * `zapclient_core` (Moteur principal, vérification des fichiers, synchronisation, migration)
  * `zapclient_db` (Base de données SQLite locale `user_data.db`)
  * `zapclient_discord` (Intégration Discord Rich Presence)
  * `zapclient_events` (Bus d'événements interne asynchrone)
  * `zapclient_java` (Détection, gestion et téléchargement des runtimes Java)
  * `zapclient_macro` (Macros procédurales pour l'UI et les événements)
  * `zapclient_mc` (Lancement du process JVM Minecraft, arguments, natives)
  * `zapclient_net` (Client HTTP reqwest, sondes de connectivité, proxy)
  * `zapclient_polyplus` (Intégration et liaison avec les services PolyPlus)
* **Installeur Windows** : `ZapInstaller.cs` (C# .NET WinForms autonome)
* **Mods Minecraft** :
  * Modpack Fabric 26.3 dans `%APPDATA%\Zap\ZapClient\.minecraft\mods\26.3 Fabric\`
  * Mod client sur-mesure : `polyplus-2.0.9+26.3.jar` (Onboarding, menu de pause, splash screen de démarrage, RPC)
  * Mod de configuration : `OneConfig-26.3-fabric-1.7.1.jar`

---

## 2. Inventaire Détaillé des Occurrences par Catégorie

### A. Packages & Structure du Workspace Cargo (15 crates & configs de build)
* **Crates à renommer** :
  * `packages/zapclient_app` -> `packages/zenithclient_app`
  * `packages/zapclient_auth` -> `packages/zenithclient_auth`
  * `packages/zapclient_cluster` -> `packages/zenithclient_cluster`
  * `packages/zapclient_common` -> `packages/zenithclient_common`
  * `packages/zapclient_content` -> `packages/zenithclient_content`
  * `packages/zapclient_core` -> `packages/zenithclient_core`
  * `packages/zapclient_db` -> `packages/zenithclient_db`
  * `packages/zapclient_discord` -> `packages/zenithclient_discord`
  * `packages/zapclient_events` -> `packages/zenithclient_events`
  * `packages/zapclient_java` -> `packages/zenithclient_java`
  * `packages/zapclient_macro` -> `packages/zenithclient_macro`
  * `packages/zapclient_mc` -> `packages/zenithclient_mc`
  * `packages/zapclient_net` -> `packages/zenithclient_net`
  * `packages/zapclient_polyplus` -> `packages/zenithclient_polyplus`
* **Exécutables binaires dans `packages/zapclient_app/Cargo.toml`** :
  * `zapclient_app` -> `zenithclient_app`
  * `ZapClient_app` -> `ZenithClient_app`
* **Fichiers de manifestes** :
  * `Cargo.toml` (dépendances workspace, authors, package metadata)
  * `knope.toml` (gestion des releases et changelogs)
  * `.signpath/zapclient.artifact-configuration.xml`
  * `.signpath/zapclient-binary.artifact-configuration.xml`

### B. Installeur Windows & Scripts Packaging
* **Code C#** : `ZapInstaller.cs` -> `ZenithInstaller.cs`
  * Namespace : `ZapClient.Installer` -> `ZenithClient.Installer`
  * Titre de fenêtre : `"Zap Client - Assistant d'installation"` -> `"Zenith Client - Assistant d'installation"`
  * Dossier d'installation par défaut : `%LOCALAPPDATA%\Programs\ZapClient` -> `%LOCALAPPDATA%\Programs\ZenithClient`
  * Raccourcis bureau et menu démarrer : `Zap Client.lnk` -> `Zenith Client.lnk`
  * Clé de registre de désinstallation : `SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\ZapClient` -> `...Uninstall\ZenithClient`
* **Packaging Linux** :
  * `distribution/flatpak/cc.zap.client.yml` -> `cc.zenith.client.yml`
  * `distribution/flatpak/cc.zap.client.metainfo.xml` -> `cc.zenith.client.metainfo.xml`
  * `distribution/aur/zapclient` -> `distribution/aur/zenithclient`
  * `distribution/aur/zapclient-bin` -> `distribution/aur/zenithclient-bin`
  * `distribution/copr/zapclient-bin.spec` -> `zenithclient-bin.spec`

### C. Répertoires de Données, Registres et Migration Automatique
* **Répertoires actuels** :
  * Windows : `%APPDATA%\Zap\ZapClient`
  * Organisation : `Zap`
  * Application : `ZapClient` (et `ZapClient-dev` en mode debug)
* **Cible Phase 2** :
  * Nouveau répertoire : `%APPDATA%\Zenith\ZenithClient`
  * Organisation : `Zenith`
  * Application : `ZenithClient`
* **Stratégie de migration automatique** :
  * Dans `paths.rs`, la fonction `migrate_legacy_data_dir()` détectera automatiquement `%APPDATA%\Zap\ZapClient` (ainsi que les anciens répertoires `OneClient` s'ils existent) et copiera en toute sécurité :
    * `auth.json` (comptes Microsoft et offline)
    * `settings.json` (préférences, mémoire RAM allouée, résolutions, Java)
    * `user_data.db` (base SQLite des instances et métadonnées)
    * Dossiers `.minecraft` et `clusters` (mondes, serveurs, configurations de mods)

### D. Code Source Rust (427 fichiers, ~9500 occurrences textuelles)
* **Constantes & Domaines (`packages/zapclient_common/src/constants.rs`)** :
  * `DISCORD_URL` : `"https://discord.gg/zapclient"` -> `"https://discord.gg/zenithclient"`
  * `TOS_URL` & `PRIVACY_URL` : restent sur le dépôt GitHub `ac-zap/Zap.CC`
* **Traductions et clés d'I18n** :
  * Fichiers : `packages/zapclient_app/lang/en_us.json`, `fr_fr.json`, `es_es.json`
  * Clés : préfixes `zapclient.` / `ZapClient.` -> `zenithclient.` / `ZenithClient.`
  * Textes utilisateur : "Zap Client", "Zap", "ZapConfig", "ZapPlus" -> "Zenith Client", "Zenith", "ZenithConfig", "ZenithPlus"
* **Variables d'environnement & Protocole** :
  * Variable Sentry : `ZAPCLIENT_SENTRY_DSN` -> `ZENITHCLIENT_SENTRY_DSN`
  * Schéma d'URL personnalisé : `zapclient://` -> `zenithclient://`

### E. Mods en Jeu & JVM Minecraft (`polyplus` & `OneConfig`)
* **Mod `polyplus-2.0.9+26.3.jar`** :
  * `fabric.mod.json` :
    * `id` : `polyplus` (gardé pour compatibilité Fabric loader)
    * `name` : `"ZapPlus"` -> `"ZenithPlus"`
    * `authors` : `["Zap"]` -> `["Zenith"]`
  * Classes / bytecode :
    * `ZapClientPauseScreen.class` -> `ZenithClientPauseScreen.class`
    * `ZapClientServerApi.class` -> `ZenithClientServerApi.class`
    * Littéraux bytecode : `"ZapClient"` -> `"ZenithClient"`, `"ZAPCLIENT"` -> `"ZENITHCLIENT"`
  * Textures et Logos intégrés :
    * `assets/polyplus/brand/oneclient.svg` -> Nouveau SVG Zenith
    * `assets/polyplus/mainmenu/logo.svg` -> Nouveau SVG Zenith
    * `assets/polyplus/startup/mark_*.png` -> Nouveau blason Zenith
    * `assets/polyplus/startup/word_*.png` -> Nouveau wordmark Zenith
* **Mod `OneConfig-26.3-fabric-1.7.1.jar`** :
  * `fabric.mod.json` : `name: "ZapConfig"` -> `"ZenithConfig"`
  * Textes de branding : `Zap` -> `Zenith`

### F. Assets Graphiques & Visuels Détectés dans `D:\[GRAPHIQUE]\[Zenith]`
* **Fichiers source déjà disponibles** :
  * `D:\[GRAPHIQUE]\[Zenith]\zenith_logo_transparent.png` (Dimensions 2000x2800, fond transparent, logo en forme de crête / éclat ascendant bleu royal et bleu ciel)
  * `D:\[GRAPHIQUE]\[Zenith]\banner_zenith.png` (Bannière officielle avec typographie `Zenith Client` et slogan `PERFORMANCE • PRECISION • POWER`)
  * `D:\[GRAPHIQUE]\[Zenith]\zap_client_logo.png` (Ancien logo Zap)
* **Sauvegarde de sécurité** :
  * Création d'un dossier `D:\[GRAPHIQUE]\[Zenith]\_ORIGINAL_ZAP_BACKUP\` avant toute régénération d'assets.

---

## 3. Plan d'Exécution Validé

1. **Phase 1 (Actuelle)** : Audit exhaustif, exploration de l'environnement, validation avec l'utilisateur.
2. **Phase 2** : Renommage technique du workspace Rust, migration automatique du dossier `%APPDATA%`, mise à jour de l'installeur C# et des manifests de distribution.
3. **Phase 3** : Rebranding des mods (`polyplus` et `OneConfig`), renommage des classes et injection des nouveaux assets.
4. **Phase 4 & 4 bis** : Exportation des assets graphiques dans toutes les tailles (PNG, ICO, ICNS, SVG) à partir du logo Zenith de `D:\[GRAPHIQUE]\[Zenith]`.
5. **Phase 5** : Refonte de la palette et de l'interface Freya aux couleurs Zenith (Bleu nuit `#0A1128`, Bleu profond `#101B3D`, Bleu ciel `#38BDF8`).
6. **Phase 6** : Vérification et branding in-game.
7. **Phase 7** : Optimisations et nettoyage.
8. **Phase 8** : Packaging et installeur.
9. **Phase 9** : Vérification finale et changelog complet.
