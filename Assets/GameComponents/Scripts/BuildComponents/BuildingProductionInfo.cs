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

        public ResourceSpawner Spawner => _spawner;
        public ResourceData ResourceData => _resourceData;
    }
}