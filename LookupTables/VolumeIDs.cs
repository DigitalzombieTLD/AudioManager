namespace AudioMgr
{
    public static class VolumeIDs
    {
        private static Dictionary<uint, AudioMaster.SourceType> _volumeRtpcID = new Dictionary<uint, AudioMaster.SourceType>() 
        {
            { 2346531308U, AudioMaster.SourceType.BGM}, // uint MUSICVOLUME = 2346531308U;
            { 3325181466U, AudioMaster.SourceType.Voice}, // uint VOVOLUME = 3325181466U;
            { 3546521921U, AudioMaster.SourceType.Ambience}, // uint AMBIENTVOLUME = 3546521921U;
            { 988953028U, AudioMaster.SourceType.SFX}, // uint SFXVOLUME = 988953028U;
        };

        private static uint _masterVolume = 2918011349U;  // uint MASTERVOLUME = 2918011349U;
        private static uint _globalVolume = 4071000082U;  // uint GLOBALVOLUME = 4071000082U;

        public static uint GetRtpcIDMaster()
        {
            return _globalVolume;
        }

        public static uint GetRtpcGlobal()
        {
            return _globalVolume;
        }

        public static Dictionary<uint, AudioMaster.SourceType> GetRtpcIDList()
        {
            return _volumeRtpcID;
        }

    }
}
