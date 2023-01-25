// Copyright (c) 2023 EchKode
// SPDX-License-Identifier: BSD-3-Clause

using System.Collections.Generic;

using HarmonyLib;

using PhantomBrigade.Data;
using PBDataManagerText = PhantomBrigade.Data.DataManagerText;
using PBModManager = PhantomBrigade.Mods.ModManager;

namespace EchKode.PBMods.EnglishTextFallback
{
	[HarmonyPatch]
	static class Patch
	{
		[HarmonyPatch(typeof(PBDataManagerText), "GetText")]
		[HarmonyPostfix]
		static void Dmt_GetTextPostfix(
			ref string __result,
			string sectorKey,
			string textKey)
		{
			if (string.IsNullOrEmpty(__result))
			{
				__result = DataManagerText.GetText(sectorKey, textKey);
			}
		}

		[HarmonyPatch(typeof(PBModManager), "ProcessLocalizationEdits")]
		[HarmonyPrefix]
		static bool Mm_ProcessLocalizationEditsPrefix(
			string languageName,
			SortedDictionary<string, DataContainerTextSectorLocalization> sectors)
		{
			ModManager.ProcessLocalizationEdits(languageName, sectors);
			return false;
		}
	}
}
