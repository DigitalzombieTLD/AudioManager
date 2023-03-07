using MelonLoader;
using Il2CppInterop.Runtime.Injection;
using Il2Cpp;
using UnityEngine;
using UnityEngine.Rendering;

namespace AudioMgr
{
    public class AudioMain : MelonMod
	{
        bool initialized = false;
        ClipManager myClipManager;
        Shot myPlayerShot;

        public override void OnInitializeMelon() 
		{
            ClassInjector.RegisterTypeInIl2Cpp<Shot>();
            ClassInjector.RegisterTypeInIl2Cpp<Queue>();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName.Contains("Boot"))
            {
                AudioMaster.CreateMasterParent();
                myClipManager = new ClipManager();

                myClipManager.LoadClipFromFile("start", "start.mp3", ClipManager.LoadType.Decompressed);
                myClipManager.LoadClipFromFile("shutdown", "shutdown.mp3", ClipManager.LoadType.Decompressed);
            }

            if (sceneName.Contains("Menu"))
            {
                initialized = true;
               

                myPlayerShot = AudioMaster.CreatePlayerShot(AudioMaster.SourceType.Ambience);


                PatchMaster.AddReplacePatch("PLAY_SNDMECHDOORWOODOPEN1", myClipManager, "start", AudioMaster.SourceType.SFX);
                PatchMaster.AddReplacePatch("PLAY_SNDMECHDOORWOODCLOSE1", myClipManager, "shutdown", AudioMaster.SourceType.SFX);

              
            }
        }

        public override void OnFixedUpdate()
        {
            if (initialized)
            {
                AudioMaster.MoveMasterToPlayer();
            }
        }

        public override void OnUpdate()
        {
            /*
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.Keypad0))
            {
                // myPlayerShot.PlayOneshot(myClipManager.GetClip("waterfall"));
                PatchMaster.AddSkipPatch("PLAY_RANDOMBUILDINGCREAKS1");
                PatchMaster.AddParameterPatch("WINDGUSTINTENSITY", 0f, PatchMaster.ParameterType.Limitter);
                PatchMaster.AddParameterPatch("WINDACTUALSPEED", 0f, PatchMaster.ParameterType.Limitter); 
                PatchMaster.AddParameterPatch("AMBIENTVOLUME", 0f, PatchMaster.ParameterType.Limitter);
                PatchMaster.AddParameterPatch("GLOBALVOLUME", 0f, PatchMaster.ParameterType.Limitter);
            }          
            */
        }
    }
}
