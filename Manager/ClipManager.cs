using Il2Cpp;
using MelonLoader;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace AudioMgr
{
    public class ClipManager
    {
        private Dictionary<string, Clip> _loadedClips = new Dictionary<string, Clip>();
        public enum LoadType { Compressed, Decompressed, Stream };
        private string downloaderPath = @"file:///" + Application.dataPath + @"/../Mods";

        public Clip GetClip(string clipName)
        {
            if (_loadedClips.ContainsKey(clipName))
            {
                return _loadedClips[clipName];
            }
            else
            {
                MelonLogger.Msg("Warning: Trying to retrieve non-existent audioclip " + clipName);
                return null;
            }
        }

        public Clip GetClipAtIndex(int index)
        {
            if (_loadedClips.ElementAt(index).Key != null)
            {
                return _loadedClips.ElementAt(index).Value;
            }
            else
            {
                MelonLogger.Msg("Warning: Trying to retrieve non-existent audioclip at index " + index);
                return null;
            }
        }

        public void UnloadClip(string clipName)
        {
            if (_loadedClips.ContainsKey(clipName))
            {
                _loadedClips[clipName].Unload();
                _loadedClips.Remove(clipName);
            }
        }

        public void UnloadAllClips()
        {
            foreach (KeyValuePair<string, Clip> singleClip in _loadedClips)
            {
                singleClip.Value.Unload();
                _loadedClips.Remove(singleClip.Key);
            }
        }

        public void LoadClipFromBundle(string newClipName, string clipInBundle, AssetBundle assetBundle)
        {
            if (_loadedClips.ContainsKey(newClipName))
            {
                return;
            }

            MelonCoroutines.Start(LoadClipFromBundleRoutine(newClipName, clipInBundle, assetBundle));
        }

        public void LoadAllClipsFromBundle(AssetBundle assetBundle)
        {
            string[] clipNamesInBundle = assetBundle.AllAssetNames();

            foreach(string singleName in clipNamesInBundle)
            {
                if(singleName.Contains(".ogg") || singleName.Contains(".wav") || singleName.Contains(".mp3"))
                {
                    string[] clipNameSplit;
                    string tmpClipName;

                    clipNameSplit = singleName.Split('/');
                    tmpClipName = clipNameSplit[clipNameSplit.Length-1];

                    if (!_loadedClips.ContainsKey(tmpClipName))
                    {
                        LoadClipFromBundle(FileNameCutter(tmpClipName), singleName, assetBundle);
                    }                    
                }
            }
        }

        private IEnumerator LoadClipFromBundleRoutine(string newClipName, string clipInBundle, AssetBundle assetBundle)
        {
            _loadedClips.Add(newClipName, new Clip(assetBundle.LoadAsset<AudioClip>(clipInBundle), newClipName));
            yield break;
        }

        public void LoadClipsFromDir(string directory, LoadType loadType)
        {            
            string[] allTheFilesInDir = Directory.GetFiles(Application.dataPath + "/../Mods/" + directory, "*", SearchOption.TopDirectoryOnly);

            foreach(string singleFile in allTheFilesInDir)
            {
                LoadClipFromFile(FileNameCutter(ClipStringCutter(singleFile)), directory + "/" + ClipStringCutter(singleFile), loadType);
            }
        }

        public void LoadClipFromFile(string newClipName, string fileName, LoadType loadType)
        {
            if (_loadedClips.ContainsKey(newClipName))
            {
                return;
            }

            MelonCoroutines.Start(LoadClipFromFileRoutine(newClipName, fileName, loadType));
        }

        private IEnumerator LoadClipFromFileRoutine(string clipName, string fileName, LoadType loadType)
        {
            bool compressed = true;
            bool stream = false;

            if (loadType == LoadType.Compressed)
            {
                compressed = true;
                stream = false;
            }

            if (loadType == LoadType.Decompressed)
            {
                compressed = false;
                stream = false;
            }

            if (loadType == LoadType.Stream)
            {
                compressed = false;
                stream = true;
            }

            _loadedClips.Add(clipName, null);

            UnityWebRequest www;
            www = UnityWebRequest.Get(downloaderPath + @"/" + fileName);
            www.SendWebRequest();

            while (!www.isDone) yield return null;

            if (!www.isNetworkError && !www.isHttpError)
            {
                _loadedClips[clipName] = new Clip(WebRequestWWW.InternalCreateAudioClipUsingDH(www.downloadHandler, www.url, stream, compressed, AudioType.UNKNOWN), clipName);
                

            }
            else
            {
                _loadedClips.Remove(clipName);
                MelonLogger.Msg("Error while loading audioclip. Skipping " + www.error);
            }
        }

        private string ClipStringCutter(string stringToCut)
        {
            string[] clipNameSplit;
            string tmpClipName;

            clipNameSplit = stringToCut.Split('\\');
      
            tmpClipName = clipNameSplit[clipNameSplit.Length-1];

            return (tmpClipName);
        }

        private string FileNameCutter(string stringToCut)
        {
            string[] clipNameSplit;
            string tmpClipName;

            clipNameSplit = stringToCut.Split('.');

            tmpClipName = clipNameSplit[0];

            return tmpClipName;
        }

        public int clipCount
        {
            get
            {
                return _loadedClips.Count;
            }
        }
    }
}
