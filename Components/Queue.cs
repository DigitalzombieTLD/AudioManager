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

        private Dictionary<bool, AudioSource> _audioSources;
        private bool _toggleAudioSource = true;

        private Setting _activeSetting;
        private AudioMaster.SourceType _sourceType;

        private ClipManager _assignedClipManager;
        private float _timeGap = 2f;
        private float _lowerRandomGap = 0;
        private float _upperRandomGap = 2;
        private bool _randomGap = false;

        private int _currentClipIndex = 0;

        private object _timerLoop;

        public enum Loop { None, Single, All, Randomize };
        private Loop _loop = Loop.All;

        public enum PlayState { Stopped, Playing, Paused };
        private PlayState _playState = PlayState.Stopped;


        [HideFromIl2Cpp]
        public void Setup(ClipManager assignedClipManager, float timeGap, Loop loopType, AudioMaster.SourceType sourceType)
        {
            _assignedClipManager = assignedClipManager;
            _timeGap = timeGap;
            _loop = loopType;
            _audioSources = new Dictionary<bool, AudioSource>();
            _audioSources.Add(true, gameObject.AddComponent<AudioSource>());
            _audioSources.Add(false, gameObject.AddComponent<AudioSource>());

            _audioSources[true].playOnAwake = false;
            _audioSources[false].playOnAwake = false;

            _audioSources[true].volume = VolumeMaster.GetVolume(sourceType);
            _audioSources[false].volume = VolumeMaster.GetVolume(sourceType);

            VolumeMaster.onVolumeChange += ResetVolume;

            ApplySettings(SettingMaster.Defaults(sourceType));
        }

        [HideFromIl2Cpp]
        public int GetNextClip()
        {
            if (_loop == Loop.Single) // Keep current clip
            {
                return _currentClipIndex;
            }
            else if (_loop == Loop.Randomize)
            {
                int randomIndex = _currentClipIndex;

                while (randomIndex == _currentClipIndex)
                {
                    randomIndex = UnityEngine.Random.Range(0, _assignedClipManager.clipCount - 1);
                }

                return randomIndex;
            }
            else if (_currentClipIndex < _assignedClipManager.clipCount - 1) // Not at the end yet, increase by 1
            {
                return _currentClipIndex + 1;
            }
            else if (_currentClipIndex >= _assignedClipManager.clipCount - 1)
            {
                if (_loop == Loop.All)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }

            return -1;
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
            if (_playState == PlayState.Stopped && _assignedClipManager.clipCount > 0)
            {
                _playState = PlayState.Playing;
                _timerLoop = MelonCoroutines.Start(TimerLoop());
            }
            else if (_playState == PlayState.Paused)
            {
                AudioListener.pause = false;
                _playState = PlayState.Playing;
            }
        }

        [HideFromIl2Cpp]
        public void Stop()
        {
            if (_timerLoop != null)
            {
                MelonCoroutines.Stop(_timerLoop);
                _playState = PlayState.Stopped;
                _audioSources[true].Stop();
                _audioSources[false].Stop();
            }
        }


        [HideFromIl2Cpp]
        private IEnumerator TimerLoop()
        {
            double _startTime;
            double _timeToNext = 0;
            int _nextClip = 0;
                      
            _startTime = AudioSettings.dspTime + 0.5;

            if (_randomGap)
            {
                _timeGap = UnityEngine.Random.Range(_lowerRandomGap, _upperRandomGap);
            }

            _audioSources[_toggleAudioSource].clip = _assignedClipManager.GetClipAtIndex(_currentClipIndex).audioClip;
            _audioSources[_toggleAudioSource].PlayScheduled(_startTime + _timeGap);

            _timeToNext = _startTime;

            while (true)
            {
                if (_playState == PlayState.Playing)
                {                    
                    // Assign new clip                
                    _nextClip = GetNextClip();

                    if (_nextClip < 0)
                    {
                        yield break;
                    }

                    if(_randomGap)
                    {
                        _timeGap = UnityEngine.Random.Range(_lowerRandomGap, _upperRandomGap);
                    }

                    _toggleAudioSource = !_toggleAudioSource;

                    _timeToNext = _timeToNext + _assignedClipManager.GetClipAtIndex(_currentClipIndex).clipLength + _timeGap;

                    _audioSources[_toggleAudioSource].clip = _assignedClipManager.GetClipAtIndex(_nextClip).audioClip;
                    _audioSources[_toggleAudioSource].PlayScheduled(_timeToNext);

                    while (AudioSettings.dspTime < _timeToNext + 0.05)
                    {
                        yield return null;
                    }                
                    _currentClipIndex = _nextClip;
                }
                yield return null;
            }            
        }

        [HideFromIl2Cpp]
        public void Settings(ClipManager assignedClipManager, float timeGap, Loop loopType)
        {
            _assignedClipManager = assignedClipManager;
            _timeGap = timeGap;
            _loop = loopType;
            _currentClipIndex = 0;
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
                _audioSources[_toggleAudioSource].volume = VolumeMaster.GetVolume(_sourceType);
                _audioSources[!_toggleAudioSource].volume = VolumeMaster.GetVolume(_sourceType);
            }            
        }

        [HideFromIl2Cpp]
        public void ApplySettings(Setting newSetting)
        {
            ApplySettingsToSingle(newSetting, true);
            ApplySettingsToSingle(newSetting, false);
        }

        [HideFromIl2Cpp]
        private void ApplySettingsToSingle(Setting newSetting, bool audioSource)
        {
            _activeSetting = newSetting;

            _sourceType = _activeSetting.sourceType;
            _audioSources[audioSource].spread = _activeSetting.spread;
            _audioSources[audioSource].panStereo = _activeSetting.panStereo;
            _audioSources[audioSource].dopplerLevel = _activeSetting.dopplerLevel;
            _audioSources[audioSource].maxDistance = _activeSetting.maxDistance;
            _audioSources[audioSource].minDistance = _activeSetting.minDistance;
            _audioSources[audioSource].pitch = _activeSetting.pitch;
            _audioSources[audioSource].spatialBlend = _activeSetting.spatialBlend;
            _audioSources[audioSource].spatialize = _activeSetting.spatialize;
            //_audioSources[audioSource].rolloffFactor = _activeSetting.rolloffFactor;
            _audioSources[audioSource].rolloffMode = _activeSetting.rolloffMode;
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
