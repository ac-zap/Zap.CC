use zenithclient_cluster::naming::validate_instance_name;

use super::data::Picks;
use super::model::{
    FolderChoice, ImportMode, ImportSource, ModpackOrigin, Step, TypeChoice, Wizard,
};
use crate::i18n::{t, tf};
use crate::utils::count_label;

pub fn heading(picks: &Picks) -> (&'static str, String) {
    match picks.step {
        Step::Type => (
            t("zenithclient.clusters.instance.copy.type.title"),
            t("zenithclient.clusters.instance.copy.type.subtitle").to_string(),
        ),
        Step::Folder => (
            t("zenithclient.clusters.instance.copy.folder.title"),
            t("zenithclient.clusters.instance.copy.folder.subtitle").to_string(),
        ),
        Step::Loader => (
            t("zenithclient.clusters.instance.copy.loader.title"),
            match &picks.versions.chosen {
                Some(version) => tf(
                    "zenithclient.clusters.instance.copy.loader.subtitle_with_version",
                    &[&version],
                ),
                None => t("zenithclient.clusters.instance.copy.loader.subtitle").to_string(),
            },
        ),
        Step::Version => (
            t("zenithclient.clusters.instance.copy.version.title"),
            match picks.choice {
                TypeChoice::ZapClient => {
                    t("zenithclient.clusters.instance.copy.version.subtitle.ZapClient").to_string()
                }
                TypeChoice::Scratch | TypeChoice::Modpack | TypeChoice::Import => {
                    t("zenithclient.clusters.instance.copy.version.subtitle.any").to_string()
                }
            },
        ),
        Step::Bundles => (
            t("zenithclient.clusters.instance.copy.bundles.title"),
            t("zenithclient.clusters.instance.copy.bundles.subtitle").to_string(),
        ),
        Step::Customize => (
            t("zenithclient.clusters.instance.copy.customize.title"),
            t("zenithclient.clusters.instance.copy.customize.subtitle").to_string(),
        ),
        Step::Modpack => (
            t("zenithclient.clusters.instance.copy.modpack.title"),
            t("zenithclient.clusters.instance.copy.modpack.subtitle").to_string(),
        ),
        Step::ImportSource => (
            t("zenithclient.clusters.instance.copy.import_source.title"),
            t("zenithclient.clusters.instance.copy.import_source.subtitle").to_string(),
        ),
        Step::ImportMode => (
            t("zenithclient.clusters.instance.copy.import_mode.title"),
            t("zenithclient.clusters.instance.copy.import_mode.subtitle").to_string(),
        ),
        Step::Import => (
            t("zenithclient.clusters.instance.copy.import.title"),
            t("zenithclient.clusters.instance.copy.import.subtitle").to_string(),
        ),
        Step::ImportReview => (
            t("zenithclient.clusters.instance.copy.import_review.title"),
            t("zenithclient.clusters.instance.copy.import_review.subtitle").to_string(),
        ),
    }
}

