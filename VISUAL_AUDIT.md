# VISUAL AUDIT — Refonte Visuelle Totale : Zenith Client (Rust) & Mods

> **Règle d'or respectée** : L'architecture technique, les modules, crates, workspace, APIs, fichiers et flux de données sont strictement préservés. Seuls le rendu graphique, le ressenti visuel, les animations et les styles sont métamorphosés vers l'identité **Zenith v2**.

---

## 1. Identification du Framework UI

* **Framework UI Principal** : **Freya 0.5.0** (`freya`, `freya-core`, `freya-skia-safe`, `plusrenderer`).
* **Moteur Graphique & Rendu** : **Skia Safe** (rendu GPU matériel direct via Vulkan / DirectX / Metal via winit).
* **Gestion d'État & Composants** : Système réactif dérivé de Dioxus (`use_state`, `use_memo`, `use_animation`, `radio`).
* **Moteur de Disposition** : **Torin** (layout flexbox haute performance, alignements, paddings, gaps, offsets).
* **Rendu Vectoriel SVG** : `SvgViewer` avec rastérisation Skia vectorielle et support des `currentColor`.
* **Animations** : `use_animation`, `AnimNum`, courbes d'interpolation cubiques/sinusoïdales (`Ease::InOut`, `Ease::Out`, `Function::Cubic`, `Function::Sine`).

---

## 2. Cartographie Complète des Écrans du Launcher

| Écran / Vue | Fichier Source | État Actuel | Transformation Visuelle Zenith v2 |
|---|---|---|---|
| **Startup / Splash** | `packages/zenithclient_app/src/view/startup.rs` | Barre de progression simple, logo 2D statique, fond noir uni | Révélation lumineuse du logo Zenith, lueur pulsante Bleu Ciel `#38BDF8`, anneau de chargement dynamique, fond Bleu Nuit `#0A1128` avec dégradé radial subtil |
| **Accueil (Home)** | `packages/zenithclient_app/src/view/app/home/` & `active_cluster_panel.rs` | Art de fond standard, bouton Play simple, texte blanc | Background immersif avec vignettage glassmorphism, grand titre version avec typographie Poppins Display, **Bouton JOUER signature** avec glow respirant, gradient interactif et micro-interactions au survol |
| **Navigation / Navbar** | `packages/zenithclient_app/src/components/navbar/app_navbar.rs` | Barre sombre plate, icônes simples | Effet verre fumé (glassmorphism Skia avec flou), icônes vectorielles Zenith épurées avec indicateur d'onglet actif rétroéclairé |
| **Onboarding (Bienvenue, Compte, Emplacement, Préférences, Termes)** | `packages/zenithclient_app/src/view/onboarding/` & `onboarding_shell.rs` | Formulaires standards, cartes monochromes sombres | Stepper moderne avec indicateurs de progression lumineux, cartes en élévation Bleu Profond `#101B3D`, focus glow bleu ciel sur les inputs, micro-transitions fluides |
| **Gestionnaire d'Instances / Versions (Clusters)** | `packages/zenithclient_app/src/view/app/clusters/` | Grille de cartes classiques, badges de loader gris | Cartes d'instances haute finition avec bordures lumineuses `#38BDF826`, tags de version stylisés (Fabric, Forge, NeoForge, Vanilla), hover d'élévation avec ombre douce |
| **Navigateur de Mods & Modpacks (Browser)** | `packages/zenithclient_app/src/view/app/browser/` | Liste Modrinth/CurseForge standard, squelettes simples | Cartes de mods avec badges de popularité/téléchargement dégradés, squelettes de chargement (shimmer effect animé), filtres et barre de recherche redesignés |
| **Paramètres (Settings)** | `packages/zenithclient_app/src/view/app/settings/` (Accounts, Appearance, Java, Minecraft, Launcher) | Listes plates de paramètres, toggles classiques | Sélecteurs d'onglets ergonomiques, curseurs de mémoire RAM avec jauge stylisée, toggles fluides avec animation de glissement élastique, prévisualisation des thèmes |
| **Console & Logs** | `packages/zenithclient_app/src/view/console.rs` & `log_viewer.rs` | Texte brut sur fond noir | Coloration syntaxique premium (INFO en cyan `#38BDF8`, WARN en ambre doux, ERROR en corail/danger, DEBUG en vert menthe), barre d'outils avec copie en 1 clic |
| **Wardrobe / Cosmétiques** | `packages/zenithclient_app/src/view/app/wardrobe/` | Prévisualisation 3D basique, listes sombres | Scène 3D avec piédestal illuminé en bleu cobalt, carrousel d'éléments avec badge de rareté et sélection active surlignée |
| **Chat & Social** | `packages/zenithclient_app/src/view/chat/` | Fenêtres de messagerie standard | Bulles de discussion soignées, indicateurs de présence en ligne vert émeraude / bleu ciel, avatars avec contour anti-crénelé |
| **Modales & Popups** | `packages/zenithclient_app/src/components/*_popup.rs` | Modales grises centrées | Backdrop blur assombri, fenêtres aux coins arrondis et bordures lumineuses, boutons d'action clairement hiérarchisés (primaire éclatant / secondaire discret) |

---

## 3. Cartographie des Composants & Widgets UI

