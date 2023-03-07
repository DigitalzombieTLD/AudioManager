using Il2Cpp;
using Il2CppAudio.SimpleAudio;
using Il2CppNodeCanvas.Tasks.Actions;
using MelonLoader;
using UnityEngine;

namespace AudioMgr
{      
    [HarmonyLib.HarmonyPatch(typeof(PlayAudioSimple), "Start")]
    public class PlayAudioSimplePatch
    {    
        public static bool Prefix(ref PlayAudioSimple __instance)
        {
            //MelonLogger.Msg("Play simple started " + __instance.m_Event.Name + " on " + __instance.gameObject.name);

            if (PatchMaster.PatchAction(__instance.name, __instance.gameObject))
            {
                return false;
            }

            if (PatchMaster.PatchAction(__instance.m_Event.Name, __instance.gameObject))
            {
                return false;
            }

            if (PatchMaster.PatchAction(__instance.m_EventName, __instance.gameObject))
            {
                return false;
            }

            return true;
        }
    }
}
