using UnityEngine;
using ModSettings;
using MelonLoader;

namespace AudioMgr
{
    internal class AudioManagerSettings : JsonModSettings
    {
        [Section("Custom Audio")]

        [Name("Volume")]
        [Description("Volume of custom audio sources")]
        [Slider(0, 1)]
        public float customVolume = 1;

        [Section("Aurora Audio")]

        [Name("Limit Volume")]
        [Description("Limit Volume to Value below")]
        public bool enableAuroraTweaks = false;

        [Name("Music")]
		[Description("Left: Silent / Right: Maximum Volume")]
		[Slider(0, 100)]
		public int auroraVolume = 0;

        [Section("Radio")]

        [Name("Custom Radio Music")]
        [Description("Play custom ogg files instead of the standard classical music")]
        public bool customRadioMusic = false;

        [Name("Radio Works Without Auroa")]
        [Description("Makes radios functional at any time")]
        public bool radioWorksWithoutAurora = true;

        [Name("Randomize Playback Order")]
        [Description("Plays audiofiles in alphabetical order if disabled")]
        public bool randomRadioMusic = true;

        [Name("Radio Volume")]
        [Description("Left: Silent / Right: Maximum Volume")]
        [Slider(0, 1)]
        public float customRadioVolume = 0.3f;

        [Section("Wind Audio Indoor (clattering)")]

        [Name("Limit Volume")]
        [Description("Limit Volume to Value below")]
        public bool enableWindTweaks = false;

        [Name("Indoor Wind Audio")]
        [Description("Left: Silent / Right: Maximum Volume")]
        [Slider(0, 100)]
        public int windVolume = 0;

        [Section("Waterfalls")]

        [Name("Silent Waterfalls")]
        [Description("Disable waterfall audio")]
        public bool disableWaterfall = true;

        [Section("Flares")]

        [Name("Silent Flares")]
        [Description("Disable flare audio")]
        public bool disableFlare = true;

        protected override void OnConfirm()
        {
            base.OnConfirm();
            VolumeMaster.SetVolume(AudioMaster.SourceType.AuroraRadio, customRadioVolume);
        }
    }

    internal static class Settings
    {
        public static AudioManagerSettings options;

        public static void OnLoad()
        {
            options = new AudioManagerSettings();
            options.AddToModSettings("AudioManager");
            VolumeMaster.SetVolume(AudioMaster.SourceType.AuroraRadio, Settings.options.customRadioVolume);
        }
    }
}
