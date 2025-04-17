using System;
using System.Collections.Generic;
using UnityEngine;
using GameComponents.Scripts.Player.Animations;
using GameComponents.Scripts.Resource;
using GameComponents.Scripts.Resource.Spawner;
using GameComponents.Scripts.Resource.UI;

namespace GameComponents.Scripts.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAnimation))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ResourceCollectionUI _resourceCollectionUI;
        
        private readonly Dictionary<ResourceType, int> _collectedResources = new Dictionary<ResourceType, int>();
        
        private PlayerMovement _playerMovement;
        private PlayerInput _inputProcessor;
        private PlayerAnimation _playerAnimation;
        private ResourceSpawner _targetBuilding;
        private ResourceСollector _collector;
        
        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _collector = new ResourceСollector(_resourceCollectionUI, _collectedResources);
        }

        private void OnEnable()
        {
            _playerMovement.ReachedDestination += OnReachedDestination;
        }
        
        private void OnDisable()
        {
            _playerMovement.ReachedDestination -= OnReachedDestination;
        }
        
        private void Start()
        {
            InitializePlayer();
        }

        private void Update()
        {
            _inputProcessor.ProcessInput();
        }
        
        public void HandleMoveCommand(Vector3 worldPoint, ResourceSpawner spawner)
        {
            _playerMovement.MoveTo(worldPoint);
            
            if (spawner != null)
            {
                _targetBuilding = spawner;
                _collector.StartGathering(spawner);
            }
            else
            {
                _targetBuilding = null;
                EndGathering();
            }
        }

        private void OnReachedDestination()
        {
            if (_targetBuilding != null)
            {
                _collector.Arrived();
                _playerAnimation.SetCollecting(true);
            }
        }
        
        public void OnLeftBuilding()
        {
            EndGathering();
        }
        
        private void EndGathering()
        {
            _collector.StopGathering();
            _playerAnimation.SetCollecting(false);
        }
        
        private void InitializePlayer()
        {
            foreach(ResourceType type in Enum.GetValues(typeof(ResourceType)))
            {
                _collectedResources[type] = 0;
            }

            _inputProcessor = new PlayerInput(this);
        }
    }
}