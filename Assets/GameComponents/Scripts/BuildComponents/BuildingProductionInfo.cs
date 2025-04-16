using UnityEngine;
using GameComponents.Scripts.ResourceComponents;
using GameComponents.Scripts.ResourceComponents.SpawnerComponents;

namespace GameComponents.Scripts.BuildComponents
{
    [System.Serializable]
    public class BuildingProductionInfo
    {
        [SerializeField] private ResourceSpawner _spawner;
        [SerializeField] private ResourceData _resourceData;
        [SerializeField] private int _poolSize = 10;
        [SerializeField] private float _productionTime;

        public ResourceSpawner Spawner => _spawner;
        public ResourceData ResourceData => _resourceData;
        public int PoolSize => _poolSize;
        public float ProductionTime
        {
            get => _productionTime;
            set => _productionTime = value;
        }
    }
}