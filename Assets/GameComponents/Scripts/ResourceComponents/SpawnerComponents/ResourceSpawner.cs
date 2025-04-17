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
        
        private readonly Queue<Resource> _activeResources = new Queue<Resource>();
        
        private ObjectPool<Resource> _resourcePool;
        private ResourceType _assignedType;
        
        private int _resourceCount;
        
        public event Action<int> ResourceProduced;
        
        public ResourceType AssignedResourceType => _assignedType;
        public Transform StopPointPlayer => _stopPointPlayer;
        public int ResourceCount => _resourceCount;
        
        public void Initialize(ResourceData resourceData, int poolSize)
        {
            if (resourceData == null)
            {
                return;
            }
            
            _assignedType = resourceData.ResourceType;
            _resourcePool = new ObjectPool<Resource>(resourceData.ResourcePrefab, poolSize);
        }
        
        public Resource SpawnResource()
        {
            if (_resourcePool == null)
            {
                return null;
            }
            
            Vector3 origin = _spawnPoint != null ? _spawnPoint.position : transform.position;
            Vector2 offset = Random.insideUnitCircle * _spawnRadius;
            Vector3 spawnPosition = origin + new Vector3(offset.x, 0f, offset.y);
            
            Resource resourceInstance = _resourcePool.Get();
            
            resourceInstance.transform.SetParent(transform);
            resourceInstance.SpawnAt(spawnPosition);
            
            /*resourceInstance.transform.position = spawnPosition;
            resourceInstance.transform.rotation = Quaternion.identity;
            resourceInstance.gameObject.SetActive(true);*/

            _activeResources.Enqueue(resourceInstance);
            
            _resourceCount++;
            
            ResourceProduced?.Invoke(_resourceCount);

            return resourceInstance;
        }
        
        public bool CollectResource()
        {
            if(_activeResources.Count == 0 || _resourcePool == null)
            {
                return false;
            }
            
            Resource resource = _activeResources.Dequeue();
            
            _resourceCount--;
            
            ResourceProduced?.Invoke(_resourceCount);
            
            resource.Collect();
            _resourcePool.Return(resource);

            return true;
        }
    }
}