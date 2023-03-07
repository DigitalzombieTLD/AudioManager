using Il2Cpp;
using static Il2Cpp.CookingPotItem;
using static Il2CppAK.SWITCHES;
using static Il2CppSystem.Net.ServicePointManager;

namespace AudioMgr
{
    public static class GameParameterIDs
    {
        private static Dictionary<uint, string> _gameParameterID = new Dictionary<uint, string>() 
        {
            {3770572768U,"AZIMUTH"},
            {2348125913U,"DISTANCE"},
            {4025273213U,"ELEVATION"},
            {2832409559U,"EMITTERCONE"},
            {1840270857U,"LISTENERCONE"},
            {3546521921U,"AMBIENTVOLUME"},
            {991664965U,"AURORAELECTROLIZER"},
            {3174855862U,"AURORASTRENGTH"},
            {3182076450U,"BEARANGERLEVEL"},
            {2713585638U,"BEARDISTANCE"},
            {4007481307U,"BREATHLEVEL"},
            {3729329928U,"CONDITIONHEARTBEAT"},
            {2497634674U,"COOKINGSTATE"},
            {1954039910U,"COUGHINGINTENSITY"},
            {2055680380U,"DARKWALKERBANISHMENTGLYPH"},
            {2551811045U,"EARRINGINGVOLUME"},
            {2141048674U,"ELEVATION"},
            {2728861532U,"ENTITYDISTANCERTPC"},
            {2496722626U,"ENTITYDUCKMETER"},
            {1844117411U,"ENTITYFSDUCKER"},
            {3949411123U,"ENTITYINTENSITY"},
            {923990149U,"FIREBLENDSTATE"},
            {1967737439U,"FOOTSTEPWINDDUCKER"},
            {4071000082U,"GLOBALVOLUME"},
            {2950444590U,"GUIWINDLEVELLING"},
            {642872282U,"ICECRACKINGVOLUME"},
            {1116036883U,"INCREMENTAL"},
            {3332483390U,"INTERACTIVEBUSMETER"},
            {1444535918U,"INTERACTIVEDUCKSMETER"},
            {2686616043U,"INTERIOREXTERIOR"},
            {2064316281U,"INVENTORYWEIGHTGENERAL"},
            {135115684U,"INVENTORYWEIGHTMETAL"},
            {1721946080U,"INVENTORYWEIGHTWATER"},
            {330491720U,"INVENTORYWEIGHTWOOD"},
            {3731891983U,"ITEMCONDITION"},
            {1825515640U,"LIGHTSOURCEFLICKER"},
            {477512431U,"LPFILTERLEVEL"},
            {2918011349U,"MASTERVOLUME"},
            {1599364659U,"MUSICDUCKSMETER"},
            {2346531308U,"MUSICVOLUME"},
            {3385215644U,"PAINPULSE"},
            {841158053U,"PLAYERBOWFATIGUE"},
            {3741502372U,"PLAYERINCLINE"},
            {2886637823U,"PLAYERVELOCITY"},
            {88134581U,"PLAYERWINDANGLE"},
            {2692428577U,"PREDATORDISTANCE"},
            {1922851440U,"ROPEVELOCITY"},
            {988953028U,"SFXVOLUME"},
            {1668026760U,"SPEECHAUTODUCKER"},
            {640949982U,"SPEED"},
            {2935111770U,"SPVALVE"},
            {1351367891U,"SS_AIR_FEAR"},
            {3002758120U,"SS_AIR_FREEFALL"},
            {1029930033U,"SS_AIR_FURY"},
            {2648548617U,"SS_AIR_MONTH"},
            {3847924954U,"SS_AIR_PRESENCE"},
            {822163944U,"SS_AIR_RPM"},
            {3074696722U,"SS_AIR_SIZE"},
            {3715662592U,"SS_AIR_STORM"},
            {3203397129U,"SS_AIR_TIMEOFDAY"},
            {4160247818U,"SS_AIR_TURBULENCE"},
            {2720872602U,"STIMPACKSTRENGTH"},
            {2603274823U,"STOVEDOORS"},
            {899752953U,"TEMPERATURE"},
            {3729505769U,"TIMEOFDAY"},
            {3325181466U,"VOVOLUME"},
            {476134665U,"WEAPONFIRE"},
            {69961555U,"WEAPONSHELLNUMDROPPED"},
            {3643818186U,"WINDACTUALSPEED"},
            {2777629753U,"WINDGUSTINTENSITY"},
            {1219918067U,"WINDGUSTSSIDECHAIN"},
            {3236975263U,"WINDINTENSITYBLEND"},
            {262526082U,"WOLFATTACKMUTE"},
            {1377735963U,"WOOZYFADE"}
        };



        public static string GetString(uint parameterID)
        {
            if (_gameParameterID.ContainsKey(parameterID))
            {
                return _gameParameterID[parameterID];
            }
            else
            {
                return "GameParameter ID " + parameterID + " not found!";
            }
        }
    }
}
