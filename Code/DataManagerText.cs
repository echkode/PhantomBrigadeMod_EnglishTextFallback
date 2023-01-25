using HarmonyLib;

using PBDataManagerText = PhantomBrigade.Data.DataManagerText;

using UnityEngine;

namespace EchKode.PBMods.EnglishTextFallback
{
	using GetTextDelegate = System.Func<string, string, bool, string>;

	static class DataManagerText
	{
		internal static bool IsInitialized;
		private static GetTextDelegate getEnglishFallbackText;

		internal static void Initialize()
		{
			if (IsInitialized)
			{
				return;
			}

			var mi = AccessTools.DeclaredMethod(
				typeof(PBDataManagerText),
				"GetTextFromLibrary",
				new System.Type[] { typeof(string), typeof(string), typeof(bool) });
			if (mi == null)
			{
				Debug.LogWarningFormat(
					"Mod {0} ({1}) | Unable to find GetTextFromLibrary method on DataManagerText with reflection",
					ModLink.modIndex,
					ModLink.modId);
				return;
			}
			getEnglishFallbackText = (GetTextDelegate)mi.CreateDelegate(typeof(GetTextDelegate));
			IsInitialized = true;
		}

		internal static string GetText(string sectorKey, string textKey)
		{
			var text = getEnglishFallbackText(sectorKey, textKey, false);
			if (text == null)
			{
				text = string.Format(ModLink.Settings.blankFallbackFormat, textKey);
			}

			if (ModLink.Settings.logWarningOnFallback)
			{
				Debug.LogWarningFormat(
					"Mod {0} ({1}) | Using English fallback text | sector: {2} | key: {3}",
					ModLink.modIndex,
					ModLink.modId,
					sectorKey,
					textKey);
			}

			return text;
		}
	}
}
