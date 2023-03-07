using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace AudioMgr
{    
    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "Play3DSound", new Type[] { typeof(string), typeof(GameObject)})]
    public class Play3DSoundPatchString
    {
        public static bool Prefix(ref GameAudioManager __instance, ref string soundID, ref GameObject go)
        {
            //MelonLogger.Msg("Play3D string " + soundID + " on " + go.name);

            if (PatchMaster.PatchAction(soundID, go))
            {
                return false;
            }

            return true;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "Play3DSound", new Type[] { typeof(uint), typeof(GameObject) })]
    public class Play3DSoundPatchUInt
    {
        public static bool Prefix(ref GameAudioManager __instance, ref uint soundID, ref GameObject go)
        {
            //MelonLogger.Msg("Play3D uint " + EventIDs.GetEventString(soundID) + " on " + go.name);


            if (PatchMaster.PatchAction(EventIDs.GetEventString(soundID), go))
            {
                return false;
            }

            return true;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "Play3DSound", new Type[] { typeof(Il2CppAK.Wwise.Event), typeof(GameObject) })]
    public class Play3DSoundPatchEvent
    {
        public static void Prefix(ref GameAudioManager __instance, ref Il2CppAK.Wwise.Event soundEvent, ref GameObject go)
        {
            //MelonLogger.Msg("Play3D event " + soundEvent.Name + " on " + go.name);
        }
    }
}
