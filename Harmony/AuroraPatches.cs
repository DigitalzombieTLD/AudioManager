using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace AudioMgr
{   
    
    [HarmonyLib.HarmonyPatch(typeof(AuroraActivatedToggle), "SetState", new Type[] { typeof(AuroraActivatedToggleState) })]
    public class TurnOnPatch
    {
        public static void Prefix(ref AuroraActivatedToggle __instance, ref AuroraActivatedToggleState state)
        {           
            if (Settings.options.customRadioMusic)
            {
                if (__instance.m_ToggleOffAudio == "Stop_RadioAurora" && state == AuroraActivatedToggleState.On)
                {
                    if (Settings.options.radioWorksWithoutAurora)
                    {
                        __instance.m_RequiresAuroraField = false;
                       
                        for (int i = 0; i < __instance.transform.childCount; i++)
                        {
                            __instance.transform.GetChild(i).gameObject.SetActive(true);
                        }
                        __instance.m_ChildrenEnabled = true;
                        //__instance.UpdateChildStatus(true);

                        MelonLogger.Msg("StartPlay");
                        RadioMaster.StartPlay(__instance.gameObject);
                    }
                }

                if (__instance.m_ToggleOffAudio == "Stop_RadioAurora" && state == AuroraActivatedToggleState.Off)
                {
                    if (Settings.options.radioWorksWithoutAurora)
                    {
                        MelonLogger.Msg("StopPlay");
                        RadioMaster.StopPlay(__instance.gameObject);
                    }
                }
            }
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(AuroraActivatedToggle), "Update")]
    public class LightingPatch
    {
        public static bool Prefix(ref AuroraActivatedToggle __instance)
        {
            if (Settings.options.customRadioMusic && Settings.options.radioWorksWithoutAurora)
            {
                __instance.m_RequiresAuroraField = false;
                return false;
            }

            return true;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(AuroraActivatedToggle), "ShouldEnableChildren")]
    public class ShouldEnableEffectPatch
    {
        public static void Postfix(ref AuroraActivatedToggle __instance, ref bool __result)
        {
            if (Settings.options.customRadioMusic && Settings.options.radioWorksWithoutAurora)
            {
                __result = true;
            }           
        }
    }

}
