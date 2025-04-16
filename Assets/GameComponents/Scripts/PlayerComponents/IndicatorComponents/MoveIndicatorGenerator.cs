using UnityEngine;
using GameComponents.Scripts.PoolComponents;

namespace GameComponents.Scripts.PlayerComponents.IndicatorComponents
{
    public class MoveIndicatorGenerator
    {
        private readonly PlayerController _controller;
        private readonly ObjectPool<MoveIndicator> _pool;

        public MoveIndicatorGenerator(PlayerController controller, ObjectPool<MoveIndicator> pool)
        {
            _controller = controller;
            _pool = pool;
        }

        public void GenerateIndicator(Vector3 hitPoint)
        {
            ClearIndicator();

            MoveIndicator indicator = _pool.Get();
            float yOffset = indicator.GetYOffset();
        
            indicator.transform.position = new Vector3(hitPoint.x, yOffset, hitPoint.z);
            indicator.transform.rotation = Quaternion.identity;
            
            indicator.PlayAnimation();
            _controller.SetCurrentIndicator(indicator);
        }
        
        public void ClearIndicator()
        {
            if (_controller.CurrentIndicator != null)
            {
                _controller.CurrentIndicator.ResetIndicator();
                _pool.Return(_controller.CurrentIndicator);
                _controller.SetCurrentIndicator(null);
            }
        }
    }
}