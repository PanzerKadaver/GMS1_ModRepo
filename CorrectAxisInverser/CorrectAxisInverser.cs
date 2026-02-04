using BepInEx;
using BepInEx.Logging;

using HarmonyLib;

using UnityEngine;

namespace CorrectAxisInverser
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class CorrectAxisInverser : BaseUnityPlugin
	{
		internal static new ManualLogSource Logger;

		private void Awake()
		{
			Harmony.CreateAndPatchAll(typeof(CorrectAxisInverser));

			// Plugin startup logic
			Logger = base.Logger;
			Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded !");
		}

		[HarmonyPatch(typeof(InputManager), "GetAxis")]
		[HarmonyPrefix]
		static bool GetAxis_Prefix(string axisCode, ref float __result)
		{
			// Rewrite the whole method flow in according to last csharp good practices
			if (!global::Singleton<global::InputManager>.Instance.AllInputBlocked)
			{
				float flag = ((axisCode == "Mouse X" && global::InputManager.InvertX) || (axisCode == "Mouse Y" && global::InputManager.InvertY))! ? -1f : 1f; // Retrieve inversion parameter
				flag = global::InputManager.IsKeyboard() ? 1f : flag; // Don't invert if using keyboard+mouse

				float sensitivity = global::InputManager.IsKeyboard() ? global::InputManager.MouseSensivity : global::InputManager.PadSensivity; // Retrieve sensitivity based on input method
				sensitivity = (axisCode == "Mouse X" || axisCode == "Mouse Y") ? sensitivity : 1f; // Only apply sensitivity to camera axes

				__result = Rewired.ReInput.players.Players[0].GetAxis(axisCode) * flag * sensitivity;
			}

			return false; // Skip original method
		}

		/** ORIGINAL CODE on v1.10.2.3
		 * Lib: Assembly-CSharp.dll
		 * Namespace : None
		 * Class : InputManager
		 * Function : GetAxis
		 * Code:
		public static float GetAxis(string axisCode)
		{
			if (global::Singleton<global::InputManager>.Instance.AllInputBlocked)
			{
				return 0f;
			}
			bool flag = (axisCode == "Mouse X" && global::InputManager.InvertX) || (axisCode == "Mouse Y" && global::InputManager.InvertY);
			if ((axisCode == "Mouse X" || axisCode == "Mouse Y") && global::InputManager.IsKeyboard())
			{
				return ReInput.players.Players[0].GetAxis(global::InputManager.GetRealAxisName(axisCode)) * (flag ? (-1f) : 1f) * global::InputManager.MouseSensivity;
			}
			if ((axisCode == "Mouse X" || axisCode == "Mouse Y") && !global::InputManager.IsKeyboard())
			{
				return ReInput.players.Players[0].GetAxis(global::InputManager.GetRealAxisName(axisCode)) * (flag ? (-1f) : 1f) * global::InputManager.PadSensivity;
			}
			return ReInput.players.Players[0].GetAxis(global::InputManager.GetRealAxisName(axisCode)) * (flag ? (-1f) : 1f);
		}
		**/

		[HarmonyPatch(typeof(InputManager), "GetAxisRaw")]
		[HarmonyPrefix]
		static bool GetAxisRaw_Prefix(string axisCode, ref float __result)
		{
			// Rewrite the whole method flow in according to last csharp good practices
			if (!global::Singleton<global::InputManager>.Instance.AllInputBlocked)
			{
				float flag = ((axisCode == "Mouse X" && global::InputManager.InvertX) || (axisCode == "Mouse Y" && global::InputManager.InvertY))! ? -1f : 1f; // Retrieve inversion parameter
				flag = global::InputManager.IsKeyboard() ? 1f : flag; // Don't invert if using keyboard+mouse

				float sensitivity = (global::InputManager.IsKeyboard() ? global::InputManager.MouseSensivity : global::InputManager.PadSensivity) * 2f; // Retrieve sensitivity based on input method and apply raw multiplier
				sensitivity = (axisCode == "Mouse X" || axisCode == "Mouse Y") ? sensitivity : 1f; // Only apply sensitivity to camera axes

				__result = Rewired.ReInput.players.Players[0].GetAxisRaw(axisCode) * flag * sensitivity;
			}

			return false; // Skip original method
		}

		/** ORIGINAL CODE on v1.10.2.3
		 * Lib: Assembly-CSharp.dll
		 * Namespace : None
		 * Class : InputManager
		 * Function : GetAxisRaw
		 * Code:
		public static float GetAxisRaw(string axisCode)
		{
			if (global::Singleton<global::InputManager>.Instance.AllInputBlocked)
			{
				return 0f;
			}
			bool flag = (axisCode == "Mouse X" && global::InputManager.InvertX) || (axisCode == "Mouse Y" && global::InputManager.InvertY);
			if ((axisCode == "Mouse X" || axisCode == "Mouse Y") && global::InputManager.IsKeyboard())
			{
				return ReInput.players.Players[0].GetAxisRaw(global::InputManager.GetRealAxisName(axisCode)) * (flag ? (-1f) : 1f) * global::InputManager.MouseSensivity * 2f;
			}
			if ((axisCode == "Mouse X" || axisCode == "Mouse Y") && !global::InputManager.IsKeyboard())
			{
				return ReInput.players.Players[0].GetAxisRaw(global::InputManager.GetRealAxisName(axisCode)) * (flag ? (-1f) : 1f) * global::InputManager.PadSensivity * 2f;
			}
			return ReInput.players.Players[0].GetAxisRaw(global::InputManager.GetRealAxisName(axisCode)) * (flag ? (-1f) : 1f);
		}
		**/
	}
}