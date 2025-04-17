using UnityEngine;

namespace GameComponents.Scripts.ResourceComponents
{
    [RequireComponent(typeof(Collider))]
    public class ResourceFall : MonoBehaviour
    {
        [SerializeField] private float _gravity = 9.81f;
        [SerializeField] private float _groundY = 0f;

        private float _velocity;
        private bool _isFalling;
        
        private void Update()
        {
            HandleFall();
        }
        
        public void StartFall(Vector3 spawnPosition)
        {
            transform.position = spawnPosition;
            _velocity = 0f;
            _isFalling = true;
        }

        private void HandleFall()
        {
            if(!_isFalling)
            {
                return;
            }
            
            _velocity += _gravity * Time.deltaTime;
            transform.position += Vector3.down * _velocity * Time.deltaTime;
            
            if (transform.position.y <= _groundY)
            {
                transform.position = new Vector3(transform.position.x, _groundY, transform.position.z);

                _isFalling = false;
            }
        }
    }
}