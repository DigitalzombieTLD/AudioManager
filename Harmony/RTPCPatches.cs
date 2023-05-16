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
            if (AudioMain._debug)
                MelonLogger.Msg("RTPC " + GameParameterIDs.GetString(rtpcID) + "; " + rtpcValue);


            if (go == null)
            {
                return;
            }

   
            // Aurora music patch
            if (Settings.options.enableAuroraTweaks && GameParameterIDs.GetString(rtpcID) == "AURORASTRENGTH") 
            {
                if (rtpcValue > Settings.options.auroraVolume) 
                {
                    rtpcValue = Settings.options.auroraVolume;                 
                }
            }
      
            // Wind audioc patch
            if (Settings.options.enableWindTweaks && GameManager.GetWeatherComponent().IsIndoorScene() && (GameParameterIDs.GetString(rtpcID) == "WINDINTENSITYBLEND"))
            {            
                if (rtpcValue > Settings.options.windVolume)
                {
               
                    rtpcValue = Settings.options.windVolume;
                }
            }

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
