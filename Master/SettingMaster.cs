using Il2Cpp;
using UnityEngine;

namespace AudioMgr
{
    public static class SettingMaster
    {
        private static Dictionary<AudioMaster.SourceType, Setting> _defaultSetting = new Dictionary<AudioMaster.SourceType, Setting>();

        public static Setting NewSetting(AudioMaster.SourceType sourceType, float spread, float panStereo, float dopplerLevel, float maxDistance, float minDistance, float pitch, float spatialBlend, float rolloffFactor, bool spatialize, AudioRolloffMode audioRolloffMode, AnimationCurve rollOffCurve, int priority)
        {            
            Setting newSetting = new Setting(sourceType);
            newSetting.sourceType = sourceType;
            newSetting.spread = spread;
            newSetting.panStereo = panStereo;
            newSetting.dopplerLevel = dopplerLevel;
            newSetting.maxDistance = maxDistance;
            newSetting.minDistance = minDistance;
            newSetting.pitch = pitch;
            newSetting.spatialBlend = spatialBlend;
            newSetting.rolloffFactor = rolloffFactor;
            newSetting.spatialize = spatialize;
            newSetting.rolloffMode = audioRolloffMode;
            //newSetting.rollOffCurve = rollOffCurve;
            newSetting.priority = priority;

            return newSetting;
        }

