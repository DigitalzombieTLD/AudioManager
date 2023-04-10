using Il2Cpp;
using UnityEngine;

namespace AudioMgr
{    
    /*
    [HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "SetRTPCValue")]
    public class VolumeChanged
    {
        public static void Prefix(ref GameAudioManager __instance, ref uint rtpcID, ref float rtpcValue, ref GameObject go)
        {
            if (VolumeIDs.GetRtpcIDMaster() == rtpcID)
            {
                VolumeMaster.SetMasterVolume(rtpcValue/100);
                
            }            
            else if (VolumeIDs.GetRtpcIDList().ContainsKey(rtpcID)) // Set sfx/voice/ambient/bgm
            {
                VolumeMaster.SetVolume(VolumeIDs.GetRtpcIDList()[rtpcID], rtpcValue/100);
            }           
        }
    }    */
}
