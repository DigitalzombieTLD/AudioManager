using Il2CppInterop.Runtime.Attributes;
using MelonLoader;
using System.Collections;
using UnityEngine;

namespace AudioMgr
{
    public class Queue : MonoBehaviour
    {
        public Queue(IntPtr intPtr) : base(intPtr)
        {
        }

        private AudioSource _audioSource;
        
        private Setting _activeSetting;
        private AudioMaster.SourceType _sourceType;

        private ClipManager _assignedClipManager;
        private float _timeGap = 2f;
        private float _lowerRandomGap = 0;
        private float _upperRandomGap = 2;
        private bool _randomGap = false;

        private int _activeClip = 0;
        private bool _isSetup = false;
        private float _silenceTimer = 0f;

        public enum Loop { None, Single, All, Randomize };
        private Loop _loop = Loop.All;

        public enum PlayState { Stopped, Playing, Paused };
        private PlayState _playState = PlayState.Stopped;


        [HideFromIl2Cpp]
        public void Setup(ClipManager assignedClipManager, float timeGap, Loop loopType, AudioMaster.SourceType sourceType)
        {
            _assignedClipManager = assignedClipManager;
            _sourceType = sourceType;
            _timeGap = timeGap;
            _loop = loopType;

            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.volume = VolumeMaster.GetVolume(sourceType);

            VolumeMaster.onVolumeChange += ResetVolume;

            ApplySettings(SettingMaster.Defaults(sourceType));
            _isSetup = true;
        }

        [HideFromIl2Cpp]
        public void Setup(ClipManager assignedClipManager, float timeGap, Loop loopType, Setting sourceSetting)
        {
            _assignedClipManager = assignedClipManager;
            _timeGap = timeGap;
            _loop = loopType;
            _sourceType = AudioMaster.SourceType.Custom;
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.volume = VolumeMaster.GetVolume(sourceType);

            VolumeMaster.onVolumeChange += ResetVolume;

            ApplySettings(sourceSetting);
            _isSetup = true;
        }

       

        [HideFromIl2Cpp]
        public void SetRandomTimeGap(float lowestTimeGap, float highestTimeGap)
        {
            _randomGap = true;
            _lowerRandomGap = lowestTimeGap;
            _upperRandomGap = highestTimeGap;  
        }

        public void SetFixedTimeGap(float timeGap)
        {
            _randomGap = false;
            _timeGap = timeGap;
        }

        [HideFromIl2Cpp]
        public void Play()
        {
            if(_assignedClipManager.clipCount == 0 || _isSetup == false)
            {
                return;
            }

            if (_playState == PlayState.Stopped)
            {
                _playState = PlayState.Playing;
                _audioSource.clip = _assignedClipManager.GetClipAtIndex(_activeClip).audioClip;
                _audioSource.PlayDelayed(0.6f);
            }
            else if (_playState == PlayState.Paused)
            {
                UnPause();
            }
            else if(_playState == PlayState.Playing)
            {                
  
                Clip nextClip = GetNextClip();

                if(_audioSource.clip != nextClip.audioClip)
                {
                    _audioSource.clip = nextClip.audioClip;
                }

                if (_playState == PlayState.Playing)
                {
                    _audioSource.PlayDelayed(0.5f + _timeGap);
                }                
            }
        }

        public void Update()
        {
            if (_playState == PlayState.Playing)
            {
                _silenceTimer += Time.deltaTime;

                if (_silenceTimer >= 1f)
                {
                    if (!_audioSource.isPlaying)
                    {
                        Play();
                    }
                    _silenceTimer = 0f;
                }
            }
        }

        [HideFromIl2Cpp]
        public void Stop()
        {
            if (_playState == PlayState.Playing)
            {                
                _playState = PlayState.Stopped;
                _audioSource.Stop();
            }
        }

        [HideFromIl2Cpp]
        public void UnPause()
        {
            if (_playState == PlayState.Paused)
            {
                _playState = PlayState.Playing;
                _audioSource.UnPause();
            }
        }

        [HideFromIl2Cpp]
        public void Pause()
        {
            if (_playState == PlayState.Playing)
            {
                _playState = PlayState.Paused;
                _audioSource.Pause();
            }
        }

        [HideFromIl2Cpp]
        public Clip GetNextClip()
        {
            if (_loop == Loop.None) // Keep current clip
            {                
                Stop();
            }
            else if (_loop == Loop.Single) // Keep current clip
            {
                _activeClip = _activeClip; // duh
            }
            else if (_loop == Loop.Randomize)
            {
                int randomIndex = _activeClip;

                if(_assignedClipManager.clipCount != 1)
                {
                    while (_activeClip == randomIndex)
                    {
                        randomIndex = UnityEngine.Random.Range(0, _assignedClipManager.clipCount);
                    }
                }               

                _activeClip = randomIndex;              
            }
            else if (_loop == Loop.All) // _loop == Loop.All
            {
                if (_activeClip < _assignedClipManager.clipCount - 1) // Not at the end yet, increase by 1
                {
                    _activeClip++;
                }
                else if (_activeClip >= _assignedClipManager.clipCount - 1)
                {                    
                    _activeClip = 0;                 
                }
            }

            if (_randomGap)
            {
                _timeGap = UnityEngine.Random.Range(_lowerRandomGap, _upperRandomGap);          
            }
            return _assignedClipManager.GetClipAtIndex(_activeClip);
        }
        
        [HideFromIl2Cpp]
        public void AssignClipManager(ClipManager assignedClipManager, float timeGap, Loop loopType)
        {
            Stop();
            _assignedClipManager = assignedClipManager;
            _timeGap = timeGap;
            _loop = loopType;
            _activeClip = 0;
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
            if(_audioSource &&  _sourceType != AudioMaster.SourceType.Custom)
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
        public Loop loop
        {
            get
            {
                return _loop;
            }
            set
            {
                _loop = value;
            }
        }

        [HideFromIl2Cpp]
        public float timeGap
        {
            get
            {
                return _timeGap;
            }
            set
            {
                _timeGap = value;
            }
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
        
       
    }
}
