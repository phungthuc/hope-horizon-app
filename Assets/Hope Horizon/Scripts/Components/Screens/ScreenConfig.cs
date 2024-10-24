using System.Collections.Generic;
using UnityEngine;

namespace LaserPathPuzzle.Scripts.Components.Screens
{
    [CreateAssetMenu(fileName = "Screen", menuName = "LaserPathPuzzle/Screen/ScreenConfig")]
    public class ScreenConfig : ScriptableObject
    {
        [SerializeField] private List<string> screenKeyList;

        public List<string> ScreenKeyList
        {
            get => screenKeyList;
            set => screenKeyList = value;
        }
    }
}