pub fn footer_note(picks: &Picks) -> String {
    match picks.step {
        Step::Type => match picks.choice {
            TypeChoice::ZapClient => {
                t("zenithclient.clusters.instance.copy.note.type.ZapClient").to_string()
            }
            TypeChoice::Scratch => {
                t("zenithclient.clusters.instance.copy.note.type.scratch").to_string()
            }
            TypeChoice::Modpack => {
                t("zenithclient.clusters.instance.copy.note.type.modpack").to_string()
            }
            TypeChoice::Import => {
                t("zenithclient.clusters.instance.copy.note.type.import").to_string()
            }
        },
        Step::Folder => match &picks.folder {
            FolderChoice::Shared => {
                t("zenithclient.clusters.instance.copy.note.folder.shared").to_string()
            }
            FolderChoice::Dedicated => {
                t("zenithclient.clusters.instance.copy.note.folder.dedicated").to_string()
            }
            FolderChoice::Custom(group) => tf(
                "zenithclient.clusters.instance.copy.note.folder.custom",
                &[group],
            ),
        },
        Step::Loader => match (&picks.versions.chosen, picks.loader.chosen) {
            (Some(version), None) => {
                tf("zenithclient.clusters.instance.copy.note.loader", &[&version])
            }
            _ => picks.loader_label(),
        },
        Step::Version => match &picks.versions.chosen {
            Some(version) => tf(
                "zenithclient.clusters.instance.copy.note.version.chosen",
                &[&version],
            ),
            None => t("zenithclient.clusters.instance.copy.note.version.none").to_string(),
        },
        Step::Bundles => {
            let taken = picks.bundles.taken_count();
            let total = picks.bundles.archives.len();
            if taken == 0 {
                t("zenithclient.clusters.instance.copy.note.bundles.none").to_string()
            } else {
                tf(
                    "zenithclient.clusters.instance.copy.note.bundles.some",
                    &[&taken, &total],
                )
            }
        }
        Step::Customize => t("zenithclient.clusters.instance.copy.note.customize").to_string(),
        Step::Modpack => match picks.modpack_origin {
            ModpackOrigin::Browse => {
                t("zenithclient.clusters.instance.copy.note.modpack.browse").to_string()
            }
            ModpackOrigin::File => {
                t("zenithclient.clusters.instance.copy.note.modpack.file").to_string()
            }
        },
        Step::ImportSource => match picks.import_source {
            ImportSource::File => {
                t("zenithclient.clusters.instance.copy.note.import_source.file").to_string()
            }
            ImportSource::Code => {
                t("zenithclient.clusters.instance.copy.note.import_source.code").to_string()
            }
            ImportSource::Launcher => {
                t("zenithclient.clusters.instance.copy.note.import_source.launcher").to_string()
            }
        },
        Step::ImportMode => match picks.import_mode {
            ImportMode::AsIs => {
                t("zenithclient.clusters.instance.copy.note.import_mode.as_is").to_string()
            }
            ImportMode::Improve => {
                t("zenithclient.clusters.instance.copy.note.import_mode.improve").to_string()
            }
        },
        Step::Import => match picks.import_count {
            0 => t("zenithclient.clusters.instance.copy.note.import.none").to_string(),
            1 => t("zenithclient.clusters.instance.copy.note.import.one").to_string(),
            n => tf("zenithclient.clusters.instance.copy.note.import.other", &[&n]),
        },
        Step::ImportReview => match &picks.import_screening {
            None if picks.import_screening_error.is_some() => {
                t("zenithclient.clusters.instance.copy.note.import_review.unchecked").to_string()
            }
            None => t("zenithclient.clusters.instance.copy.note.import_review.checking").to_string(),
            Some(_) => match review_parts(picks).as_slice() {
                [] => t("zenithclient.clusters.instance.copy.note.import_review.none").to_string(),
                parts => tf(
                    "zenithclient.clusters.instance.copy.note.import_review.parts",
                    &[&parts.join(", ")],
                ),
            },
        },
    }
}

