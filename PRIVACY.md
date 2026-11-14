# Politique de Confidentialité — Zenith Client

*Dernière mise à jour : 13 novembre 2026*

Le respect de votre vie privée et la sécurité de vos données personnelles sont au cœur de la conception de **Zenith Client**. En tant que logiciel libre et open source, notre engagement est clair : vous gardez le contrôle total sur votre installation et vos données.

---

## 1. Données de compte et Authentification

- **Authentification Microsoft / Xbox Live** :
  - La connexion à votre compte Minecraft s'effectue via le protocole OAuth sécurisé officiel de Microsoft.
  - **Zenith Client ne reçoit, ne stocke et ne transmet JAMAIS vos identifiants ou mots de passe sur des serveurs tiers.**
  - Vos jetons de session (tokens) sont chiffrés et conservés exclusivement en local sur votre ordinateur dans votre dossier de configuration utilisateur.
- **Mode hors-ligne** :
  - Si vous utilisez un profil local hors-ligne, aucune communication réseau d'authentification n'a lieu.

---

## 2. Téléchargements et Services tiers

Lorsque vous utilisez Zenith Client pour installer des éléments de jeu, le logiciel communique directement avec les services officiels suivants :
- **Mojang Studios / Microsoft** : Pour télécharger les fichiers de jeu Minecraft (binaires, assets, sons).
- **Modrinth & CurseForge** : Pour rechercher et télécharger les mods, shaders, modpacks et packs de ressources que vous choisissez d'installer.
- **mclo.gs (Optionnel)** : Uniquement si vous cliquez expressément sur le bouton de partage de vos logs de crash. Vos logs sont anonymisés avant envoi (les adresses IP locales et noms d'utilisateurs sont masqués).

Chacun de ces services est régi par sa propre politique de confidentialité.

---

## 3. Données de télémétrie et Rapports d'erreurs

- **Aucune télémétrie cachée** : Zenith Client ne collecte aucune donnée analytique, publicitaire ou comportementale.
- **Rapports de plantage** : En cas de crash inopiné, aucune donnée n'est transmise automatiquement sans votre consentement. Vous avez toujours le choix d'examiner et d'envoyer ou non un rapport de diagnostic.

---

## 4. Stockage local de vos données

Toutes vos données (instances de jeu, mondes, captures d'écran, configurations de touches et réglages du launcher) sont stockées localement sur votre disque dur dans :
- **Windows** : `%APPDATA%\Zap\ZenithClient`
- **Linux** : `~/.config/Zap/ZenithClient` (ou dans le bac à sable Flatpak le cas échéant)
- **macOS** : `~/Library/Application Support/Zap/ZenithClient`

Vous pouvez à tout moment supprimer ou sauvegarder ce dossier pour effacer l'intégralité de vos données.

---

## 5. Contact & Questions

Pour toute question ou demande relative à la sécurité ou à cette politique de confidentialité, vous pouvez ouvrir une discussion ou une issue sur notre dépôt officiel :
👉 **[https://github.com/ac-zap/Zap.CC/issues](https://github.com/ac-zap/Zap.CC/issues)**

