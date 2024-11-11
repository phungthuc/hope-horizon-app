using FarmerEscape.Scripts.Animal;
using FarmerEscape.Scripts.Inputs;
using Game.Core;
using Game.Scripts.Level;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace FarmerEscape.Scripts.Level
{
    public class AnimalLevelController : LevelController
    {
        [SerializeField] private GameObject animalHolder;
        [SerializeField] private TextMeshProUGUI timerText;
        public float LevelTime = 5;

        private bool _isAllowFlip;
        private bool _isAllowUFO;
        
        private bool _isEnd;
        private float _playTime;

        public UnityEvent AnimalFlipped;
        public UnityEvent UFODone;
        
        private void Start()
        {
            _isEnd = false;
            _playTime = 0;
            _isAllowFlip = false;
            var animals = animalHolder.GetComponentsInChildren<AnimalController>();
            foreach (var animalController in animals)
            {
                var pickable = animalController.GetComponent<Pickable>();
                if (pickable)
                {
                    pickable.OnPointerDown.AddListener(() =>
                    {
                        if (_isAllowFlip)
                        {
                            animalController.Flip();
                        }

                        if (_isAllowUFO)
                        {
                            animalController.UFO();
                        }
                    });
                }
                
                animalController.Flipped.AddListener(() =>
                {
                    _isAllowFlip = false;
                    SetAnimalPick(false);
                    AnimalFlipped.Invoke();
                    CheckWin();
                    CheckLose();
                });
                animalController.Dead.AddListener(() =>
                {
                    _isAllowUFO = false;
                    SetAnimalPick(false);
                    UFODone.Invoke();
                    CheckLose();
                    CheckWin();
                });
            }
        }

        public void AllowFip()
        {
            _isAllowFlip = true;
            SetAnimalPick(true);
        }

        public void UFO()
        {
            _isAllowUFO = true;
            SetAnimalPick(true);
        }

        public void AddTime()
        {
            LevelTime += GameConstant.TIME_ADDITIONAL;
        }
        
        private void SetAnimalPick(bool isAllow)
        {
            var animals = animalHolder.GetComponentsInChildren<AnimalController>();
            foreach (var animalController in animals)
            {
                animalController.IsAllowPick = isAllow;
            }
        }
        
        private void Update()
        {
            if(_isEnd)
            {
                return;
            }
            _playTime += Time.deltaTime;
            timerText.text = (LevelTime - _playTime).ToString("0");
            if(_playTime >= LevelTime)
            {
                GameLose();
            }
            CheckWin();
            CheckLose();
        }


        private void CheckWin()
        {
            var animals = animalHolder.GetComponentsInChildren<AnimalController>();
            foreach (var animal in animals)
            {
                if (!animal.IsFinish)
                {
                    return;
                }
            }
            Win();
        }
        
        private void CheckLose()
        {
            var animals = animalHolder.GetComponentsInChildren<AnimalController>();
            foreach (var animal in animals)
            {
                if (animal.IsDead)
                {
                    GameLose();
                    return;
                }
            }
        }

        private void Win()
        {
            _isEnd = true;
            OnWin();
        }
        
        private void GameLose()
        {
            _isEnd = true;
            OnLose();
        }
    }
}
