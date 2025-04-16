using System.Collections.Generic;
using GameComponents.Scripts.BuildComponents;
using GameComponents.Scripts.ResourceComponents.SpawnerComponents;
using GameComponents.Scripts.ResourceComponents.UI;

namespace GameComponents.Scripts.ResourceComponents
{
    public class ResourceСollector
    {
        private readonly ResourceCollectionUI _collectionUI;
        private readonly Dictionary<ResourceType, int> _collectedCounts;
        
        private ResourceSpawner _spawner;
        private bool _isGathering;
        private bool _pendingGather;

        public ResourceСollector(ResourceCollectionUI collectionUI, Dictionary<ResourceType, int> collectedCounts)
        {
            _collectionUI = collectionUI;
            _collectedCounts = collectedCounts;
        }
        
        public void StartGathering(ResourceSpawner spawner)
        {
            if (spawner == null)
            {
                return;
            }
            
            StopGathering();

            _spawner = spawner;
            _pendingGather = true;
        }
        
        public void Arrived()
        {
            if (!_pendingGather || _spawner == null)
            {
                return;
            }

            _spawner.ResourceProduced += OnResourceProduced;
            
            _collectionUI.ShowPanel();
            CollectAll();

            _isGathering = true;
            _pendingGather = false;
        }
        
        public void StopGathering()
        {
            if (_spawner != null && _isGathering)
            {
                _spawner.ResourceProduced -= OnResourceProduced;
                
                _collectionUI.HidePanelWithDelay();
            }

            _spawner = null;
            _isGathering = false;
            _pendingGather = false;
        }

        private void OnResourceProduced(int newCount)
        {
            CollectOne();
        }

        private void CollectAll()
        {
            while (_spawner != null && _spawner.ResourceCount > 0)
            {
                CollectOne();
            }
        }

        private void CollectOne()
        {
            if (_spawner == null)
            {
                return;
            }

            if (_spawner.CollectResource() && _spawner.TryGetComponent(out BuildingUI bui))
            {
                ResourceType type = bui.ResourceData.ResourceType;
                
                _collectedCounts[type]++;
                
                _collectionUI.UpdateResourceCount(type, _collectedCounts[type]);
            }
        }
    }
}