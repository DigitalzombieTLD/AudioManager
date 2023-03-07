using Il2Cpp;
using Il2CppInterop.Runtime.Attributes;
using MelonLoader;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace AudioMgr
{
    public class Shot : MonoBehaviour
    {
        public Shot(IntPtr intPtr) : base(intPtr)
        {
        }

        private AudioSource _audioSource;
        private Setting _activeSetting;

        private bool _isEnabled = false;
       
        private AudioMaster.SourceType _sourceType;
        public enum PlayState { Stopped, Playing, Paused };
        private PlayState _playState = PlayState.Stopped;

        [HideFromIl2Cpp]
        public void Setup(AudioMaster.SourceType sourceType)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.volume = VolumeMaster.GetVolume(sourceType);
            VolumeMaster.onVolumeChange += ResetVolume;
            ApplySettings(SettingMaster.Defaults(sourceType));
        }

        [HideFromIl2Cpp]
        private void OnEnable()
        {
            VolumeMaster.onVolumeChange += ResetVolume;
        }

        [HideFromIl2Cpp]
        private void OnDisable()
        {
            Stop();
            VolumeMaster.onVolumeChange -= ResetVolume;
        }

        [HideFromIl2Cpp]
        private void OnDestroy()
        {
            VolumeMaster.onVolumeChange -= ResetVolume;
        }

        [HideFromIl2Cpp]
        public void SetVolume(float newVolume)
        {
            if (_sourceType == AudioMaster.SourceType.Custom)
            {
                _audioSource.volume = newVolume;
            }
        }

        [HideFromIl2Cpp]
        public void AssignClip(Clip audioClip)
        {
            Stop();
            _audioSource.clip = audioClip.audioClip;            
        }

        [HideFromIl2Cpp]
        public void Disable()
        {                     
            OnDisable();
        }

        [HideFromIl2Cpp]
        public void Enable()
        {
            OnEnable();
        }

        [HideFromIl2Cpp]
        public void Play(Clip audioClip)
        {
            Stop();
            _playState = PlayState.Playing;
            MelonCoroutines.Start(PlayRoutine(audioClip));
        }

        private IEnumerator PlayRoutine(Clip audioClip)
        {
            double _startTime = AudioSettings.dspTime + 0.6;
            double _endTime = _startTime + audioClip.clipLength + 0.6;

            _audioSource.PlayScheduled(_startTime);
            _playState = PlayState.Playing;

            while (AudioSettings.dspTime < _endTime)
            {
                yield return null;
            }

            Stop();
        }      

        [HideFromIl2Cpp]
        public void Stop()
        {
            _playState = PlayState.Stopped;
            _audioSource.Stop();
        }

        [HideFromIl2Cpp]
        public void ResetVolume()
        {
            if(_sourceType != AudioMaster.SourceType.Custom)
            {
                _audioSource.volume = VolumeMaster.GetVolume(_sourceType);
            }            
        }

        [HideFromIl2Cpp]
        public void PlayOneshot(Clip audioClip)
        {           
            MelonCoroutines.Start(PlayOneshotRoutine(audioClip));
        }

        [HideFromIl2Cpp]
        private IEnumerator PlayOneshotRoutine(Clip audioClip)
        {     
            _audioSource.PlayOneShot(audioClip.audioClip);
            yield return null;
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
            //_audioSource.rolloffFactor = _activeSetting.rolloffFactor;
            _audioSource.rolloffMode = _activeSetting.rolloffMode;
            _audioSource.priority = _activeSetting.priority;

            ResetVolume();
        }

        [HideFromIl2Cpp]
        public AudioMaster.SourceType sourceType
        {
            get
            {
                return _sourceType;
            }
        }
    }
}
