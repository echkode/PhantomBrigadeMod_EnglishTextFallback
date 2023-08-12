# EnglishTextFallback

**This mod is obsolete.** Localization support has been improved since I created this mod so it's no longer necessary.

A library mod for [Phantom Brigade (Alpha)](https://braceyourselfgames.com/phantom-brigade/) that will substitute the default English text for strings that are blank after localization or when an entire localization sector is missing.

This mod is primarily for modders who are doing localization work. This mod should not be needed for languages that have the proper localizations.

It is compatible with game patch **0.23.1-b5426**. That is an **EXPERIMENTAL** release. All library mods are fragile and susceptible to breakage whenever a new version is released.

There are a couple of settings that may be changed by adding a `settings.yaml` file to the mod directory, next to the `metadata.yaml` file.

| Setting Name | Value Type | Description |
| ------------ | ---------- | ----------- |
| logWarningOnFallback | bool | set to `true` to log every substitution |
| blankFallbackFormat | string | format string when there is no English substitution; this will use the text entry key as format parameter `{0}` |

Logging is sent to the standard Unity player.log file found at `C:\Users\<username>\AppData\LocalLow\Brace Yourself Games\Phantom Brigade\Player.log`.

The `settings.yaml` file in this repo is an example. It is not needed and the mod doesn't consider it an error if it can't find it.
