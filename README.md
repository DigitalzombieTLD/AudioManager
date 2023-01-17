
# AudioManager
Audio helper mod for The Long Dark. Uses Unitys native audio engine to enable easier usage of custom audio in mods.

#### Current features v0.8
 - Loading of single audio files (ogg, wav, mp3) on runtime
 - Loading of all audio files inside a directory
 - Loading of single audioclips inside assetbundles
 - Loading of all audioclips inside assetbundles
 - Creating of audiosources on objects/player
 - Playing of singleshot audioclips on object/player
- Playing list of audioclips on object/player (looped, randomized)
- Audiocategories linked to vanilla volume settings
- Global and custom settings for audiosources

#### Core concept
Audioclips are loaded from assetbundle or audiofiles by the ClipManager class. Loading should happen in the main menu scene ("Menu") to not affect loading times and get the clips ready for usage. If custom audio is needed in the main menu after the first load, add a timer of about 2-3 seconds before starting to play.

Loaded clips can be played on audiosources. Those are separated in multiple categories.

 1. Audiosource for playing single short clips (Shot) on a distant audiosource (eg. explosion)
 2. Audiosource for playing a playlist (Queue) on a distant audiosource (eg. radio or randomized ambience)
 3. Audiosource for playing single shot clips (Shot) on the player itself (eg. voice clip)
 4. Audiosource for playing a playlist (Queue) on the player itself (eg. background music)
 
 Each audiosource get a settingstemplate applied on instantiation. The settings affect volume and 3d audio of the source (eg. BGM will get played without any 3d setting, in both ears, SFX can be located in 3d space). Custom templates are possible.

Shot audiosources can play audioclips from a ClipManager instance directly. Queue sources access all clips inside a ClipManager instance. Control of playback is possible through the source instances. If multiple Queue sources share the same ClipManager, both are independant of each other.

#### Usage example


```
   ```csharp
using MelonLoader;
using UnityEngine;
using Il2Cpp;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection;
using AudioMgr;


namespace HereBeNamespace
{
	public class AudioExample : MelonMod
	{      
        private ClipManager _clipManagerBundle;
        private ClipManager _clipManagerDirectory;

        private AssetBundle _assetBundle;

        private Shot _voiceSource;
        private Queue _bgmSource;

        public override void OnInitializeMelon()
        {
            _assetBundle = AssetBundle.LoadFromFile("Mods\\musicbundle.unity3d");
        }

        public override void OnUpdate()
		{
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.Keypad0))
            {
                // Play clip named "isThisFood.ogg on the player
                _voiceSource.Play(_clipManagerBundle.GetClip("isThisFood.ogg"));                
            }
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.Keypad1))
            {
                // Start playlist/Queue on the player
                _bgmSource.Play();
            }
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.Keypad2))
            {
                // Stop playlist/Queue
                _bgmSource.Stop();
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        { 
            if (sceneName.Contains("Menu"))
            {
                //
                // Playlist / Queue
                //

                // Create new ClipManager instance
                _clipManagerBundle = AudioMaster.NewClipManager();

                // Load all clips inside a assetbundle
                _clipManagerBundle.LoadAllClipsFromBundle(_assetBundle);

                // Create new Queue source on player
                _bgmSource = AudioMaster.CreatePlayerQueue(_clipManagerBundle, 3f, Queue.Loop.All, AudioMaster.SourceType.BGM);
                

                //
                // Singleshot
                //

                // Create new ClipManager instance
                _clipManagerDirectory = AudioMaster.NewClipManager();

                // Load all audiofiles inside /Mods/MyAudioMod/voice
                _clipManagerDirectory.LoadClipsFromDir("MyAudioMod/voice", ClipManager.LoadType.Compressed);

                // Create audiosource for single short audioclips on the player
                _voiceSource = AudioMaster.CreatePlayerShot(AudioMaster.SourceType.Voice);
            }
        }
    }
}
    ```
``````
