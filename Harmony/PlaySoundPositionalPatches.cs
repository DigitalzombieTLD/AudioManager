using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace AudioMgr
{    
    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlaySoundWithPositionTracking", new Type[] { typeof(string), typeof(GameObject), typeof(AkCallbackManager.EventCallback), typeof(GameAudioManager.PlayOptions) })]
    public class PlaySoundPositionalPatchString
    {
        public static void Prefix(ref GameAudioManager __instance, string soundID, GameObject go, AkCallbackManager.EventCallback eventCallback, GameAudioManager.PlayOptions playOptions)
        {
            if (AudioMain._debug)
                MelonLogger.Msg("Play positional string " + soundID + "; " + go.name);

        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlaySoundWithPositionTracking", new Type[] { typeof(Il2CppAK.Wwise.Event), typeof(GameObject), typeof(AkCallbackManager.EventCallback), typeof(GameAudioManager.PlayOptions) })]
    public class PlaySoundPositionalPatchEvent
    {
        public static void Prefix(ref GameAudioManager __instance, Il2CppAK.Wwise.Event soundEvent, GameObject go, AkCallbackManager.EventCallback eventCallback, GameAudioManager.PlayOptions playOptions)
        {
            if (AudioMain._debug)
                MelonLogger.Msg("Play positional event " + EventIDs.GetEventString(soundEvent.PlayingId) + "; " + go.name);
        }
    }
}
