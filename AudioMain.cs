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
        ClipManager myClipManager, myClipManager2;
        Shot myPlayerShot;
        string rootPath = Application.dataPath + "/../Mods/";
        //AssetBundle bundle;
        public static bool _debug = false;

        public override void OnInitializeMelon() 
		{
            ClassInjector.RegisterTypeInIl2Cpp<Shot>();
            ClassInjector.RegisterTypeInIl2Cpp<Queue>();

            AudioMgr.Settings.OnLoad();
            //bundle = AssetBundle.LoadFromFile(Application.dataPath + "/../Mods/sillysounds.unity3d");
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName.Contains("Boot"))
            {
                AudioMaster.CreateMasterParent();
                RadioMaster.Initialize();
                
                //myClipManager = new ClipManager();
                //myClipManager2 = new ClipManager();

                //myClipManager.LoadClipFromFile("start", "start.mp3", ClipManager.LoadType.Decompressed);
                //myClipManager.LoadClipFromFile("shutdown", "shutdown.mp3", ClipManager.LoadType.Decompressed);
                //myClipManager.LoadClipFromFile("waterfall", "waterfall.ogg", ClipManager.LoadType.Decompressed);

                //myClipManager2.LoadAllClipsFromBundle(bundle);


            }

            if (sceneName.Contains("Menu"))
            {
                initialized = true;
               

               //myPlayerShot = AudioMaster.CreatePlayerShot(AudioMaster.SourceType.SFX);


                //PatchMaster.AddReplacePatch("PLAY_CROWCAWSDISTANT", myClipManager, "start", AudioMaster.SourceType.SFX);
                //PatchMaster.AddReplacePatch("PLAY_SNDMECHDOORWOODCLOSE1", myClipManager, "shutdown", AudioMaster.SourceType.SFX);

              
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
            
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.Keypad0))
            {
                // myPlayerShot.PlayOneshot(myClipManager.GetClip("waterfall"));
                PatchMaster.AddSkipPatch("PLAY_RANDOMBUILDINGCREAKS1");
                PatchMaster.AddParameterPatch("WINDGUSTINTENSITY", 0f, PatchMaster.ParameterType.Limitter);
                PatchMaster.AddParameterPatch("WINDACTUALSPEED", 0f, PatchMaster.ParameterType.Limitter); 
                PatchMaster.AddParameterPatch("AMBIENTVOLUME", 0f, PatchMaster.ParameterType.Limitter);
                PatchMaster.AddParameterPatch("GLOBALVOLUME", 0f, PatchMaster.ParameterType.Limitter);
            }
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.L))
            {
                //myPlayerShot.PlayOneshot(myClipManager2.GetClip("woo"));
            }

            }
        }
}
