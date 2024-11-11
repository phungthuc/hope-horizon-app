using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace FarmerEscape.Scripts.Animal
{
    public class AnimalController : MonoBehaviour
    {
        [SerializeField] LayerMask boundingLayerMask;
        [SerializeField] LayerMask[] enemyLayerMask;
        [SerializeField] LayerMask[] ignoreLayerMask;
        public float speed;
        
        private bool _isMoving;
        private Rigidbody2D _rigidbody2D;
        private Vector3 _targetVel;
        private Vector3 _currentVel;
        
        public bool IsFinish { get; private set; }
        public bool IsDead { get; private set; }
        public bool IsAllowPick { get; set; }
        public UnityEvent Flipped;
        public UnityEvent Dead;
        public UnityEvent Backed;

        private void Start()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        }

        public void OnMove()
        {
            if (_isMoving || IsDead || IsFinish || IsAllowPick)
            {
                return;
            }
            _isMoving = true;
        }

        public void OnStopMove()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _isMoving = false;
        }

        public void Flip()
        {
            transform.Rotate(0, 0, 180);
            Flipped.Invoke();
        }

        public void UFO()
        {
            IsAllowPick = true;
            transform.DOScale(0, 0.5f).SetLink(gameObject).OnComplete(() =>
            {
                Dead.Invoke();
                Destroy(gameObject);
            });
        }

        private void Update()
        {
            if (_isMoving && !IsDead)
            {
                _currentVel = _targetVel;
                float smoothVal = .2f; // Higher = 'Smoother'  
                _targetVel = transform.up * speed;

                _rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, _targetVel, ref _currentVel, smoothVal);
                
                if(IsFinish)
                {
                    OnStopMove();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & boundingLayerMask.value) != 0)
            {
                IsFinish = true;
                OnStopMove();
                Destroy(gameObject);
            }
            else if (Array.Exists(enemyLayerMask, element => ((1 << other.gameObject.layer) & element.value) != 0))
            {
                OnDead();
            }
            else 
            {
                if (Array.Exists(ignoreLayerMask, element => ((1 << other.gameObject.layer) & element.value) != 0))
                {
                    return;
                }
                if (_isMoving)
                {
                    Back();
                }
            }
        }

        private void OnDead()
        {
            IsDead = true;
            OnStopMove();
        }

        private void Back()
        {
            var targetPos = transform.position - transform.up / 2;
            transform.DOMove(targetPos, 0.2f).SetLink(gameObject).OnComplete(OnStopMove);
            Backed.Invoke();
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (Array.Exists(ignoreLayerMask, element => ((1 << other.gameObject.layer) & element.value) != 0))
            {
                return;
            }
            OnStopMove();
        }
    }
}
