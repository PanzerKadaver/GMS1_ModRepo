using BepInEx;
using BepInEx.Logging;

using GoldDigger;

using HarmonyLib;

namespace MegaLadowarka
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class MegaLadowarka : BaseUnityPlugin
	{
		internal static new ManualLogSource Logger;

		private void Awake()
		{
			Harmony.CreateAndPatchAll(typeof(MegaLadowarka));

			// Plugin startup logic
			Logger = base.Logger;
			Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded !");
		}

		[HarmonyPatch(typeof(ItemDescriptor), "Price", MethodType.Getter)]
		[HarmonyPostfix]
		static void Price_Postfix(ItemDescriptor __instance, ref int __result)
		{
			if (__instance.ItemCategory == ItemDescriptor.EItemCategory.VEHICLE && __instance.ItemName == "SHOP_MACHINES_VEHICLE_980B_NAME")
			{
				__result *= 4;
			}
			if (__instance.ItemCategory == ItemDescriptor.EItemCategory.VEHICLE && __instance.ItemName == "SHOP_MACHINES_VEHICLE_DUMPTRUCK_NAME")
			{
				__result *= 3;
			}
		}

		[HarmonyPatch(typeof(Ladowarka), "Awake")]
		[HarmonyPostfix]
		static void Ladowarka_Awake_Postfix(Ladowarka __instance)
		{
			if (__instance.Type == MachineType.Loader)
			{
				__instance.Digging._maxShovelVolume = 42f;
				__instance.Fuel.ConsumptionPerH = 180f;
			}
		}

		[HarmonyPatch(typeof(DumpTruck), "Awake")]
		[HarmonyPostfix]
		static void DumpTruck_Awake_Postfix(DumpTruck __instance)
		{
			if (__instance.Type == MachineType.Moxy)
			{
				__instance.Digging._maxShovelVolume = 84f;
				__instance.Fuel.ConsumptionPerH = 100f;
			}
		}

		[HarmonyPatch(typeof(DiggingController), "IsFull")]
		[HarmonyPrefix]
		static bool DiggingController_IsFull_Prefix(DiggingController __instance, ref bool __result)
		{
			__result = (double)__instance._currShovelTotalVolume >= (double)__instance._maxShovelVolume;

			return false; // Skip original method
		}
	}
}