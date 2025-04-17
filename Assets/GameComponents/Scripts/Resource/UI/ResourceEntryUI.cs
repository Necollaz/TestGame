using GameComponents.Scripts.Resource;
using UnityEngine;
using TMPro;

namespace GameComponents.Scripts.Resource.UI
{
    [System.Serializable]
    public class ResourceEntryUI
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private TextMeshProUGUI _collectedCountText;
        
        public ResourceType ResourceType => _resourceType;
        public TextMeshProUGUI CollectedCountText => _collectedCountText;
    }
}