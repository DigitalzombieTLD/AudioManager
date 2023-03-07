using Il2Cpp;
using Il2CppAK.Wwise;
using Il2CppTLD.Audio;
using MelonLoader;
using UnityEngine;

namespace AudioMgr
{
    [HarmonyLib.HarmonyPatch(typeof(AmbientEmitter), "Start")]
    public class AudioEmitterPatch
    {
        public static void Postfix(ref AmbientEmitter __instance)
        {
            //MelonLogger.Msg("Ambient emitter Start: " + __instance + " on gameobject " + __instance.gameObject.name);
        }
    }

   
}
