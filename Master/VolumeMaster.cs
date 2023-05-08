using UnityEngine;

namespace AudioMgr
{
    public static class VolumeMaster
    {
        public delegate void OnVolumeChange();
        public static event OnVolumeChange onVolumeChange;

        private static float _masterVolume = 1f;

        private static Dictionary<AudioMaster.SourceType, float> _globalVolumes = new Dictionary<AudioMaster.SourceType, float>
        {
            { AudioMaster.SourceType.SFX, 1f },
            { AudioMaster.SourceType.BGM, 1f },
            { AudioMaster.SourceType.Voice, 1f },
            { AudioMaster.SourceType.Ambience, 1f },
            { AudioMaster.SourceType.Custom, 1f },
            { AudioMaster.SourceType.AuroraRadio, 1f }
        };

        private static void VolumeChanged()
        {
            if (onVolumeChange != null)
            {
                onVolumeChange();
            }
        }

        public static float GetVolume(AudioMaster.SourceType type)
        {           
            _globalVolumes[AudioMaster.SourceType.Custom] = Settings.options.customVolume;

            return ApplyMasterOffset(_globalVolumes[type]); 
        }

        private static float ApplyMasterOffset(float volume)
        {   

            return volume * _masterVolume;
        }

        public static void SetMasterVolume(float volume)
        {
            _masterVolume = volume;
            VolumeChanged();
        }

        public static void SetVolume(AudioMaster.SourceType type, float volume)
        {
            _globalVolumes[type] = volume;
            VolumeChanged();
        }       
    }
}
