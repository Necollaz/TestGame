using UnityEngine;

namespace GameComponents.Scripts.Player.Indicator
{
    public class MoveIndicatorGenerator : MonoBehaviour
    {
        [SerializeField] private MoveIndicator _indicatorPrefab;
        
        private MoveIndicator _currentIndicator;
        
        public void SpawnIndicator(Vector3 worldPoint)
        {
            ClearIndicator();

            _currentIndicator = Instantiate(_indicatorPrefab);
            _currentIndicator.transform.position = new Vector3(worldPoint.x, _currentIndicator.YOffset, worldPoint.z);
            _currentIndicator.transform.rotation = Quaternion.identity;
            
            _currentIndicator.PlayAnimation();
        }
        
        public void ClearIndicator()
        {
            if(_currentIndicator == null)
            {
                return;
            }

            _currentIndicator.ResetIndicator();
            Destroy(_currentIndicator.gameObject);
            
            _currentIndicator = null;
        }
    }
}