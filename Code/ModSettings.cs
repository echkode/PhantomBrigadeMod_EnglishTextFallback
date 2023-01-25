// Copyright (c) 2023 EchKode
// SPDX-License-Identifier: BSD-3-Clause

using System.IO;

using UnityEngine;

namespace EchKode.PBMods.EnglishTextFallback
{
	partial class ModLink
	{
		internal sealed class ModSettings
		{
#pragma warning disable CS0649
			public bool logWarningOnFallback;
			public string blankFallbackFormat = "[b][ff8888]${0}[-][/b]";
#pragma warning restore CS0649
		}

		internal static ModSettings Settings;

		private static void LoadSettings()
		{
			var isDefaults = false;
			var settingsPath = Path.Combine(modPath, "settings.yaml");
			Settings = UtilitiesYAML.ReadFromFile<ModSettings>(settingsPath, false);
			if (Settings == null)
			{
				Settings = new ModSettings();
				isDefaults = true;
			}
			Debug.LogFormat(
				"Mod {0} ({1}) | Settings | using defaults: {2} | path: {3}\n  {4}: {5}\n  {6}: {7}",
				modIndex,
				modId,
				isDefaults,
				settingsPath,
				nameof(Settings.logWarningOnFallback),
				Settings.logWarningOnFallback,
				nameof(Settings.blankFallbackFormat),
				Settings.blankFallbackFormat);
		}
	}
}
