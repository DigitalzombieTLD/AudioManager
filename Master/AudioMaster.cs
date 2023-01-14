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


        public enum SourceType { SFX, BGM, Voice, Ambience, Custom };

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



        /// <summary>
        /// Creates a new ClipManager instance for loading audioclips from files or bundles.
        /// </summary>
        /// <returns>New "ClipManager" class instance. Manage yourself</returns>
        public static ClipManager NewClipManager()
        {
            ClipManager newManager = new ClipManager();

            return newManager;
        }

        /// <summary>
        /// Creates a new AudioSource on targetobject. Used for single short audioclips.
        /// </summary>
        /// <returns>New "Shot" class instance. Manage yourself</returns>
        /// <param name="targetObject">Parentobject for new audiosource</param>
        /// <param name="sourceType">Enum AudioMaster.SourceType.* - Affects volume and 3d audio settings</param>
        /// 
        public static Shot CreateShot(GameObject targetObject, SourceType sourceType)
        {
            Shot newAudioSource = targetObject.AddComponent<Shot>();
            newAudioSource.Setup(sourceType);

            return newAudioSource;
        }

        /// <summary>
        /// Creates a new AudioSource on the player. Used for single short audioclips that emit from the player (eg. voice)
        /// </summary>
        /// <returns>New "Shot" class instance. Manage yourself</returns>
        /// <param name="sourceType">Enum AudioMaster.SourceType.* - Affects volume and 3d audio settings</param>
        /// 
        public static Shot CreatePlayerShot(SourceType sourceType)
        {
            Shot newAudioSource = _masterParent.AddComponent<Shot>();
            newAudioSource.Setup(sourceType);

            return newAudioSource;
        }

        /// <summary>
        /// Creates a new AudioSource on targetobject. Used for playing a list of audioclips.
        /// </summary>
        /// <returns>New "Queue" class instance. Manage yourself</returns>
        /// <param name="targetObject">Parentobject for new audiosource</param>
        /// <param name="clipManager">ClipManager that acts as the playlist</param>
        /// <param name="timeGap">Pause between clips. Use 0f for gapless playback</param>
        /// <param name="loopType">Enum Queue.Loop.* - Loop single clip / Loop complete list / Randomize play order (never stop)</param>
        /// <param name="sourceType">Enum AudioMaster.SourceType.* - Affects volume and 3d audio settings</param>
        /// 
        public static Queue CreateQueue(GameObject targetObject, ClipManager clipManager, float timeGap, Queue.Loop loopType, SourceType sourceType)
        {
            Queue newAudioSource = targetObject.AddComponent<Queue>();
            newAudioSource.Setup(clipManager, timeGap, loopType, sourceType);

            return newAudioSource;
        }

        /// <summary>
        /// Creates a new AudioSource on the player. Used for playing a list of audioclips.
        /// </summary>
        /// <returns>New "Queue" class instance. Manage yourself</returns>
        /// <param name="clipManager">ClipManager that acts as the playlist</param>
        /// <param name="timeGap">Pause between clips. Use 0f for gapless playback</param>
        /// <param name="loopType">Enum Queue.Loop.* - Loop single clip / Loop complete list / Randomize play order (never stop)</param>
        /// <param name="sourceType">Enum AudioMaster.SourceType.* - Affects volume and 3d audio settings</param>
        /// 
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


        /// <summary>
        /// Removes any Queue on the player with the source type BGM.
        /// </summary>
        /// 
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
