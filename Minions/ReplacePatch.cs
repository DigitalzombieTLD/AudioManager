using MelonLoader;
using UnityEngine;

namespace AudioMgr
{
    public class ReplacePatch
    {
        private Clip _audioClip;              
        private string _eventID;
        private GameObject _emitterObject;
        private AudioMaster.SourceType _sourceType;
        private Shot _shotSource;
        private Queue _queueSource;
        private ClipManager _clipManager;

        public ReplacePatch(string eventID, ClipManager clipManager, string clipString, AudioMaster.SourceType sourceType)
        {
            _eventID = eventID;
            _audioClip = clipManager.GetClip(clipString);
            _sourceType = sourceType;            
            
            _sourceType = sourceType;
            _clipManager = clipManager;
        }

        public void SetEmitterObject(GameObject emitterObject)
        {
            if (_emitterObject == null)
            {               
                _emitterObject = emitterObject;
                _shotSource = AudioMaster.CreateShot(_emitterObject, _sourceType);
                // _queueSource = AudioMaster.CreateQueue(_emitterObject, _clipManager, 2f, Queue.Loop.All,AudioMaster.SourceType.SFX);
            }            
        }

        public void PlayOneshot(GameObject emitterObject)
        {
            SetEmitterObject(emitterObject);
            _shotSource.PlayOneshot(_audioClip);
        }

        public void PlayQueue(GameObject emitterObject)
        {
            SetEmitterObject(emitterObject);
            _queueSource.Play();
        }

        public string eventID
        {
            get
            {
                return _eventID;
            }
        }

        public GameObject emitterObject
        {
            get
            {
                return _emitterObject;
            }
        }
    }
}
