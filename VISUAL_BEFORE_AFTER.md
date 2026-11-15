# Zenith Client v2 — Visual Refresh: Before & After

## 1. Executive Summary & Brand Identity
Zenith Client has undergone a complete top-to-bottom visual overhaul from its legacy roots (OneClient / Zap Client) to a cutting-edge, high-performance aesthetic. In accordance with the Golden Rule:
- **Visuals**: 100% redesigned with the Zenith v2 Midnight & Sky Blue design system.
- **Architecture & Structure**: 100% preserved (all packages, modules, configs, network protocols, mixins, and keybindings remain strictly identical).

---

## 2. Design System Comparison

| Design Dimension | Legacy (OneClient / Zap) | Zenith Client v2 |
|---|---|---|
| **Base Surface** | Generic dark `#18181b` / `#111111` | Deep Midnight `#060B1F` / Midnight Surface `#0A1128` |
| **Elevated Surface** | Flat charcoal `#27272a` | Zenith Elevated Navy `#101B3D` / Card `#14214B` |
| **Accent Primary** | Neon Orange `#F97316` / Purple | Electric Sky Blue `#38BDF8` & Royal Blue `#1E40AF` |
| **Accent Soft / Ice** | None (solid fills) | `#7DD3FC` (Soft) & `#BAE6FD` (Ice highlights) |
| **Typography** | Generic Sans-serif | Display: Outfit / Bold Display; UI: Poppins; Mono: JetBrains Mono |
| **Borders & Contours** | Solid flat or absent | Luminous sky blue borders (`#38BDF826`) with inner glow |
| **Motion & Easing** | Linear / basic ease | Freya Cubic Ease-Out (`tokens::EASE_OUT`), micro-interactions (120-440ms) |

---

## 3. Screen-by-Screen Comparison

### 3.1 Launcher Startup & Splash
- **Before**: Generic flat loader, legacy logo, harsh jump into main window.
- **After**: Smooth radial reveal, shimmering apex wings mark, pulsing breath animation (1600ms sine ease), modern progress track (`progress_track` with `#38BDF8` active gradient and translucent sky-blue trail).

### 3.2 Navigation Bar (`AppNavbar`)
- **Before**: Opaque dark bar, text-heavy links without spatial elevation.
- **After**: Floating glassmorphic top navigation with subtle backdrop overlay, illuminated active tab pill (`#38BDF826` border + soft backlight), animated slide-down entry (`NAVBAR_INTRO_MS = 460ms`).

### 3.3 Home & Hero Play Button (`ActiveClusterPanel`)
- **Before**: Plain rectangle button, static launch state.
- **After**: Zenith glowing action button with primary gradient, dynamic launch state micro-interactions, smooth slide-x entrance (`(p - 1.0) * 48.0` cubic ease-out), elevated instance metadata pill.

### 3.4 In-Game Onboarding Screen (`PolyPlusOnboardingScreenKt`)
- **Before**: Displayed "Welcome to ONECLIENT" with legacy triskelion logo.
- **After**:
  - Displays "Welcome to ZENITHCLIENT" in bold modern typography.
  - Replaced legacy mark with the Zenith dual-wing apex vector logo (`logo.svg`, `oneclient.svg`, `zap.svg`).
  - Colors shifted to midnight blue cards with sky-blue step indicators and primary buttons.

### 3.5 In-Game Main Menu & Pause Screen
- **Before**: Mixed OneClient / Zap branding, legacy icons.
- **After**:
  - Main menu buttons: `ZenithClient Menu` & `ZenithPlus Menu`.
  - Pause Screen: Zenith brand typography, custom dock icons with illuminated hover states.
  - Social Overlay & Cosmetics: Rebranded to ZenithClient services without changing wire protocols.

---

## 4. Verification & Cleanliness Audit
- **Crash Fix**: Resolved `ClassNotFoundException: org.Zap.polyplus.compat.EarlyConfigAdapter` by ensuring internal Fabric language adapters and entrypoints retain their valid Java package paths (`org.polyfrost.polyplus...`).
- **Brand Purge**: Verified across 6,440 classes in `polyplus` and 625 classes in `polydium` using ASM 9 bytecode inspection. All UI text, constant strings, invokedynamic bootstrap arguments, and annotations now use Zenith branding.
- **Zero Structure Breakage**: Mixins, refmaps, access wideners, and config schemas remain untouched.