| Composant | Fichier Source | État Actuel | Refonte Zenith v2 |
|---|---|---|---|
| **Boutons (`Button`)** | `packages/zenithclient_app/src/components/button.rs` | Rectangles plats avec changement d'alpha | 4 variantes révisées (Primary dégradé royal/ciel, Secondary verre fumé, Danger satiné, Ghost néon), élévation au survol, compression tactile au clic |
| **Champs de Saisie (`TextInput`)** | `packages/zenithclient_app/src/components/text_input.rs` | Fond sombre avec contour discret | Fond `#1A2B5E` adouci, contour actif bleu ciel `#38BDF8` avec lueur d'accentuation, placeholder glacé |
| **Curseurs & RAM (`MemoryField`)** | `packages/zenithclient_app/src/components/memory_field.rs` | Sliders rectangulaires simples | Jauge de progression continue avec repères visuels (Recommandé, Min, Max), curseur circulaire avec halo lumineux |
| **Interrupteurs (`Toggle`)** | `packages/zenithclient_app/src/components/toggle.rs` | Toggle basique on/off | Animation fluide du switch, pastille blanche sur rail bleu royal `#1E40AF` / bleu ciel `#38BDF8` |
| **Barres de Progression (`progress_track.rs`)** | `packages/zenithclient_app/src/components/progress_track.rs` | Barre bleue unie | Dégradé animé en mouvement continu (shimmer), affichage précis du pourcentage et transition sans à-coups |
| **Sélecteurs & Dropdowns (`dropdown.rs`)** | `packages/zenithclient_app/src/components/dropdown.rs` | Menu déroulant rectangulaire | Menu en suspension avec ombre portée douce, animation de déploiement en fondu vertical, items surlignés |
| **Notifications & Toasts (`toasts.rs`)** | `packages/zenithclient_app/src/components/toasts.rs` | Cartouche rectangulaire standard | Toasts glassmorphism avec barre de durée résiduelle, icône contextuelle lumineuse et entrée en slide latéral |

---

## 4. Cartographie Complète des Mods Minecraft En-Jeu

| Élément Mod | Fichier / Classe Clé | État Actuel | Transformation Visuelle Zenith v2 |
|---|---|---|---|
| **Startup Loading Screen** | `polyplus`: `StartupOverlay.class`, `StartupTextures.class`, `assets/polyplus/startup/` | Ancien logo fleur OneClient / Zap, textures étirées | Rendu vectoriel exact de l'apex Zenith, wordmark natif 5.34:1 sans distorsion, jauge de chargement bleu ciel, typographie Poppins Bold |
| **Écran d'Accueil En-Jeu (Main Menu)** | `polyplus`: `PolyPlusMainMenuScreen.class`, `assets/polyplus/mainmenu/` | Menu OneClient classique | Logo vectoriel Zenith centré, boutons de navigation aux contours bleu néon, boutons de profil et de serveurs partenaires rafraîchis |
| **Écran de Pause In-Game** | `polyplus`: `ZenithClientPauseScreen.class`, `assets/polyplus/brand/` | Pause screen OneClient avec boutons classiques | Interface en suspension avec flou d'arrière-plan du monde en pause, carte du joueur intégrée, boutons aux arrondis soignés et icônes Zenith |
| **Onboarding Initial En-Jeu** | `polyplus`: `PolyPlusOnboardingScreenKt.class` | Textes et logos hérités | Header modernisé "Welcome to ZenithClient", palette bleu nuit & bleu ciel, étapes de configuration avec cartes claires |
| **ClickGUI & Menus de Configuration** | `OneConfig` (`ZenithConfig`): `config-impl`, `ThemeConfig`, UI bridges | Thème violet/sombre OneConfig hérité | Thème Zenith appliqué : surfaces bleu nuit `#0A1128`, accent bleu ciel `#38BDF8`, sliders et toggles modernes, police d'écriture nette |
| **HUD & Overlays (Keystrokes, CPS, Watermark)** | `polyplus` & `OneConfig` HUD modules | Textes blancs bruts avec fond noir semi-transparent | Watermark compacte "Zenith Client [FPS]", badges keystrokes avec contours luminescents discrets au clic, arraylist avec dégradé chromatique bleu ciel -> bleu royal |
| **Textures, Atlas & Logos** | `assets/polyplus/` & `assets/oneconfig/` | Anciens SVGs et bitmaps Zap | Nouveaux fichiers graphiques Zenith vectoriels haute résolution, suppression totale des références Zap/One |

---

## 5. Synthèse des Tokens de Design Zenith v2

* **Palette Chromatique** :
  * Fond de base : `#060B1F`
  * Fond surface : `#0A1128`
  * Fond surélevé (cartes/panneaux) : `#101B3D`
  * Composants interactifs : `#14214B` & `#1A2B5E`
  * Accent Primaire (Bleu Royal) : `#1E40AF` (Hover: `#2563EB`)
  * Accent Secondaire (Bleu Ciel) : `#38BDF8` (Lueur/Active: `#7DD3FC`)
  * Texte Primaire : `#F1F5F9` (Blanc cassé)
  * Texte Secondaire : `#BAE6FD` (Bleu glacé)
  * Texte Atténué : `#64748B` (Gris bleu ardoise)
  * Bordures lumineuses : `rgba(56, 189, 248, 0.20)` (Hover: `rgba(56, 189, 248, 0.45)`)
* **Motion Tokens** :
  * Micro-interaction (hover/press) : 140ms, `Ease::Out`
  * Transition de composant (toggle/slider) : 220ms, `Ease::InOut`
  * Transition d'écran : 360ms, `Function::Cubic`
  * Révélation d'en-tête et splash : 620ms, `Ease::Out`
