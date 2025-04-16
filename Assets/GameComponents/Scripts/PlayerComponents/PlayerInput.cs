using UnityEngine;
using GameComponents.Scripts.ResourceComponents.SpawnerComponents;

namespace GameComponents.Scripts.PlayerComponents
{
    public class PlayerInput
    {
        private readonly PlayerController _controller;

        public PlayerInput(PlayerController controller)
        {
            _controller = controller;
        }

        public void ProcessInput()
        {
            bool inputDetected = false;
            Ray ray = default;

            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                inputDetected = true;
            }
            else if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                {
                    ray = Camera.main.ScreenPointToRay(touch.position);
                    inputDetected = true;
                }
            }

            if (inputDetected)
            {
                ProcessRaycast(ray);
            }
        }

        private void ProcessRaycast(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.TryGetComponent(out ResourceSpawner spawner))
                {
                    _controller.IndicatorGenerator.ClearIndicator();

                    Vector3 targetPos;
                    
                    if (spawner.StopPointPlayer != null)
                    {
                        targetPos = new Vector3(spawner.StopPointPlayer.position.x, _controller.transform.position.y, spawner.StopPointPlayer.position.z);
                    }
                    else
                    {
                        targetPos = new Vector3(spawner.transform.position.x, _controller.transform.position.y, spawner.transform.position.z);
                    }
                
                    _controller.SetTargetPosition(targetPos, spawner);
                }
                else
                {
                    Vector3 targetPos = new Vector3(hitInfo.point.x, _controller.transform.position.y, hitInfo.point.z);
                
                    _controller.SetTargetPosition(targetPos, null);
                    _controller.SpawnIndicator(hitInfo.point);
                }
            }
        }
    }
}