pub fn step_value(wizard: Wizard, picks: &Picks, step: Step) -> String {
    match step {
        Step::Type => match picks.choice {
            TypeChoice::ZapClient => "ZenithClient".to_string(),
            TypeChoice::Scratch => t("zenithclient.clusters.instance.copy.value.scratch").to_string(),
            TypeChoice::Modpack => t("zenithclient.clusters.instance.copy.value.modpack").to_string(),
            TypeChoice::Import => t("zenithclient.clusters.instance.copy.value.import").to_string(),
        },
        Step::Folder => match &picks.folder {
            FolderChoice::Shared => t("zenithclient.clusters.instance.copy.value.shared").to_string(),
            FolderChoice::Dedicated => {
                t("zenithclient.clusters.instance.copy.value.dedicated").to_string()
            }
            FolderChoice::Custom(group) => group.clone(),
        },
        Step::Loader => picks.loader_label(),
        Step::Version => {
            picks.versions.chosen.clone().unwrap_or_else(|| {
                t("zenithclient.clusters.instance.copy.value.not_chosen").to_string()
            })
        }
        Step::Bundles => {
            let taken = picks.bundles.taken_count();
            if taken == 0 {
                t("zenithclient.clusters.instance.copy.value.none").to_string()
            } else {
                tf("zenithclient.clusters.instance.copy.value.selected", &[&taken])
            }
        }
        Step::Modpack => match picks.modpack_origin {
            ModpackOrigin::Browse => t("zenithclient.clusters.instance.copy.value.browse").to_string(),
            ModpackOrigin::File => t("zenithclient.clusters.instance.copy.value.file").to_string(),
        },
        Step::ImportSource => match picks.import_source {
            ImportSource::File => t("zenithclient.clusters.instance.copy.value.file").to_string(),
            ImportSource::Code => t("zenithclient.clusters.instance.copy.value.code").to_string(),
            ImportSource::Launcher => {
                t("zenithclient.clusters.instance.copy.value.launcher").to_string()
            }
        },
        Step::ImportMode => match picks.import_mode {
            ImportMode::AsIs => t("zenithclient.clusters.instance.copy.value.as_is").to_string(),
            ImportMode::Improve => t("zenithclient.clusters.instance.copy.value.improved").to_string(),
        },
        Step::Import => match picks.import_count {
            0 => t("zenithclient.clusters.instance.copy.value.none_selected").to_string(),
            n => tf("zenithclient.clusters.instance.copy.value.selected", &[&n]),
        },
        Step::ImportReview => match &picks.import_screening {
            None if picks.import_screening_error.is_some() => {
                t("zenithclient.clusters.instance.copy.value.not_checked").to_string()
            }
            None => t("zenithclient.clusters.instance.copy.value.checking").to_string(),
            Some(_) => match review_parts(picks).as_slice() {
                [] => t("zenithclient.clusters.instance.copy.value.nothing_flagged").to_string(),
                parts => parts.join(", "),
            },
        },
        Step::Customize => match wizard.details.tags.read().len() {
            0 => t("zenithclient.clusters.instance.copy.value.optional").to_string(),
            1 => t("zenithclient.clusters.instance.copy.value.tags.one").to_string(),
            many => tf(
                "zenithclient.clusters.instance.copy.value.tags.other",
                &[&many],
            ),
        },
    }
}

fn review_parts(picks: &Picks) -> Vec<String> {
    [
        (
            picks.flagged_count(),
            t("zenithclient.clusters.instance.copy.value.flagged.one"),
            t("zenithclient.clusters.instance.copy.value.flagged.other"),
        ),
        (
            picks.unpublished_count(),
            t("zenithclient.clusters.instance.copy.value.unpublished.one"),
            t("zenithclient.clusters.instance.copy.value.unpublished.other"),
        ),
        (
            picks.unconfirmed_count(),
            t("zenithclient.clusters.instance.copy.value.unconfirmed.one"),
            t("zenithclient.clusters.instance.copy.value.unconfirmed.other"),
        ),
    ]
    .into_iter()
    .filter(|(count, _, _)| *count > 0)
    .map(|(count, one, many)| count_label(count, one, many))
    .collect()
}

pub fn is_ready(picks: &Picks) -> bool {
    match picks.step {
        Step::Type | Step::Folder | Step::Bundles | Step::Modpack => true,
        Step::Loader => picks.loader.chosen.is_some(),
        Step::Version => {
            picks.versions.chosen.is_some()
                && (picks.choice == TypeChoice::Scratch || picks.loader.chosen.is_some())
        }
        Step::Customize => validate_instance_name(&picks.name).is_ok(),
        Step::ImportSource => {
            picks.import_source != ImportSource::Code
                || zenithclient_core::sharing::ShareCode::parse(&picks.share_code).is_some()
        }
        Step::ImportMode => true,
        Step::Import => picks.import_count > 0,
        Step::ImportReview => {
            picks.import_count > 0
                && (picks.import_screening.is_some() || picks.import_screening_error.is_some())
        }
    }
}
