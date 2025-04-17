using UnityEngine;

namespace GameComponents.Scripts.ResourceComponents
{
    [CreateAssetMenu(fileName = "New Resource Data", menuName = "Resources/Resource Data", order = 51)]
    public class ResourceData : ScriptableObject
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private Sprite _resourceIcon;
        [SerializeField] private string _resourceDisplayName;
        
        public ResourceType ResourceType => _resourceType;
        public Sprite ResourceIcon => _resourceIcon;
        public string ResourceDisplayName => _resourceDisplayName;
    }
}