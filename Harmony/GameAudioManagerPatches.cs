using Il2Cpp;
using MelonLoader;
using UnityEngine;


namespace AudioMgr
{
    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlayMusic", new Type[] {typeof(string), typeof(GameObject) })]
    public class GameAudio1
    {
        public static void Prefix(ref GameAudioManager __instance, ref string soundID, ref GameObject go)
        {
            if (AudioMain._debug2)
            {
                MelonLogger.Msg("[GAM] [PlayMusic] [STR] " + soundID + "; " + go.name);
            }                
        }
    }


    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlayMusic", new Type[] { typeof(uint), typeof(GameObject) })]
    public class GameAudio2
    {
        public static void Prefix(ref GameAudioManager __instance, ref uint soundID, ref GameObject go)
        {
            if (AudioMain._debug2)
            {
                MelonLogger.Msg("[GAM] [PlayMusic] [uINT]" + EventIDs.GetEventString(soundID) + "; " + go.name);
            }
        }
    }


    //PlaySoundWithPositionTracking(AK.Wwise.Event soundEvent, GameObject go, AkCallbackManager.EventCallback eventCallback, GameAudioManager.PlayOptions playOptions)
    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlaySoundWithPositionTracking", new Type[] { typeof(Il2CppAK.Wwise.Event), typeof(GameObject), typeof(AkCallbackManager.EventCallback), typeof(GameAudioManager.PlayOptions) })]
    public class GameAudio3
    {
        public static void Prefix(ref GameAudioManager __instance, ref Il2CppAK.Wwise.Event soundEvent, ref GameObject go, ref AkCallbackManager.EventCallback eventCallback, ref GameAudioManager.PlayOptions playOptions)
        {
            if (AudioMain._debug2)
            {
                MelonLogger.Msg("[GAM] [PlaySoundWithPositionTracking] [EVENT] " + EventIDs.GetEventString(soundEvent.m_playingId) + "; " + go.name + "; " + eventCallback.ToString() + "; " + playOptions.ToString());
            }
        }
    }


    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlaySoundWithPositionTracking", new Type[] { typeof(string), typeof(GameObject), typeof(AkCallbackManager.EventCallback), typeof(GameAudioManager.PlayOptions) })]
    public class GameAudio4
    {
        public static void Prefix(ref GameAudioManager __instance, ref string soundID, ref GameObject go, ref AkCallbackManager.EventCallback eventCallback, ref GameAudioManager.PlayOptions playOptions)
        {
            if (AudioMain._debug2)
            {
                MelonLogger.Msg("[GAM] [PlaySoundWithPositionTracking] [STR] " + soundID + "; " + go.name + "; " + eventCallback.ToString() + "; " + playOptions.ToString());
            }
        }
    }


    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlaySound", new Type[] { typeof(Il2CppAK.Wwise.Event), typeof(GameObject) })]
    public class GameAudio5
    {
        public static void Prefix(ref GameAudioManager __instance, ref Il2CppAK.Wwise.Event soundEvent, ref GameObject go)
        {
            if (AudioMain._debug2)
            {
                MelonLogger.Msg("[GAM] [PlaySound] [EVENTx] " + soundEvent.Name + "; " + go.name);
            }
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlaySound", new Type[] { typeof(string), typeof(GameObject) })]
    public class GameAudio6
    {
        public static void Prefix(ref GameAudioManager __instance, ref string soundID, ref GameObject go)
        {
            if (AudioMain._debug2)
            {
                MelonLogger.Msg("[GAM] [PlaySound] [STR] " + soundID + "; " + go.name);
            }
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlaySound", new Type[] { typeof(uint), typeof(GameObject) })]
    public class GameAudio7
    {
        public static void Prefix(ref GameAudioManager __instance, ref uint soundID, ref GameObject go)
        {
            if (AudioMain._debug2)
            {
                MelonLogger.Msg("[GAM] [PlaySound] [uINT] " + EventIDs.GetEventString(soundID) + "; " + go.name);
            }
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "Play3DSound", new Type[] { typeof(Il2CppAK.Wwise.Event), typeof(GameObject) })]
    public class GameAudio8
    {
        public static void Prefix(ref GameAudioManager __instance, ref Il2CppAK.Wwise.Event soundEvent, ref GameObject go)
        {
            if (AudioMain._debug2)
            {
                MelonLogger.Msg("[GAM] [Play3DSound] [EVENTx] " + EventIDs.GetEventString(soundEvent.m_playingId) + "; " + go.name);
            }
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "Play3DSound", new Type[] { typeof(string), typeof(GameObject) })]
    public class GameAudio9
    {
        public static void Prefix(ref GameAudioManager __instance, ref string soundID, ref GameObject go)
        {
            if (AudioMain._debug2)
            {
                MelonLogger.Msg("[GAM] [Play3DSound] [STR] " + soundID + "; " + go.name);
            }
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "Play3DSound", new Type[] { typeof(uint), typeof(GameObject) })]
    public class GameAudio10
    {
        public static void Prefix(ref GameAudioManager __instance, ref uint soundID, ref GameObject go)
        {
            if (AudioMain._debug2)
            {
                MelonLogger.Msg("[GAM] [Play3DSound] [uINT] " + EventIDs.GetEventString(soundID) + "; " + go.name);
            }
        }
    }



}
