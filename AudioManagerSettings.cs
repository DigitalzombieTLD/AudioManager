using UnityEngine;
using ModSettings;
using MelonLoader;

namespace AudioMgr
{
    internal class AudioManagerSettings : JsonModSettings
    {     
		[Section("Aurora Audio")]

        [Name("Limit Volume")]
        [Description("Limit Volume to Value below")]
        public bool enableAuroraTweaks = false;

        [Name("Music")]
		[Description("Left: Silent / Right: Maximum Volume")]
		[Slider(0, 100)]
		public int auroraVolume = 0;

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
         
        }
    }

    internal static class Settings
    {
        public static AudioManagerSettings options;

        public static void OnLoad()
        {
            options = new AudioManagerSettings();
            options.AddToModSettings("AudioManager");           
        }
    }
}
