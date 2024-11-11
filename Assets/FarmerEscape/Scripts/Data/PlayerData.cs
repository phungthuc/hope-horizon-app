using Game.Core;
using UnityEngine;

namespace FarmerEscape.Scripts.Data
{
    public class PlayerData
    {
        public static readonly PlayerData Instance = new();
        
        public int CurrentLevel
        {
            get => PlayerPrefs.GetInt(GameConstant.PLAYER_LEVEL_PLAYER_PREFABS_KEY, GameConstant.DEFAULT_LEVEL);
            set => PlayerPrefs.SetInt(GameConstant.PLAYER_LEVEL_PLAYER_PREFABS_KEY, value);
        }
        
        public int HighestLevel
        {
            get => PlayerPrefs.GetInt(GameConstant.PLAYER_HIGHEST_LEVEL_PLAYER_PREFABS_KEY, GameConstant.DEFAULT_LEVEL);
            set => PlayerPrefs.SetInt(GameConstant.PLAYER_HIGHEST_LEVEL_PLAYER_PREFABS_KEY, value);
        }
        
        public int FlipCount
        {
            get => PlayerPrefs.GetInt(GameConstant.PLAYER_FLIP_COUNT_PLAYER_PREFABS_KEY, 0);
            set => PlayerPrefs.SetInt(GameConstant.PLAYER_FLIP_COUNT_PLAYER_PREFABS_KEY, value);
        }

        public void ResetAll()
        {
            CurrentLevel = GameConstant.DEFAULT_LEVEL;
            HighestLevel = GameConstant.DEFAULT_LEVEL;
        }

    }
}
