using UnityEngine;

namespace GameComponents.Scripts.PlayerComponents
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector3 _minBounds;
        [SerializeField] private Vector3 _maxBounds;
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _edgeThreshold = 0.1f;
        [SerializeField] private float _touchDragFactor = 0.1f;

        private void Update()
        {
            MoveCamera();
        }

        private void MoveCamera()
        {
            Vector3 move = Vector3.zero;

            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Moved)
                {
                    Vector3 right = _camera.transform.right;
                    Vector3 forward = Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up).normalized;
                    move = (-touch.deltaPosition.x * right - touch.deltaPosition.y * forward) * _touchDragFactor;
                }
            }
            else
            {
                Vector3 viewportPos = _camera.ScreenToViewportPoint(Input.mousePosition);
                Vector3 right = _camera.transform.right;
                Vector3 forward = Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up).normalized;
                
                if(viewportPos.x < _edgeThreshold)
                {
                    move -= right;
                }
                else if(viewportPos.x > 1f - _edgeThreshold)
                {
                    move += right;
                }
                
                if(viewportPos.y < _edgeThreshold)
                {
                    move -= forward;
                }
                else if(viewportPos.y > 1f - _edgeThreshold)
                {
                    move += forward;
                }

                move = move.normalized * _moveSpeed * Time.deltaTime;
            }
            
            Vector3 newPosition = transform.position + move;
            newPosition.x = Mathf.Clamp(newPosition.x, _minBounds.x, _maxBounds.x);
            newPosition.z = Mathf.Clamp(newPosition.z, _minBounds.z, _maxBounds.z);
            transform.position = newPosition;
        }
    }
}
