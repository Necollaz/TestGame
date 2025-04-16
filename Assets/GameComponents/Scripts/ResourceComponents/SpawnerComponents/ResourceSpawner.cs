using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using GameComponents.Scripts.PoolComponents;

namespace GameComponents.Scripts.ResourceComponents.SpawnerComponents
{
    public class ResourceSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _stopPointPlayer;
        [SerializeField] private float _spawnRadius = 2f;
        
        private readonly List<Resource> _activeResources = new List<Resource>();
        private ObjectPool<Resource> _resourcePool;
        
        private int _resourceCount;
        
        public event Action<int> ResourceProduced;
        
        public Transform StopPointPlayer => _stopPointPlayer;
        public int ResourceCount => _resourceCount;
        
        public void Initialize(ResourceData resourceData, int poolSize)
        {
            if (resourceData == null)
            {
                return;
            }
            
            _resourcePool = new ObjectPool<Resource>(resourceData.ResourcePrefab, poolSize);
        }
        
        public Resource SpawnResource()
        {
            if (_resourcePool == null)
            {
                return null;
            }
            
            Vector3 spawnOrigin = _spawnPoint != null ? _spawnPoint.position : transform.position;
            Vector2 randomOffset = Random.insideUnitCircle * _spawnRadius;
            Vector3 spawnPosition = spawnOrigin + new Vector3(randomOffset.x, 0f, randomOffset.y);
            
            Resource resourceInstance = _resourcePool.Get();
            
            resourceInstance.transform.SetParent(transform);
            resourceInstance.transform.position = spawnPosition;
            resourceInstance.transform.rotation = Quaternion.identity;
            resourceInstance.gameObject.SetActive(true);

            _activeResources.Add(resourceInstance);
            
            _resourceCount++;
            
            ResourceProduced?.Invoke(_resourceCount);

            return resourceInstance;
        }
        
        public bool CollectResource()
        {
            if (_activeResources.Count == 0 || _resourcePool == null)
                return false;
            
            Resource resource = _activeResources[0];
            
            _activeResources.RemoveAt(0);
            
            _resourceCount--;
            
            ResourceProduced?.Invoke(_resourceCount);
            
            resource.gameObject.SetActive(false);
            _resourcePool.Return(resource);

            return true;
        }
    }
}