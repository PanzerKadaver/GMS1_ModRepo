using BepInEx;
using BepInEx.Logging;

using HarmonyLib;

using System.Reflection;
using System.Reflection.Emit;

namespace LosslessBlacksmith
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class LosslessBlacksmith : BaseUnityPlugin
	{
		internal static new ManualLogSource Logger;

		private void Awake()
		{
			Harmony.CreateAndPatchAll(typeof(LosslessBlacksmith));

			// Plugin startup logic
			Logger = base.Logger;
			Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded !");
		}

		[HarmonyPatch(typeof(GoldDigger.BlacksmithCraftGUI), "OnEnable")]
		[HarmonyPostfix]
		static void OnEnable_Postfix(GoldDigger.BlacksmithCraftGUI __instance)
		{
			float[] fill = { 0f, 0.5f, 0.75f, 1f };
			int lvl = Singleton<GameStateManager>.Instance.BlLossLv;

			__instance.GoldLossesWheelBonus.fillAmount = fill[lvl];
		}

		[HarmonyPatch(typeof(GoldDigger.BlacksmithCraftGUI), "GetGoldLosses")]
		[HarmonyPrefix]
		static bool GetGoldLosses_Prefix(GoldDigger.BlacksmithCraftGUI __instance, ref FixedGold __result)
		{
			float[] loss = { 0.1f, 0.05f, 0.025f, 0f };
			int lvl = Singleton<GameStateManager>.Instance.BlLossLv;

			__result = __instance.AmountOfGold * loss[lvl];

			return false; // Skip original method
		}
	}
}