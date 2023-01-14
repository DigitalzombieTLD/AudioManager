using MelonLoader;
using Il2CppInterop.Runtime.Injection;

namespace AudioMgr
{
    public class AudioMain : MelonMod
	{
        bool initialized = false;           

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
	}
}
