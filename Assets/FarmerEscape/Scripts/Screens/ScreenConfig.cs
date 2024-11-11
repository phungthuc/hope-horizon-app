using System.Collections.Generic;
using UnityEngine;

namespace Game.Screens
{
    [CreateAssetMenu(fileName = "Screen", menuName = "Screen/ScreenConfig")]
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