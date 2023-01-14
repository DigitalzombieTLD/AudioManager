using Il2Cpp;
using UnityEngine;

namespace AudioMgr
{
    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlayMusic", new Type[] {typeof(string), typeof(GameObject)})]
    public class PlayMusicStringPatch
    {
        public static bool Prefix(ref GameAudioManager __instance, string soundID, GameObject go)
        {
            if (AudioMaster.vanillaMusicDisabled)
            {
                return false;                
            }
            return true;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlayMusic", new Type[] {typeof(uint), typeof(GameObject)})]
    public class PlayMusicUIntPatch
    {
        public static bool Prefix(ref GameAudioManager __instance, uint soundID, GameObject go)
        {
            if (AudioMaster.vanillaMusicDisabled)
            {
                return false;
            }
            return true;
        }
    }
}
