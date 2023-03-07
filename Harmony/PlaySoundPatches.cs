using Il2Cpp;
using Il2CppNodeCanvas.Tasks.Actions;
using MelonLoader;
using UnityEngine;

namespace AudioMgr
{    
    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlaySound", new Type[] { typeof(string), typeof(GameObject)})]
    public class PlaySoundPatchString
    {
        public static bool Prefix(ref GameAudioManager __instance, ref string soundID, ref GameObject go)
        {
            //MelonLogger.Msg("Play string " + soundID + " on " + go.name);

            if (PatchMaster.PatchAction(soundID, go))
            {
                return false;
            }

            return true;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlaySound", new Type[] { typeof(uint), typeof(GameObject) })]
    public class PlaySoundPatchUInt
    {
        public static bool Prefix(ref GameAudioManager __instance, ref uint soundID, ref GameObject go)
        {
            //MelonLogger.Msg("Play uint " + EventIDs.GetEventString(soundID) + " on " + go.name);

            if (PatchMaster.PatchAction(EventIDs.GetEventString(soundID), go))
            {
                return false;
            }

            return true;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlaySound", new Type[] { typeof(Il2CppAK.Wwise.Event), typeof(GameObject) })]
    public class PlaySoundPatchEvent
    {
        public static bool Prefix(ref GameAudioManager __instance, ref Il2CppAK.Wwise.Event soundEvent, ref GameObject go)
        {
            //MelonLogger.Msg("Play event " + soundEvent.Name + " on " + go.name);

            if (PatchMaster.PatchAction(soundEvent.Name, go))
            {
                return false;
            }

            return true;
        }
    }
}
