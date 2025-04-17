using System;
using UnityEngine;

namespace GameComponents.Scripts.Resource.Spawner
{
    public class ResourceSpawner : MonoBehaviour
    {
        [SerializeField] private ResourceData _resourceData;
        [SerializeField] private Transform _stopPointPlayer;
        [SerializeField] private float _productionTime = 5f;
        
        private ProductionTimer _timer;
        private int _resourceCount;
        
        public event Action<int> ResourceProduced;
        
        public ResourceData ResourceData => _resourceData;
        public ResourceType AssignedResourceType => _resourceData.ResourceType;
        public Transform StopPointPlayer => _stopPointPlayer;
        public int ResourceCount => _resourceCount;
        
        private void Awake()
        {
            _timer = new ProductionTimer(_productionTime);
            _timer.Elapsed += OnTimerElapsed;
        }
        
        private void Update()
        {
            _timer.Tick(Time.deltaTime);
        }
        
        private void OnDestroy()
        {
            if(_timer != null)
            {
                _timer.Elapsed -= OnTimerElapsed;
            }
        }
        
        public bool CollectResource()
        {
            if(_resourceCount <= 0)
            {
                return false;
            }

            _resourceCount--;
            
            ResourceProduced?.Invoke(_resourceCount);
            
            return true;
        }
        
        private void OnTimerElapsed()
        {
            _resourceCount++;
            
            ResourceProduced?.Invoke(_resourceCount);
        }
    }
}