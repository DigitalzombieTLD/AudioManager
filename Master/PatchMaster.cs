using Harmony;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace AudioMgr
{
    public static class PatchMaster
    {
        private static Dictionary<string, ReplacePatch> _replacePatches = new Dictionary<string, ReplacePatch>();
        private static List<string> _skipPatches = new List<string>();

        public enum ParameterType { Absolute, Percentage, Limitter };
        private static Dictionary<string, ParameterType> _parameterPatches = new Dictionary<string, ParameterType>();
        private static Dictionary<string, float> _parameterValues = new Dictionary<string, float>();

        public static void AddParameterPatch(string parameterID, float value, ParameterType type)
        {
            if (!_parameterPatches.ContainsKey(parameterID))
            {
                _parameterPatches.Add(parameterID, type);
                _parameterValues.Add(parameterID, value);
            }
        }

        public static void AddReplacePatch(string eventID, ClipManager clipManager, string clipString, AudioMaster.SourceType sourceType)
        {
            if (!_replacePatches.ContainsKey(eventID))
            {
                _replacePatches.Add(eventID, new ReplacePatch(eventID, clipManager, clipString, sourceType));
            }
        }

        public static void AddSkipPatch(string eventID)
        {
            if (!_skipPatches.Contains(eventID))
            {
                _skipPatches.Add(eventID);
            }
        }

        public static float ParameterAction(string parameterID, float originalValue)
        {
            if (_parameterPatches.ContainsKey(parameterID))
            {
                if (_parameterPatches[parameterID] == ParameterType.Absolute)
                {
                    return _parameterValues[parameterID];
                }
                else if(_parameterPatches[parameterID] == ParameterType.Percentage)
                {
                    return (originalValue / 100) * _parameterValues[parameterID];
                }
                else if (_parameterPatches[parameterID] == ParameterType.Limitter)
                {
                    if(originalValue > _parameterValues[parameterID])
                    {
                        return _parameterValues[parameterID];
                    }
                }
            }

            return originalValue;
        }

        public static bool PatchAction(string eventID, GameObject gameObject)
        {
            if(_skipPatches.Contains(eventID))
            {               
                return true;
            }
            else if(_replacePatches.ContainsKey(eventID))
            {               
                _replacePatches[eventID].PlayOneshot(gameObject);
                //_globalPatches[eventID].PlayQueue(gameObject);
                return true;
            }            

            return false;
        }
             
    }
}
