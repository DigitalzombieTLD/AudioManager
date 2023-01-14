using UnityEngine;

namespace AudioMgr
{
    public class Clip
    {
        private AudioClip _audioClip;

        private string _clipName;
        private double _clipLength;
        private int _clipFreq;
        private int _clipSamples;

        public Clip(AudioClip clip, string clipName)
        {            
            _audioClip = clip;
            _audioClip.hideFlags = HideFlags.DontUnloadUnusedAsset;

            _clipName = clipName;

            _clipFreq = _audioClip.frequency;
            _clipSamples = _audioClip.samples;
            _clipLength = (double)_clipSamples / _clipFreq;
        }

        public void Unload()
        {
            _audioClip.UnloadAudioData();
        }

        public AudioClip audioClip
        {
            get
            {
                return _audioClip;
            }
        }

        public double clipLength
        {
            get
            {
                return _clipLength;
            }
        }
    }
}
