using UnityEngine;
using UnityEngine.EventSystems;
using GameComponents.Scripts.Resource.Spawner;

namespace GameComponents.Scripts.Player
{
    public class PlayerInput
    {
        private readonly PlayerController _controller;
        private readonly Camera _camera = Camera.main;
        private readonly EventSystem _ui = EventSystem.current;
        
        public PlayerInput(PlayerController controller)
        {
            _controller = controller;
        }

        public void ProcessInput()
        {
            bool didClick = Input.GetMouseButtonDown(0);
            bool didTouch = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;

            if (!didClick && !didTouch)
            {
                return;
            }
            
            if (didClick && _ui.IsPointerOverGameObject())
            {
                return;
            }
            
            if (didTouch && _ui.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }

            Ray ray = didClick ? _camera.ScreenPointToRay(Input.mousePosition) : _camera.ScreenPointToRay(Input.GetTouch(0).position);

            if(!Physics.Raycast(ray, out RaycastHit hit))
            {
                return;
            }
            
            if (hit.collider.TryGetComponent<ResourceSpawner>(out var spawner))
            {
                Vector3 target = spawner.StopPointPlayer != null ? new Vector3(spawner.StopPointPlayer.position.x, _controller.transform.position.y, spawner.StopPointPlayer.position.z) 
                    : new Vector3(spawner.transform.position.x,   _controller.transform.position.y, spawner.transform.position.z);

                _controller.HandleMoveCommand(target, spawner);
            }
            else
            {
                Vector3 groundPoint = new Vector3(hit.point.x, _controller.transform.position.y, hit.point.z);
                _controller.HandleMoveCommand(groundPoint, null);
            }
        }
    }
}