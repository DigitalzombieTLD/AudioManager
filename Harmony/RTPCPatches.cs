using Il2Cpp;
using Il2CppAudio.SimpleAudio;
using MelonLoader;
using UnityEngine;

namespace AudioMgr
{      
    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "SetRTPCValue", new Type[] { typeof(uint), typeof(float), typeof(GameObject) })]
    public class RTPCPatches
    {
        public static void Prefix(GameAudioManager __instance, ref uint rtpcID, ref float rtpcValue, ref GameObject go)
        {
            // GAME_PARAMETERS
            //MelonLogger.Msg("RTPC " + GameParameterIDs.GetString(rtpcID) + "; " + rtpcValue);

            if (VolumeIDs.GetRtpcIDMaster() == rtpcID)
            {
                VolumeMaster.SetMasterVolume(rtpcValue / 100);

            }
            else if (VolumeIDs.GetRtpcIDList().ContainsKey(rtpcID)) // Set sfx/voice/ambient/bgm
            {
                VolumeMaster.SetVolume(VolumeIDs.GetRtpcIDList()[rtpcID], rtpcValue / 100);
            }

            rtpcValue = PatchMaster.ParameterAction(GameParameterIDs.GetString(rtpcID), rtpcValue);           
        }
    }
}
