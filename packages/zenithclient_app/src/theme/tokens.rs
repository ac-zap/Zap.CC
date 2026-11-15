use freya::prelude::*;

/// Zenith v2 Design System Tokens
/// Centralized tokens for colors, spacing, radii, elevations, motion, and typography.

pub mod colors {
    use freya::prelude::Color;

    // Background surfaces (Dark / Deep Space)
    pub const BG_BASE: Color = Color::from_rgb(6, 11, 31);         // #060B1F
    pub const BG_SURFACE: Color = Color::from_rgb(10, 17, 40);     // #0A1128
    pub const BG_ELEVATED: Color = Color::from_rgb(16, 27, 61);    // #101B3D
    pub const BG_CARD: Color = Color::from_rgb(20, 33, 75);        // #14214B
    pub const BG_COMPONENT: Color = Color::from_rgb(26, 43, 94);   // #1A2B5E

    // Brand accents
    pub const PRIMARY: Color = Color::from_rgb(30, 64, 175);       // #1E40AF (Royal Blue)
    pub const PRIMARY_HOVER: Color = Color::from_rgb(39, 81, 208); // #2751D0
    pub const PRIMARY_PRESSED: Color = Color::from_rgb(23, 49, 138);

    pub const ACCENT: Color = Color::from_rgb(56, 189, 248);       // #38BDF8 (Sky Blue)
    pub const ACCENT_SOFT: Color = Color::from_rgb(125, 211, 252); // #7DD3FC
    pub const ICE: Color = Color::from_rgb(186, 230, 253);         // #BAE6FD

    // Typography
    pub const TEXT_PRIMARY: Color = Color::from_rgb(241, 245, 249);   // #F1F5F9
    pub const TEXT_SECONDARY: Color = Color::from_rgb(148, 163, 184); // #94A3B8
    pub const TEXT_MUTED: Color = Color::from_rgb(100, 116, 139);     // #64748B

    // Status colors
    pub const SUCCESS: Color = Color::from_rgb(16, 185, 129);      // #10B981 (Teal/Emerald)
    pub const WARNING: Color = Color::from_rgb(245, 158, 11);      // #F59E0B (Amber)
    pub const DANGER: Color = Color::from_rgb(239, 68, 68);        // #EF4444 (Coral Red)
    pub const INFO: Color = Color::from_rgb(56, 189, 248);          // #38BDF8 (Sky Blue)

    // Glow and borders
    pub const BORDER_SUBTLE: Color = Color::from_argb(38, 56, 189, 248);  // 15% opacity
    pub const BORDER_HOVER: Color = Color::from_argb(110, 56, 189, 248);  // 43% opacity
    pub const BORDER_ACTIVE: Color = Color::from_argb(200, 56, 189, 248); // 78% opacity
    pub const GLOW_ACCENT: Color = Color::from_argb(64, 56, 189, 248);   // 25% opacity
    pub const GLOW_PRIMARY: Color = Color::from_argb(80, 30, 64, 175);   // 31% opacity
}

pub mod spacing {
    pub const XXS: f32 = 2.0;
    pub const XS: f32 = 4.0;
    pub const SM: f32 = 8.0;
    pub const MD: f32 = 12.0;
    pub const LG: f32 = 16.0;
    pub const XL: f32 = 24.0;
    pub const XXL: f32 = 32.0;
    pub const HERO: f32 = 48.0;
}

pub mod radii {
    pub const SM: f32 = 6.0;
    pub const MD: f32 = 10.0;
    pub const LG: f32 = 14.0;
    pub const XL: f32 = 20.0;
    pub const FULL: f32 = 999.0;
}

pub mod motion {
    /// Milliseconds for instant response (used when reduced motion is enabled)
    pub const INSTANT_MS: u64 = 0;
    /// Hover, press, small toggle micro-interactions
    pub const MICRO_MS: u64 = 140;
    /// Component state change, input focus, switch toggle
    pub const SHORT_MS: u64 = 220;
    /// Modals, dropdown expands, tab switches
    pub const MEDIUM_MS: u64 = 340;
    /// Full screen transitions, startup reveal, large cards
    pub const LONG_MS: u64 = 520;
    /// Stagger delay between sequential items in a list
    pub const STAGGER_MS: u64 = 35;
}

pub mod typography {
    pub const DISPLAY_FONT: &str = "Poppins";
    pub const BODY_FONT: &str = "Poppins";
    pub const MONO_FONT: &str = "JetBrains Mono";

    pub const CAPTION: f32 = 11.0;
    pub const BODY_SM: f32 = 13.0;
    pub const BODY: f32 = 14.0;
    pub const BODY_LG: f32 = 16.0;
    pub const H3: f32 = 18.0;
    pub const H2: f32 = 24.0;
    pub const H1: f32 = 32.0;
    pub const DISPLAY: f32 = 56.0;
}
