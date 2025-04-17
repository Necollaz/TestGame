using System;
using UnityEngine;

namespace GameComponents.Scripts.Resource.Spawner
{
    public class ProductionTimer
    {
        private float _interval;
        private float _accumulated;
        
        public event Action Elapsed;

        public ProductionTimer(float interval)
        {
            _interval = Mathf.Max(interval, 0.001f);
            _accumulated = 0f;
        }
        
        public void Tick(float deltaTime)
        {
            _accumulated += deltaTime;
            
            if (_accumulated >= _interval)
            {
                _accumulated -= _interval;
                
                Elapsed?.Invoke();
            }
        }
        
        public void SetInterval(float interval)
        {
            _interval = Mathf.Max(interval, 0.001f);
            
            Reset();
        }
        
        private void Reset() => _accumulated = 0f;
    }
}