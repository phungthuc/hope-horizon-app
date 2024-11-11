using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Level
{
    [CreateAssetMenu(fileName = "LevelByTypeHolder", menuName = "Game/LevelByTypeHolder")]
    public class LevelByTypeHolder : ScriptableObject
    {
        public List<int> driveLevelIndexes;
        public List<int> bossLevelIndexes;
    }
}
