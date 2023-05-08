using Il2Cpp;
using MelonLoader;
using System.Collections;
using UnityEngine;

namespace AudioMgr
{
    public static class AudioMaster
    {
        private static AudioListener _playerListener;

        private static bool _playerGotBGMQueue = false;
        private static Queue _playerBGMQueue;   
        private static GameObject _masterParent;
        private static Panel_OptionsMenu _vanillaSoundOptionsMenu;
        private static bool _isVanillaMusicDisabled = false;


        public enum SourceType { SFX, BGM, Voice, Ambience, Custom, AuroraRadio };

        public static void MoveMasterToPlayer()
        {           
            if (GameManager.GetMainCamera() != null)
            {
                _masterParent.transform.position = GameManager.GetVpFPSCamera().gameObject.transform.position;
            }
        }

        public static void CreateMasterParent() 
        {
            if (_masterParent == null)
            {
                SettingMaster.Setup();
                _masterParent = new GameObject("AudioStalker");
                UnityEngine.GameObject.DontDestroyOnLoad(_masterParent);

                _playerListener = _masterParent.AddComponent<AudioListener>();
            }
        }

        private static void SearchAndDestroyListeners()
        {            
            AudioListener[] foundListeners = UnityEngine.Object.FindObjectsOfType<AudioListener>();

            foreach (AudioListener singleKill in foundListeners)
            {
                UnityEngine.Object.Destroy(singleKill);
            }
        }




        public static ClipManager NewClipManager()
        {
            ClipManager newManager = new ClipManager();

            return newManager;
        }

  
        public static Shot CreateShot(GameObject targetObject, SourceType sourceType)
        {
            Shot newAudioSource = targetObject.AddComponent<Shot>();
            newAudioSource.Setup(sourceType);

            return newAudioSource;
        }

        public static Shot CreateShot(GameObject targetObject, Setting sourceSetting)
        {
            Shot newAudioSource = targetObject.AddComponent<Shot>();
            newAudioSource.Setup(sourceSetting);

            return newAudioSource;
        }


        public static Shot CreatePlayerShot(SourceType sourceType)
        {
            Shot newAudioSource = _masterParent.AddComponent<Shot>();
            newAudioSource.Setup(sourceType);

            return newAudioSource;
        }

        public static Shot CreatePlayerShot(Setting sourceSetting)
        {
            Shot newAudioSource = _masterParent.AddComponent<Shot>();
            newAudioSource.Setup(sourceSetting);

            return newAudioSource;
        }


        public static Queue CreateQueue(GameObject targetObject, ClipManager clipManager, float timeGap, Queue.Loop loopType, SourceType sourceType)
        {
            Queue newAudioSource = targetObject.AddComponent<Queue>();
            newAudioSource.Setup(clipManager, timeGap, loopType, sourceType);

            return newAudioSource;
        }

        public static Queue CreateQueue(GameObject targetObject, ClipManager clipManager, float timeGap, Queue.Loop loopType, Setting sourceSetting)
        {
            Queue newAudioSource = targetObject.AddComponent<Queue>();
            newAudioSource.Setup(clipManager, timeGap, loopType, sourceSetting);

            return newAudioSource;
        }

        public static Queue CreatePlayerQueue(ClipManager clipManager, float timeGap, Queue.Loop loopType, SourceType sourceType)
        {
            if (_playerGotBGMQueue && sourceType == SourceType.BGM)
            {
                MelonLogger.Msg("Player already got a queue for BGM attached. Returning null");

                return null;
            }

            if (sourceType == SourceType.BGM)
            {
                _playerBGMQueue = _masterParent.AddComponent<Queue>();
                _playerBGMQueue.Setup(clipManager, timeGap, loopType, sourceType);
                _playerGotBGMQueue = true;

                return _playerBGMQueue;
            }

            Queue newAudioSource = _masterParent.AddComponent<Queue>();
            newAudioSource.Setup(clipManager, timeGap, loopType, sourceType);

            return newAudioSource;
        }

        public static Queue CreatePlayerQueue(ClipManager clipManager, float timeGap, Queue.Loop loopType, Setting sourceSetting)
        {
             Queue newAudioSource = _masterParent.AddComponent<Queue>();
            newAudioSource.Setup(clipManager, timeGap, loopType, sourceSetting);

            return newAudioSource;
        }


        public static void RemovePlayerBGMQueue()
        {
            if (_playerGotBGMQueue)
            {
                MelonLogger.Msg("Hostile takeover of BGM source on playerobject!");
               
                _playerGotBGMQueue = false;
                _playerBGMQueue = null;
            }
        }

        public static Panel_OptionsMenu vanillaAudioOptionsMenu
        {
            get
            {
                return _vanillaSoundOptionsMenu;
            }
            set
            {
                _vanillaSoundOptionsMenu = value;
            }
        }

        public static AudioListener audioListener
        {
            get
            {
                return _playerListener;
            }  
        }

        public static bool vanillaMusicDisabled
        {
            get
            {
                return _isVanillaMusicDisabled;
            }
            set
            {
                _isVanillaMusicDisabled = value;
            }
        }
    }
}
