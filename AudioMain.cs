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

        public static bool _debug = false;

        public override void OnInitializeMelon() 
		{
            RadioMaster.Initialize();
            Settings.OnLoad();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName.Contains("Boot"))
            {
                AudioMaster.CreateMasterParent();
            }

            if (sceneName.Contains("Menu"))
            {
                initialized = true;
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
        }
    }
}
