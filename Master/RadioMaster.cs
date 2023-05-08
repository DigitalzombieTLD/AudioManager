using UnityEngine;
using static Il2Cpp.BaseAi;

namespace AudioMgr
{
    public static class RadioMaster
    {
        public static string musicPath = @"file:///" + Application.dataPath + @"/../Mods/AuroraRadio";
        public static ClipManager auroraClipManager;

        public static void Initialize()
        {
            auroraClipManager = new ClipManager();

            auroraClipManager.LoadClipsFromDir("AuroraRadio", ClipManager.LoadType.Stream);
        }

        private static Queue GetOrAddQueueToRadio(GameObject radioObject)
        {
            Queue radioQueue = radioObject.GetComponent<Queue>();

            if (radioQueue == null) 
            {
                if(Settings.options.randomRadioMusic) 
                {
                    radioQueue = AudioMaster.CreateQueue(radioObject, auroraClipManager, 3f, Queue.Loop.Randomize, AudioMaster.SourceType.AuroraRadio);
                }
                else
                {
                    radioQueue = AudioMaster.CreateQueue(radioObject, auroraClipManager, 3f, Queue.Loop.All, AudioMaster.SourceType.AuroraRadio);
                }
            }

            return radioQueue;
        }

        public static void StartPlay(GameObject radioObject)
        {
            if (auroraClipManager.clipCount > 0)
            {
                Queue radioQueue = GetOrAddQueueToRadio(radioObject);
                radioQueue.GetNextClip();
                radioQueue.Play();
            }
        }

        public static void StopPlay(GameObject radioObject)
        {
            if (auroraClipManager.clipCount > 0)
            {
                Queue radioQueue = GetOrAddQueueToRadio(radioObject);
                radioQueue.Stop();
            }
        }
    }
}
