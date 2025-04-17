using UnityEngine;
using UnityEngine.EventSystems;
using GameComponents.Scripts.ResourceComponents.SpawnerComponents;

namespace GameComponents.Scripts.PlayerComponents
{
    public class PlayerInput
    {
        private readonly PlayerController _controller;
        private readonly Camera _camera;
        private readonly EventSystem _ui;
        
        public PlayerInput(PlayerController controller)
        {
            _controller = controller;
            _camera = Camera.main;
            _ui  = EventSystem.current;
        }

        public void ProcessInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(_ui.IsPointerOverGameObject())
                {
                    return;
                }
            }
            else if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Ended && _ui.IsPointerOverGameObject(touch.fingerId))
                {
                    return;
                }
            }
            else
            {
                return;
            }
            
            bool inputDetected = false;
            Ray ray = default;

            if (Input.GetMouseButtonDown(0))
            {
                ray = _camera.ScreenPointToRay(Input.mousePosition);
                inputDetected = true;
            }
            else if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                {
                    ray = _camera.ScreenPointToRay(touch.position);
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
            if(!Physics.Raycast(ray, out RaycastHit hit))
            {
                return;
            }

            PlayerController controller = _controller;
            
            if (hit.collider.TryGetComponent(out ResourceSpawner spawner))
            {
                controller.IndicatorGenerator.ClearIndicator();
                
                Vector3 targetPos = spawner.StopPointPlayer != null
                    ? new Vector3(spawner.StopPointPlayer.position.x, controller.transform.position.y, spawner.StopPointPlayer.position.z)
                    : new Vector3(spawner.transform.position.x,   controller.transform.position.y, spawner.transform.position.z);

                controller.SetTargetPosition(targetPos, spawner);
            }
            else
            {
                Vector3 targetPos = new Vector3(hit.point.x, controller.transform.position.y, hit.point.z);
                
                controller.SetTargetPosition(targetPos, null);
                controller.SpawnIndicator(hit.point);
            }
            
            /*if (Physics.Raycast(ray, out RaycastHit hitInfo))
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
            }*/
        }
    }
}