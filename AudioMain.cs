using MelonLoader;
using Il2CppInterop.Runtime.Injection;
using Il2Cpp;
using UnityEngine;
using UnityEngine.Rendering;
using HarmonyLib;


namespace AudioMgr
{
    public class AudioMain : MelonMod
	{
        bool initialized = false;
        string rootPath = Application.dataPath + "/../Mods/";

        public static bool _debug = false;
        public static bool _debug2 = false;
               
        public override void OnInitializeMelon() 
		{
            if (File.Exists(rootPath + "AMdebug"))
            {
                _debug = true;
                _debug2 = true;
            }

            RadioMaster.Initialize();
            Settings.OnLoad();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName.Contains("Boot"))
            {
               
            }

            if (sceneName.Contains("Menu"))
            {
                AudioMaster.CreateMasterParent();
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
