using MelonLoader;
using Il2CppInterop.Runtime.Injection;
using Il2Cpp;
using UnityEngine;
using UnityEngine.Rendering;
using Il2CppInterop.Runtime.Attributes;
using System.Collections;


namespace AudioMgr
{
    public class PlayAndFade
	{
        Shot _assignedShot;
        Clip _assignedClip;
        double _fadeDuration;
        double _clipDuration;
        double _startTime;
        double _stopTime;
        double _timeElapsed;
        float _startVolume;

        object _timerLoop;
        bool _isPlaying;

        public PlayAndFade(Shot targetShot, Clip audioClip, double fadeDuration)
        {
            _assignedShot = targetShot;
            _assignedClip = audioClip;
            _fadeDuration = fadeDuration;
            
            _startTime = AudioSettings.dspTime + 0.5;

            _assignedShot.AssignClip(_assignedClip);
            _assignedShot._audioSource.PlayScheduled(_startTime);
            _clipDuration = _assignedClip.clipLength;
            _startVolume = _assignedShot._audioSource.volume;
            _stopTime = _startTime + _clipDuration - _fadeDuration;

            _isPlaying = true;

            _timerLoop = MelonCoroutines.Start(TimerLoop());
        }

        [HideFromIl2Cpp]
        private IEnumerator TimerLoop()
        {           
            float time = 0;

            while (_isPlaying)
            {
                _timeElapsed = AudioSettings.dspTime - _startTime;

                if (_timeElapsed < _stopTime)
                {
                    yield return null;
                }
                else if (_timeElapsed > _stopTime)
                {
                    _assignedShot._audioSource.volume = Mathf.Lerp(_startVolume, 0, time / (float)_fadeDuration);
                    time += Time.deltaTime;
                }

                if(_assignedShot._audioSource.volume <= 0)
                {
                    _assignedShot.Stop();
                    _isPlaying = false;
                }
            }
            yield return null;
        }
    }
}
