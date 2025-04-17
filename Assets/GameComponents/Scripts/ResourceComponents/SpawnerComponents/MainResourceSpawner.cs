using System.Collections.Generic;
using UnityEngine;
using GameComponents.Scripts.BuildComponents;

namespace GameComponents.Scripts.ResourceComponents.SpawnerComponents
{
    public class MainResourceSpawner : MonoBehaviour
    {
        [SerializeField] private List<BuildingProductionInfo> _buildingProductions = new List<BuildingProductionInfo>();
        
        private void Start()
        {
            foreach (BuildingProductionInfo info in _buildingProductions)
            {
                if(info.Spawner == null || info.ResourceData == null)
                {
                    continue;
                }
                
                info.Spawner.Initialize(info.ResourceData);
            }
        }
    }
}