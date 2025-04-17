using System;
using UnityEngine;
using GameComponents.Scripts.PlayerComponents.AnimationComponents;
using GameComponents.Scripts.ResourceComponents.SpawnerComponents;

namespace GameComponents.Scripts.PlayerComponents
{
    [RequireComponent(typeof(PlayerAnimation))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 10f;

        private PlayerAnimation _animation;
        private Vector3 _targetPosition;
        private bool _isMoving = false;
        
        public event Action ReachedDestination;
        
        private void Awake()
        {
            _animation = GetComponent<PlayerAnimation>();
            _animation.SetWalking(false);
        }
        
        private void Start()
        {
            _animation.SetWalking(false);
        }
        
        private void Update()
        {
            HandleMovement();
        }
        
        public void MoveTo(Vector3 target)
        {
            _targetPosition = target;
            _isMoving = true;
            _animation.SetCollecting(false);  
        }
        
        private void HandleMovement()
        {
            if(!_isMoving)
            {
                return;
            }
            
            _animation.SetWalking(true);
            
            Vector3 direction = _targetPosition - transform.position;
            direction.y = 0f;
            
            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
            
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
            {
                _isMoving = false;
                
                _animation.SetWalking(false);

                ReachedDestination?.Invoke();
            }
        }
        
        private void OnCollisionExit(Collision collision)
        {
            if (collision.collider.TryGetComponent(out ResourceSpawner _))
            {
                GetComponent<PlayerController>()?.OnLeftBuilding();
            }
        }
    }
}