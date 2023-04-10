using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace AudioMgr
{
    [HarmonyLib.HarmonyPatch(typeof(AkSoundEngine), "SetGameObjectOutputBusVolume", new Type[] {typeof(GameObject), typeof(GameObject), typeof(float) })]
    public class BusVolumePatch
    {
        public static void Prefix(ref AkSoundEngine __instance, ref GameObject in_emitterObjID, ref GameObject in_listenerObjID, ref float in_fControlValue)
        {
            if (AudioMain._debug)
                MelonLogger.Msg("AK emitter: " + in_emitterObjID.name + "; value: " + in_fControlValue);
        }
    }

   
}
