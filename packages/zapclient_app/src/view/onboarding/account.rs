use freya::prelude::*;
use freya::query::{MutationCapability, MutationStateData, UseMutation};
use zapclient_auth::{AccountKind, MinecraftAccount};

use crate::components::{
    Avatar, Button, Icon, IconType, TextInput, use_microsoft_login,
};
use crate::hooks::{
    AddOfflineAccountKeys, RemoveAccountKeys, try_default_account, use_add_offline_account,
    use_current_account, use_remove_account,
};
use crate::i18n::t;
use crate::routes::Route;
use crate::theme::{colors, rounded, text_size};
use crate::ui::border_all_color;
use crate::view::onboarding::{
    onboarding_illustration, onboarding_nav, onboarding_page, step_heading,
};

#[derive(PartialEq)]
pub struct OnboardingAccount;

impl Component for OnboardingAccount {
    fn render(&self) -> impl IntoElement {
        let account_query = use_current_account();
        let msa = use_microsoft_login();
        let add_offline = use_add_offline_account();
        let remove_account = use_remove_account();

        let mut username = use_state(String::new);
        let mut offline_error_state = use_state(|| None::<String>);

        let account = try_default_account(&account_query);
        let has_account = account.is_some();

        let offline_err = mutation_err_text(&add_offline)
            .or_else(|| offline_error_state.read().clone());

        let on_add_offline = {
            let add_offline = add_offline.clone();
            let mut offline_error_state = offline_error_state.clone();
            move |_| {
                let name = username.peek().trim().to_string();
                if name.len() < 3 || name.len() > 16 {
                    offline_error_state.set(Some("Le pseudo doit contenir entre 3 et 16 caractères".to_string()));
                    return;
                }
                if !name.chars().all(|c| c.is_ascii_alphanumeric() || c == '_') {
                    offline_error_state.set(Some("Le pseudo ne peut contenir que des lettres, chiffres et tirets du bas".to_string()));
                    return;
                }
                offline_error_state.set(None);
                add_offline.mutate(AddOfflineAccountKeys { username: name });
            }
        };

        let on_remove = {
            let remove_account = remove_account.clone();
            let account_id = account.as_ref().map(|a| a.id);
            move |_| {
                if let Some(id) = account_id {
                    remove_account.mutate(RemoveAccountKeys { id });
                }
            }
        };

        let content = rect()
            .vertical()
            .width(Size::fill())
            .spacing(20.)
            .child(step_heading(
                t("zapclient.onboarding.account.title"),
                "Connectez votre compte Microsoft ou jouez directement avec un compte hors-ligne (crack).",
            ))
            .child(match &account {
                Some(acc) => account_card(acc, on_remove).into_element(),
                None => {
                    let start = msa.clone();
                    account_selection(
                        msa.pending,
                        msa.error.clone(),
                        move |_| start.start(),
                        username,
                        offline_err,
                        on_add_offline,
                    )
                    .into_element()
                }
            })
            .into_element();

        let page = onboarding_page(
            onboarding_illustration(IconType::OnboardingAccount),
            content,
            onboarding_nav(
                Some(Route::OnboardingLanguage {}),
                Route::OnboardingBundles {},
                has_account,
            ),
        );

        rect()
            .width(Size::fill())
            .height(Size::fill())
            .child(page)
            .maybe_child(msa.popup())
    }
}

fn account_card(
    account: &MinecraftAccount,
    on_change: impl FnMut(Event<PressEventData>) + 'static,
) -> impl IntoElement {
    let is_offline = account.kind == AccountKind::Offline;
    let badge_text = if is_offline {
        "Compte Hors-ligne (Crack)"
    } else {
        "Compte Microsoft"
    };

    rect()
        .vertical()
        .width(Size::fill())
        .spacing(16.)
        .padding(Gaps::new_all(16.))
        .corner_radius(rounded(12.))
        .background(colors::component_bg())
        .border(border_all_color(1., colors::component_border()))
        .child(
            rect()
                .horizontal()
                .spacing(14.)
                .cross_align(Alignment::Center)
                .child(
                    Avatar::new(account.id.to_string())
                        .width(Size::px(48.))
                        .height(Size::px(48.)),
                )
                .child(
                    rect()
                        .vertical()
                        .width(Size::flex(1.0))
                        .spacing(3.)
                        .child(
                            label()
                                .text(account.username.clone())
                                .font_size(text_size(17.))
                                .font_weight(FontWeight::BOLD)
                                .color(colors::fg_primary()),
                        )
                        .child(
                            label()
                                .text(badge_text)
                                .font_size(text_size(12.))
                                .color(if is_offline {
                                    colors::accent()
                                } else {
                                    colors::fg_secondary()
                                }),
                        ),
                )
                .child(
                    Button::new()
                        .secondary()
                        .on_press(on_change)
                        .child(Icon::new(IconType::RefreshCw01).size(14.))
                        .text("Changer"),
                ),
        )
        .into_element()
}

