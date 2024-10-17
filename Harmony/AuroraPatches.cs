using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace AudioMgr
{   
    
    [HarmonyLib.HarmonyPatch(typeof(AuroraActivatedToggle), "SetState", new Type[] { typeof(AuroraActivatedToggleState) })]
    public class TurnOnPatch
    {
        public static bool Prefix(ref AuroraActivatedToggle __instance, ref AuroraActivatedToggleState state)
        {            
            if (Settings.options.customRadioMusic && RadioMaster.auroraClipManager.clipCount > 0)
            {
                if (state == AuroraActivatedToggleState.On)
                {
                    if (GameManager.GetAuroraManager().AuroraIsActive() || (!GameManager.GetAuroraManager().AuroraIsActive() && Settings.options.radioWorksWithoutAurora))
                    {                     
                        
                        RadioMaster.StartQueue(__instance.gameObject);
                        __instance.m_ToggleState = state;
                        return false;
                    }                    
                }              
            }

            if (state == AuroraActivatedToggleState.Off)
            {
                RadioMaster.StopQueue(__instance.gameObject);
                __instance.m_ToggleState = state;
                return true;
            }

            return true;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(AuroraManager), "AuroraAudioSetIntensityRTPC")]
    public class TempAuroraMusicPatch
    {
        public static bool Prefix(ref AuroraManager __instance)
        {          
            return false;
        }
    }

    /*
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
            if (Settings.options.customRadioMusic && Settings.options.radioWorksWithoutAurora && RadioMaster.auroraClipManager.clipCount > 0)
            {
                __result = true;
            }           
        }
    }
    */

}
