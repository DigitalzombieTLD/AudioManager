using MelonLoader;
using UnityEngine;
using static Il2Cpp.BaseAi;

namespace AudioMgr
{
    public static class RadioMaster
    {
        public static string musicPath = @"file:///" + Application.dataPath + @"/../Mods/AuroraRadio";
        public static ClipManager auroraClipManager;
        //public static bool foundFiles = false;

        public static void Initialize()
        {
            if (!Directory.Exists(Application.dataPath + "/../Mods/AuroraRadio"))
            {
                Directory.CreateDirectory(Application.dataPath + "/../Mods/AuroraRadio");
                
            }

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

        private static Stream GetOrAddStreamToRadio(GameObject radioObject, string streamURL)
        {
            Stream radioStream = radioObject.GetComponent<Stream>();

            if (radioStream == null)
            {
                radioStream = AudioMaster.CreateStream(radioObject, streamURL, AudioMaster.SourceType.AuroraRadio);
            }

            radioStream.streamURL = streamURL;

            return radioStream;
        }
        public static void StartStream(GameObject radioObject, string streamURL)
        {
            Stream radioStream = GetOrAddStreamToRadio(radioObject, streamURL);
            radioStream.Play();
        }
        public static void StopStream(GameObject radioObject)
        {
            Stream radioStream = radioObject.GetComponent<Stream>();

            if (radioStream != null)
            {
                radioStream.Stop();
            }
        }

        public static void StartQueue(GameObject radioObject)
        {
            if (auroraClipManager.clipCount > 0)
            {
                Queue radioQueue = GetOrAddQueueToRadio(radioObject);              
                radioQueue.Play();
            }
        }

        public static void StopQueue(GameObject radioObject)
        {
            if (auroraClipManager.clipCount > 0)
            {
                Queue radioQueue = GetOrAddQueueToRadio(radioObject);
                radioQueue.Stop();
            }
        }
    }
}
