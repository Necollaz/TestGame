using UnityEngine;
using TMPro;

namespace GameComponents.Scripts.ResourceComponents.UI
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