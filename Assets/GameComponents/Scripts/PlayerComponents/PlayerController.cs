using System;
using System.Collections.Generic;
using UnityEngine;
using GameComponents.Scripts.PlayerComponents.AnimationComponents;
using GameComponents.Scripts.PlayerComponents.IndicatorComponents;
using GameComponents.Scripts.PoolComponents;
using GameComponents.Scripts.ResourceComponents;
using GameComponents.Scripts.ResourceComponents.SpawnerComponents;
using GameComponents.Scripts.ResourceComponents.UI;

namespace GameComponents.Scripts.PlayerComponents
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private MoveIndicator _moveIndicatorPrefab;
        [SerializeField] private ResourceCollectionUI _resourceCollectionUI;
        
        private readonly Dictionary<ResourceType, int> _collectedResources = new Dictionary<ResourceType, int>();
        private readonly int _poolSizeIndicator = 5;
        
        private PlayerMovement _playerMovement;
        private PlayerInput _inputProcessor;
        private MoveIndicator _currentIndicator;
        private MoveIndicatorGenerator _indicatorGenerator;
        private ObjectPool<MoveIndicator> _indicatorPool;
        private ResourceSpawner _targetBuilding;
        private ResourceСollector _сollector;
        private Coroutine _collectCoroutine;

        public MoveIndicator CurrentIndicator => _currentIndicator;
        public MoveIndicatorGenerator IndicatorGenerator => _indicatorGenerator;
        
        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _сollector = new ResourceСollector(_resourceCollectionUI, _collectedResources);
        }

        private void OnEnable()
        {
            _playerMovement.ReachedDestination += OnReachedDestination;
        }

        private void Start()
        {
            InitializePlayer();
        }

        private void Update()
        {
            _inputProcessor.ProcessInput();
        }
        
        public void SetTargetPosition(Vector3 targetPosition, ResourceSpawner spawner)
        {
            _playerMovement.MoveTo(targetPosition);
            
            if(spawner != null)
            {
                _targetBuilding = spawner;
                _сollector.StartGathering(spawner);
            }
            else
            {
                _targetBuilding = null;
                _сollector.StopGathering();

                if(TryGetComponent(out PlayerAnimation playerAnimation))
                {
                    playerAnimation.SetCollecting(false);
                }
            }
            
            _playerMovement.MoveTo(targetPosition);
        }
        
        public void SpawnIndicator(Vector3 hitPoint)
        {
            _indicatorGenerator.GenerateIndicator(hitPoint);
        }
        
        public void SetCurrentIndicator(MoveIndicator indicator)
        {
            if (_currentIndicator != null)
            {
                _currentIndicator.ResetIndicator();
                _indicatorPool.Return(_currentIndicator);
            }
            
            _currentIndicator = indicator;
        }
        
        public void OnLeftBuilding()
        {
            _сollector.StopGathering();
            
            if(TryGetComponent(out PlayerAnimation playerAnimation))
            {
                playerAnimation.SetCollecting(false);
            }
        }
        
        private void InitializePlayer()
        {
            _indicatorPool = new ObjectPool<MoveIndicator>(_moveIndicatorPrefab, _poolSizeIndicator);
            
            foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            {
                if (!_collectedResources.ContainsKey(type))
                {
                    _collectedResources.Add(type, 0);
                }
            }
            
            _inputProcessor = new PlayerInput(this);
            _indicatorGenerator = new MoveIndicatorGenerator(this, _indicatorPool);
        }
        
        private void OnReachedDestination()
        {
            _indicatorGenerator.ClearIndicator();

            if(_targetBuilding != null)
            {
                _сollector.Arrived();
                
                if(TryGetComponent(out PlayerAnimation playerAnimation))
                {
                    playerAnimation.SetCollecting(true);
                }
            }
        }
    }
}