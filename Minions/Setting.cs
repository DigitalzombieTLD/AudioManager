using UnityEngine;

namespace AudioMgr
{
    public class Setting
    {
        public AudioMaster.SourceType sourceType;

        public float spread;
        public float panStereo;
        public float dopplerLevel;
        public float maxDistance;
        public float minDistance;
        public float pitch;
        public float spatialBlend;
        public bool spatialize;
        public float rolloffFactor;
        public AudioRolloffMode rolloffMode;
        //public AnimationCurve rollOffCurve;
        public float maxVolume;
        public float minVolume;
        public int priority;

        public Setting(AudioMaster.SourceType sourcetype)
        {
            sourceType = sourcetype;           
        }
    }
}
