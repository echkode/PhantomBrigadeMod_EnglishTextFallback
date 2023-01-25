using System.Collections.Generic;

using PhantomBrigade.Data;
using PhantomBrigade.Mods;
using PBModManager = PhantomBrigade.Mods.ModManager;

using UnityEngine;

namespace EchKode.PBMods.EnglishTextFallback
{
	static class ModManager
	{
		internal static void ProcessLocalizationEdits(
			string languageName,
			SortedDictionary<string, DataContainerTextSectorLocalization> sectors)
		{
			if (!DataManagerText.IsInitialized)
			{
				return;
			}

			if (PBModManager.config == null)
			{
				return;
			}
			if (!PBModManager.config.enabled)
			{
				return;
			}
			if (PBModManager.loadedMods == null)
			{
				return;
			}
			if (sectors == null)
			{
				return;
			}
			if (sectors.Count == 0)
			{
				return;
			}

			for (int index = 0; index < PBModManager.loadedMods.Count; index += 1)
			{
				var loadedMod = PBModManager.loadedMods[index];
				if (loadedMod == null)
				{
					continue;
				}
				if (loadedMod.metadata == null)
				{
					continue;
				}
				if (!loadedMod.metadata.includesLocalizationEdits)
				{
					continue;
				}
				if (loadedMod.localizationEdits == null)
				{
					continue;
				}

				var id = loadedMod.metadata.id;
				if (!loadedMod.localizationEdits.ContainsKey(languageName))
				{
					Debug.LogWarningFormat(
						"Mod {0} ({1}) | Failed to apply localization edits: current language {2} is not covered by the mod",
						index,
						id,
						languageName);
					continue;
				}

				var localizationEdits = loadedMod.localizationEdits[languageName];
				if (localizationEdits == null)
				{
					continue;
				}
				if (localizationEdits.Count == 0)
				{
					continue;
				}

				ApplyEdits(
					languageName,
					sectors,
					index,
					id,
					localizationEdits);
			}
		}

		private static void ApplyEdits(
			string languageName,
			SortedDictionary<string, DataContainerTextSectorLocalization> sectors,
			int index,
			string id,
			List<ModLocalizationEditLoaded> localizationEdits)
		{
			foreach (var edit in localizationEdits)
			{
				if (edit == null)
				{
					continue;
				}
				if (edit.data == null)
				{
					continue;
				}
				if (edit.data.edits == null)
				{
					continue;
				}
				if (edit.data.edits.Count == 0)
				{
					continue;
				}

				var key = edit.key;
				if (string.IsNullOrEmpty(key) || !sectors.ContainsKey(key))
				{
					Debug.LogWarningFormat(
						"Mod {0} ({1}) | Failed to apply localization edit to language {2} sector {3}: no such sector found",
						index,
						id,
						languageName,
						key);
					continue;
				}

				var entries = sectors[key].entries;
				foreach (var kvp in edit.data.edits)
				{
					ApplyEdit(
						languageName,
						index,
						id,
						key,
						entries,
						kvp.Key,
						kvp.Value);
				}
			}
		}

		private static void ApplyEdit(
			string languageName,
			int index,
			string id,
			string sectorKey,
			SortedDictionary<string, DataBlockTextEntryLocalization> entries,
			string entryKey,
			string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				text = DataManagerText.GetText(sectorKey, entryKey);
			}

			if (entries.ContainsKey(entryKey))
			{
				Debug.LogWarningFormat(
					"Mod {0} ({1}) | Replacing {2} text in sector {3} key {4}",
					index,
					id,
					languageName,
					sectorKey,
					entryKey);
				entries[entryKey].text = text;
				return;
			}

			Debug.LogWarningFormat("Mod {0} ({1}) | Adding {2} text in sector {3} key {4}",
				index,
				id,
				languageName,
				sectorKey,
				entryKey);
			entries.Add(entryKey, new DataBlockTextEntryLocalization()
			{
				text = text
			});
		}
	}
}
