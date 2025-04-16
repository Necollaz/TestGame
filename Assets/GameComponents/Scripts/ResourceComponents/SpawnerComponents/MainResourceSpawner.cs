using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameComponents.Scripts.BuildComponents;

namespace GameComponents.Scripts.ResourceComponents.SpawnerComponents
{
    public class MainResourceSpawner : MonoBehaviour
    {
        [SerializeField] private List<BuildingProductionInfo> _buildingProductions = new List<BuildingProductionInfo>();

        private readonly bool _isSpawning = true;
        
        private void Start()
        {
            foreach (BuildingProductionInfo info in _buildingProductions)
            {
                if (info.Spawner != null)
                {
                    if (info.ResourceData != null)
                    {
                        info.Spawner.Initialize(info.ResourceData, info.PoolSize);
                    }
                    
                    StartCoroutine(ProductionRoutine(info));
                }
            }
        }

        private IEnumerator ProductionRoutine(BuildingProductionInfo info)
        {
            while (_isSpawning)
            {
                yield return new WaitForSeconds(info.ProductionTime);
                
                info.Spawner.SpawnResource();
            }
        }
    }
}