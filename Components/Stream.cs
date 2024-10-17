using Il2Cpp;
using Il2CppInterop.Runtime.Attributes;
using MelonLoader;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace AudioMgr
{
    [RegisterTypeInIl2Cpp]
    public class Stream : MonoBehaviour
    {
        public Stream(IntPtr intPtr) : base(intPtr)
        {
        }

        private AudioSource _audioSource;
        
        private Setting _activeSetting;
        private AudioMaster.SourceType _sourceType;
        private bool _isSetup;
        private string _streamURL;
        //private AudioClip _streamClip = null;
        private UnityWebRequest _www;

        public enum PlayState { Stopped, Playing };
        private PlayState _playState = PlayState.Stopped;


        [HideFromIl2Cpp]
        public void Setup(string streamURL, AudioMaster.SourceType sourceType)
        {
            _streamURL = streamURL;
            _sourceType = sourceType;
          
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.volume = VolumeMaster.GetVolume(sourceType);

            VolumeMaster.onVolumeChange += ResetVolume;

            ApplySettings(SettingMaster.Defaults(sourceType));
            _isSetup = true;
        }

        [HideFromIl2Cpp]
        public void Setup(string streamURL, Setting sourceSetting)
        {
            _streamURL = streamURL;
            _sourceType = sourceType;
            
            _sourceType = AudioMaster.SourceType.Custom;
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.volume = VolumeMaster.GetVolume(sourceType);

            VolumeMaster.onVolumeChange += ResetVolume;

            ApplySettings(sourceSetting);
            _isSetup = true;
        }

        
        private IEnumerator PlayAudioRoutine()
        {
            /*
            UnityWebRequest www;
            WWW foowww = new WWW(_streamURL);
            www = UnityWebRequest.Get(_streamURL);
            www.SendWebRequest();

            MelonLogger.Msg("start ruotig ");

        

            if (!www.isNetworkError && !www.isHttpError)
            {
                MelonLogger.Msg("now create");
               
                
               _audioSource.clip = WebRequestWWW.InternalCreateAudioClipUsingDH(www.downloadHandler, www.url, true, true, AudioType.UNKNOWN);

                yield return new WaitForSeconds(10f);

                MelonLogger.Msg("now play ");
                _audioSource.PlayDelayed(1f);
                _playState = PlayState.Playing;
            }
            else
            {
                _playState = PlayState.Stopped;
                MelonLogger.Msg("Error while loading audioclip. Skipping " + www.error);
            }
                      
            while (_playState == PlayState.Playing)
            {
                MelonLogger.Msg("in while ");
                yield return null;
            }

            MelonLogger.Msg("now stop ");
            Stop();
            
            */
            yield return null;
        }
        

       

        [HideFromIl2Cpp]
        public void Play()
        {          
            if (_playState == PlayState.Stopped && _isSetup == true)
            {
                _playState = PlayState.Playing;
                MelonCoroutines.Start(PlayAudioRoutine());             

            }
        }


        [HideFromIl2Cpp]
        public void Stop()
        {
            _playState = PlayState.Stopped;
            _audioSource.Stop();

            _www?.Dispose();

         
        }
        

        [HideFromIl2Cpp]
        private void OnEnable()
        {
            VolumeMaster.onVolumeChange += ResetVolume;         
        }

        [HideFromIl2Cpp]
        private void OnDisable()
        {
            VolumeMaster.onVolumeChange -= ResetVolume;
        }

        [HideFromIl2Cpp]
        private void OnDestroy()
        {
            VolumeMaster.onVolumeChange -= ResetVolume;
        }

        [HideFromIl2Cpp]
        private void ResetVolume()
        {
            if(_sourceType != AudioMaster.SourceType.Custom)
            {
                _audioSource.volume = VolumeMaster.GetVolume(_sourceType);
            }            
        }

        [HideFromIl2Cpp]
        public void ApplySettings(Setting newSetting)
        {
            _activeSetting = newSetting;

            _sourceType = _activeSetting.sourceType;
            _audioSource.spread = _activeSetting.spread;
            _audioSource.panStereo = _activeSetting.panStereo;
            _audioSource.dopplerLevel = _activeSetting.dopplerLevel;
            _audioSource.maxDistance = _activeSetting.maxDistance;
            _audioSource.minDistance = _activeSetting.minDistance;
            _audioSource.pitch = _activeSetting.pitch;
            _audioSource.spatialBlend = _activeSetting.spatialBlend;
            _audioSource.spatialize = _activeSetting.spatialize;
            _audioSource.rolloffFactor = _activeSetting.rolloffFactor;
            _audioSource.maxVolume = _activeSetting.maxVolume;
            _audioSource.maxVolume = _activeSetting.minVolume;
            _audioSource.rolloffMode = _activeSetting.rolloffMode;
            //_audioSources[audioSource].SetCustomCurve(AudioSourceCurveType.CustomRolloff, _activeSetting.rollOffCurve);

            ResetVolume();
        }

      

        [HideFromIl2Cpp]
        public PlayState playState
        {
            get
            {
                return _playState;
            }
        }

        [HideFromIl2Cpp]
        public AudioMaster.SourceType sourceType
        {
            get
            {
                return _sourceType;
            }
            set
            {
                _sourceType = value;
                ApplySettings(SettingMaster.Defaults(_sourceType));
            }
        }

        [HideFromIl2Cpp]
        public string streamURL
        {
            get
            {
                return _streamURL;
            }
            set
            {
                _streamURL = value;              
            }
        }

    }
}
