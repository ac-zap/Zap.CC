# Inventaire des Assets Graphiques (Zap -> Zenith)

> **Répertoire source** : `D:\[GRAPHIQUE]\[Zenith]`  
> **Dossier de sauvegarde des originaux** : `D:\[GRAPHIQUE]\[Zenith]\_ORIGINAL_ZAP_BACKUP\`  
> **Date de génération** : 15/11/2026

---

## 1. Fichiers Originaux Sauvegardés (Zap)

| Fichier original | Dimensions | Usage | Contenu Zap | Emplacement de sauvegarde |
|---|---|---|---|---|
| `zap_client_logo.png` | 680x960 | Ancien logo officiel Zap | Oui (ancien logo Zap) | `_ORIGINAL_ZAP_BACKUP\zap_client_logo.png` |
| `zap-logo.gif` | 400x400 | Animation logo Discord / Web | Oui (animation Zap) | `_ORIGINAL_ZAP_BACKUP\zap-logo.gif` |
| `preview.png` | 800x450 | Bannière d'aperçu | Oui (branding Zap) | `_ORIGINAL_ZAP_BACKUP\preview.png` |
| `preview.webp` | 800x450 | Version webp de prévisualisation | Oui (branding Zap) | `_ORIGINAL_ZAP_BACKUP\preview.webp` |
| `bannerinfo` | 1920x400 | Bannière d'information | Oui (branding Zap) | `_ORIGINAL_ZAP_BACKUP\bannerinfo` |

---

## 2. Nouveaux Assets Zenith Régénérés

| Fichier Zenith | Format / Résolution | Rôle / Emplacement d'intégration | Palette |
|---|---|---|---|
| `zenith_logo_transparent.png` | PNG (680x960) | Logo master haute résolution sur fond transparent | Bleu royal `#1E40AF` & Bleu ciel `#38BDF8` |
| `zenith_logo_16.png` | PNG (16x16) | Icône système / favicon basse résolution | Bleu royal & Bleu ciel |
| `zenith_logo_32.png` | PNG (32x32) | Icône tray barre des tâches & fenêtres standard | Bleu royal & Bleu ciel |
| `zenith_logo_48.png` | PNG (48x48) | Icône d'en-tête de l'installeur Windows Forms | Bleu royal & Bleu ciel |
| `zenith_logo_64.png` | PNG (64x64) | Icône de bureau & splash intermédiaire | Bleu royal & Bleu ciel |
| `zenith_logo_128.png` | PNG (128x128) | Icône d'application & StoreLogo | Bleu royal & Bleu ciel |
| `zenith_logo_256.png` | PNG (256x256) | Icône d'application HiDPI (128@2x) | Bleu royal & Bleu ciel |
| `zenith_logo_512.png` | PNG (512x512) | Grande icône haute définition (Linux / macOS) | Bleu royal & Bleu ciel |
| `zenith_logo_1024.png` | PNG (1024x1024) | Master icon Ultra-HD pour packaging | Bleu royal & Bleu ciel |
| `icon.ico` | ICO multi-résolution (16→256) | Exécutable Windows, installeur & raccourcis | Format standard ICO 32bpp |
| `banner_zenith.png` | PNG (1200x300) | Bannière officielle (*Performance • Precision • Power*) | Dégradé bleu doux & bleu ciel |
| `zenith_logo.svg` | SVG vectoriel | Logo vectoriel scalabilité infinie | SVG standard dégradé `#38BDF8` -> `#1E40AF` |
| `zenith_horizontal.svg` | SVG vectoriel (1200x300) | Logo + Typographie + Devise | SVG avec texte vectoriel |
| `zenith_monochrome.svg` | SVG vectoriel | Variante blanche pour contrastes sombres | Blanc pur `#FFFFFF` |
| `zenith_favicon.svg` | SVG vectoriel (32x32) | Favicon web & navigateur | Fond `#101B3D` & crête Zenith |
| `mark_64.png`, `128.png`, `256.png` | PNG (64, 128, 256) | Splash screen in-game (`polyplus`) | Blason centré |
| `word_54.png`, `108.png`, `216.png` | PNG | Titre d'écran de chargement in-game | Typographie « ZENITH CLIENT » |

---

## 3. Déploiement dans le Projet

* **Lanceur (`packages/zenithclient_app/icons/`)** :
  * `icon.ico`, `icon.png`, `32x32.png`, `64x64.png`, `128x128.png`, `128x128@2x.png`, `512x512.png`, `tray.png`, `StoreLogo.png`.
  * `ZenithClient.icon/Assets/zenithclient-mark.svg`.
* **Mods Minecraft (`polyplus-2.0.9+26.3.jar`)** :
  * `assets/polyplus/brand/oneclient.svg`, `mainmenu/logo.svg`, `mainmenu/footer-logo.svg`, `onboarding/zap.svg`.
  * `assets/polyplus/startup/mark_*.png`, `assets/polyplus/startup/word_*.png`.