fn account_selection(
    msa_pending: bool,
    msa_error: Option<String>,
    on_msa: impl FnMut(Event<PressEventData>) + 'static,
    username: State<String>,
    offline_error: Option<String>,
    on_add_offline: impl FnMut(Event<PressEventData>) + 'static,
) -> impl IntoElement {
    let can_submit_offline = !username.read().trim().is_empty();

    rect()
        .vertical()
        .spacing(14.)
        .width(Size::fill())
        // Microsoft Option Card
        .child(
            rect()
                .vertical()
                .width(Size::fill())
                .spacing(10.)
                .padding(Gaps::new_all(14.))
                .corner_radius(rounded(10.))
                .background(colors::component_bg())
                .border(border_all_color(1., colors::component_border()))
                .child(
                    rect()
                        .horizontal()
                        .width(Size::fill())
                        .cross_align(Alignment::Center)
                        .spacing(12.)
                        .child(
                            rect()
                                .vertical()
                                .width(Size::flex(1.0))
                                .spacing(2.)
                                .child(
                                    label()
                                        .text("Compte Microsoft")
                                        .font_size(text_size(14.))
                                        .font_weight(FontWeight::SEMI_BOLD)
                                        .color(colors::fg_primary()),
                                )
                                .child(
                                    label()
                                        .text("Connexion avec vos identifiants Microsoft / Xbox")
                                        .font_size(text_size(12.))
                                        .color(colors::fg_secondary()),
                                ),
                        )
                        .child(
                            Button::new()
                                .primary()
                                .enabled(!msa_pending)
                                .on_press(on_msa)
                                .child(Icon::new(IconType::Globe01).size(16.))
                                .text(if msa_pending {
                                    "Connexion..."
                                } else {
                                    "Se connecter"
                                }),
                        ),
                )
                .maybe_child(msa_error.map(|msg| {
                    error_line(msg)
                })),
        )
        // Separator
        .child(
            rect()
                .horizontal()
                .width(Size::fill())
                .cross_align(Alignment::Center)
                .spacing(12.)
                .child(rect().height(Size::px(1.)).width(Size::flex(1.0)).background(colors::component_border()))
                .child(
                    label()
                        .text("OU")
                        .font_size(text_size(11.))
                        .font_weight(FontWeight::BOLD)
                        .color(colors::fg_secondary()),
                )
                .child(rect().height(Size::px(1.)).width(Size::flex(1.0)).background(colors::component_border())),
        )
        // Offline / Crack Card
        .child(
            rect()
                .vertical()
                .width(Size::fill())
                .spacing(10.)
                .padding(Gaps::new_all(14.))
                .corner_radius(rounded(10.))
                .background(colors::component_bg())
                .border(border_all_color(1., colors::component_border()))
                .child(
                    rect()
                        .vertical()
                        .spacing(2.)
                        .child(
                            label()
                                .text("Compte Hors-ligne (Crack)")
                                .font_size(text_size(14.))
                                .font_weight(FontWeight::SEMI_BOLD)
                                .color(colors::fg_primary()),
                        )
                        .child(
                            label()
                                .text("Choisissez simplement un pseudo pour jouer :")
                                .font_size(text_size(12.))
                                .color(colors::fg_secondary()),
                        ),
                )
                .child(
                    rect()
                        .horizontal()
                        .width(Size::fill())
                        .cross_align(Alignment::Center)
                        .spacing(10.)
                        .child(
                            rect()
                                .width(Size::flex(1.0))
                                .child(TextInput::new(username).placeholder("Pseudo Minecraft (ex: Steve)")),
                        )
                        .child(
                            Button::new()
                                .secondary()
                                .enabled(can_submit_offline)
                                .on_press(on_add_offline)
                                .child(Icon::new(IconType::Plus).size(16.))
                                .text("Ajouter"),
                        ),
                )
                .maybe_child(offline_error.map(|msg| {
                    error_line(msg)
                })),
        )
        .into_element()
}

fn error_line(msg: String) -> impl IntoElement {
    rect()
        .horizontal()
        .cross_align(Alignment::Center)
        .spacing(6.)
        .child(
            Icon::new(IconType::AlertTriangle)
                .size(13.)
                .color(colors::danger()),
        )
        .child(
            label()
                .text(msg)
                .font_size(text_size(12.))
                .color(colors::danger()),
        )
        .into_element()
}

fn mutation_err_text<M>(mutation: &UseMutation<M>) -> Option<String>
where
    M: MutationCapability,
    M::Err: std::fmt::Display,
{
    match &*mutation.read().state() {
        MutationStateData::Settled { res: Err(err), .. } => Some(err.to_string()),
        MutationStateData::Loading {
            res: Some(Err(err)),
        } => Some(err.to_string()),
        _ => None,
    }
}