        public static void Setup()
        {
            //AnimationCurve stdRollOffCurve = null;

            _defaultSetting.Add(AudioMaster.SourceType.SFX, new Setting(AudioMaster.SourceType.SFX));
            _defaultSetting.Add(AudioMaster.SourceType.Ambience, new Setting(AudioMaster.SourceType.Ambience));
            _defaultSetting.Add(AudioMaster.SourceType.Voice, new Setting(AudioMaster.SourceType.Voice));
            _defaultSetting.Add(AudioMaster.SourceType.BGM, new Setting(AudioMaster.SourceType.BGM));
            _defaultSetting.Add(AudioMaster.SourceType.Custom, new Setting(AudioMaster.SourceType.Custom));

            _defaultSetting[AudioMaster.SourceType.SFX].spread = 0.0f;
            _defaultSetting[AudioMaster.SourceType.SFX].panStereo = 0.0f;
            _defaultSetting[AudioMaster.SourceType.SFX].dopplerLevel = 0f;
            _defaultSetting[AudioMaster.SourceType.SFX].maxDistance = 18f; // 18.0f;
            _defaultSetting[AudioMaster.SourceType.SFX].minDistance = 1.0f;
            _defaultSetting[AudioMaster.SourceType.SFX].pitch = 1.0f;
            _defaultSetting[AudioMaster.SourceType.SFX].spatialBlend = 1.0f;
            _defaultSetting[AudioMaster.SourceType.SFX].rolloffFactor = 0.8f;
            _defaultSetting[AudioMaster.SourceType.SFX].spatialize = true;
            _defaultSetting[AudioMaster.SourceType.SFX].rolloffMode = AudioRolloffMode.Linear;
            //_defaultSetting[AudioMaster.SourceType.SFX].rollOffCurve = stdRollOffCurve;
            _defaultSetting[AudioMaster.SourceType.SFX].priority = 128;

            _defaultSetting[AudioMaster.SourceType.Ambience].spread = 0.0f;
            _defaultSetting[AudioMaster.SourceType.Ambience].panStereo = 0.0f;
            _defaultSetting[AudioMaster.SourceType.Ambience].dopplerLevel = 0f;
            _defaultSetting[AudioMaster.SourceType.Ambience].maxDistance = 2000.0f;
            _defaultSetting[AudioMaster.SourceType.Ambience].minDistance = 50.0f;
            _defaultSetting[AudioMaster.SourceType.Ambience].pitch = 1.0f;
            _defaultSetting[AudioMaster.SourceType.Ambience].spatialBlend = 0.0f;
            _defaultSetting[AudioMaster.SourceType.Ambience].rolloffFactor = 1f;
            _defaultSetting[AudioMaster.SourceType.Ambience].spatialize = true;
            _defaultSetting[AudioMaster.SourceType.Ambience].rolloffMode = AudioRolloffMode.Linear;
            //_defaultSetting[AudioMaster.SourceType.Ambience].rollOffCurve = stdRollOffCurve;
            _defaultSetting[AudioMaster.SourceType.Ambience].priority = 64;

            _defaultSetting[AudioMaster.SourceType.Voice].spread = 0.0f;
            _defaultSetting[AudioMaster.SourceType.Voice].panStereo = 0.0f;
            _defaultSetting[AudioMaster.SourceType.Voice].dopplerLevel = 0f;
            _defaultSetting[AudioMaster.SourceType.Voice].maxDistance = 20.0f;
            _defaultSetting[AudioMaster.SourceType.Voice].minDistance = 1.0f;
            _defaultSetting[AudioMaster.SourceType.Voice].pitch = 1.0f;
            _defaultSetting[AudioMaster.SourceType.Voice].spatialBlend = 1.0f;
            _defaultSetting[AudioMaster.SourceType.Voice].rolloffFactor = 1.0f;
            _defaultSetting[AudioMaster.SourceType.Voice].spatialize = true;
            _defaultSetting[AudioMaster.SourceType.Voice].rolloffMode = AudioRolloffMode.Linear;
            //_defaultSetting[AudioMaster.SourceType.Voice].rollOffCurve = stdRollOffCurve;
            _defaultSetting[AudioMaster.SourceType.Voice].priority = 50;

            _defaultSetting[AudioMaster.SourceType.BGM].spread = 0.0f;
            _defaultSetting[AudioMaster.SourceType.BGM].panStereo = 0.0f;
            _defaultSetting[AudioMaster.SourceType.BGM].dopplerLevel = 0f;
            _defaultSetting[AudioMaster.SourceType.BGM].maxDistance = 10.0f;
            _defaultSetting[AudioMaster.SourceType.BGM].minDistance = 1.0f;
            _defaultSetting[AudioMaster.SourceType.BGM].pitch = 1.0f;
            _defaultSetting[AudioMaster.SourceType.BGM].spatialBlend = 0.0f;
            _defaultSetting[AudioMaster.SourceType.BGM].rolloffFactor = 1.0f;
            _defaultSetting[AudioMaster.SourceType.BGM].spatialize = true;
            _defaultSetting[AudioMaster.SourceType.BGM].rolloffMode = AudioRolloffMode.Linear;
            //_defaultSetting[AudioMaster.SourceType.BGM].rollOffCurve = stdRollOffCurve;
            _defaultSetting[AudioMaster.SourceType.BGM].priority = 64;

            _defaultSetting[AudioMaster.SourceType.Custom].spread = 0.0f;
            _defaultSetting[AudioMaster.SourceType.Custom].panStereo = 1.0f;
            _defaultSetting[AudioMaster.SourceType.Custom].dopplerLevel = 0f;
            _defaultSetting[AudioMaster.SourceType.Custom].maxDistance = 500.0f;
            _defaultSetting[AudioMaster.SourceType.Custom].minDistance = 1.0f;
            _defaultSetting[AudioMaster.SourceType.Custom].pitch = 1.0f;
            _defaultSetting[AudioMaster.SourceType.Custom].spatialBlend = 0.8f;
            _defaultSetting[AudioMaster.SourceType.Custom].rolloffFactor = 1.0f;
            _defaultSetting[AudioMaster.SourceType.Custom].spatialize = true;
            _defaultSetting[AudioMaster.SourceType.Custom].rolloffMode = AudioRolloffMode.Linear;
            //_defaultSetting[AudioMaster.SourceType.Custom].rollOffCurve = stdRollOffCurve;
            _defaultSetting[AudioMaster.SourceType.Custom].priority = 80;
        }
        public static Setting Defaults(AudioMaster.SourceType sourceType)
        {
            return _defaultSetting[sourceType];
        }
    }
}
