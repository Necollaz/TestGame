using UnityEngine;

namespace GameComponents.Scripts.ResourceComponents
{
    [CreateAssetMenu(fileName = "New Resource Data", menuName = "Resources/Resource Data", order = 51)]
    public class ResourceData : ScriptableObject
    {
        [SerializeField] private Resource _resourcePrefab;
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private Sprite _resourceIcon;
        [SerializeField] private string _resourceDisplayName;

        public Resource ResourcePrefab => _resourcePrefab;
        public ResourceType ResourceType => _resourceType;
        public Sprite ResourceIcon => _resourceIcon;
        public string ResourceDisplayName => _resourceDisplayName;
    }
}