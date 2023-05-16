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
            if (AudioMain._debug)
                MelonLogger.Msg("Play string " + soundID + " on " + go.name);

            if (Settings.options.disableFlare)
            {
                if (soundID.Contains("FlareLoop") || soundID.Contains("FLARELIGHT"))
                {
                    return false;
                }
            }

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
            if (AudioMain._debug)
                MelonLogger.Msg("Play uint " + EventIDs.GetEventString(soundID) + " on " + go.name);

            if(go == null)
            {
                return true;
            }

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
            if (AudioMain._debug)
                MelonLogger.Msg("Play event " + soundEvent.Name + " on " + go.name);



            if (go == null || soundEvent == null)
            {
                return true;
            }

            if (Settings.options.disableWaterfall && soundEvent.Name.Contains("Waterfall"))
            {
                return false;
            }

            if (PatchMaster.PatchAction(soundEvent.Name, go))
            {
                return false;
            }

            return true;
        }
    }
}
