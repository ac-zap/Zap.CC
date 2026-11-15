# Zenith Client ⚡

<p align="center">
  <img src=".github/media/RepoBanner.png" alt="Zenith Client Banner" width="100%">
</p>

<p align="center">
  <img src=".github/media/zenith-logo.gif" alt="Zenith Animation" width="300">
</p>

<p align="center">
  <strong>Un launcher Minecraft moderne, ultra-rapide et open source écrit en Rust.</strong>
</p>

<p align="center">
  <a href="https://github.com/ac-zap/Zap.CC/releases"><img src="https://img.shields.io/github/v/release/ac-zap/Zap.CC?style=flat-square&color=blue" alt="Dernière release"></a>
  <a href="LICENSE"><img src="https://img.shields.io/badge/licence-GPL--3.0-green?style=flat-square" alt="Licence GPL-3.0"></a>
  <a href="https://github.com/ac-zap/Zap.CC/issues"><img src="https://img.shields.io/github/issues/ac-zap/Zap.CC?style=flat-square" alt="Issues"></a>
</p>

---

## 🚀 Fonctionnalités principales

- 🏎️ **Performance & Légèreté** : Développé 100 % en **Rust** avec l'interface graphique native **Freya** (Skia / Dioxus). Faible consommation mémoire et démarrage quasi instantané.
- 📦 **Navigateur de Mods & Shaders intégré** :
  - Recherche et installation en 1 clic depuis **Modrinth** et **CurseForge**.
  - Gestion automatique des dépendances et mises à jour des mods.
- 🧩 **Tous les Modloaders supportés** :
  - **Fabric**, **Forge**, **NeoForge**, **Quilt** et **Minecraft Vanilla**.
  - Détection automatique et isolation complète des versions de Java.
- 🔒 **Respect de la vie privée** :
  - Aucun tracking invasif.
  - Vos identifiants Microsoft ne transitent par aucun serveur tiers et sont chiffrés localement sur votre machine.
- 📂 **Gestion avancée des instances** :
  - Instances isolées ou dossiers partagés pour économiser l'espace disque.
  - Exportation et importation rapides de profils.
  - Outil de diagnostic et de détection automatique des crashs avec explications claires.

---

## 📥 Installation

Téléchargez la dernière version compilée pour votre système d'exploitation dans l'onglet [Releases](https://github.com/ac-zap/Zap.CC/releases) :

- **Windows** : Téléchargez l'installeur `.exe` ou l'archive portable.
- **Linux** : Disponible au format `.AppImage`, `.deb`, ou via les paquets AUR / Copr.
- **macOS** : Téléchargez le fichier `.dmg`.

---

## 🛠️ Compilation depuis les sources

### Prérequis
- [Rust](https://rustup.rs/) (version 1.80 ou plus récente recommandée)
- Outils de compilation C/C++ de votre système (ex: Visual Studio C++ Build Tools sur Windows, `build-essential` sur Ubuntu/Debian)

### Instructions

1. **Cloner le dépôt :**
   ```bash
   git clone https://github.com/ac-zap/Zap.CC.git
   cd Zap.CC
   ```

2. **Compiler le client en mode release :**
   ```bash
   cargo build --release --bin zenithclient_app
   ```

L'exécutable compilé sera disponible dans `target/release/zenithclient_app` (ou `zenithclient_app.exe` sous Windows).

---

## 📜 Politiques & Légalité

- [Conditions d'utilisation (TERMS.md)](TERMS.md)
- [Politique de confidentialité (PRIVACY.md)](PRIVACY.md)
- [Guide de contribution (CONTRIBUTING.md)](CONTRIBUTING.md)

*Zenith Client est un projet indépendant open source et n'est en aucun cas affilié à Mojang Studios, Microsoft, ou Minecraft.*

---

## ⚖️ Licence

Ce projet est distribué sous la licence libre **GNU General Public License v3.0** (GPL-3.0). Consultez le fichier [LICENSE](LICENSE) pour plus de détails.

