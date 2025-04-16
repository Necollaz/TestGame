using UnityEngine;
using UnityEngine.UI;
using GameComponents.Scripts.ResourceComponents;
using GameComponents.Scripts.ResourceComponents.SpawnerComponents;
using TMPro;

namespace GameComponents.Scripts.BuildComponents
{
    public class BuildingUI : MonoBehaviour
    {
        [SerializeField] private ResourceData _resourceData; 
        [SerializeField] private ResourceSpawner _resourceSpawner;
        [SerializeField] private Image _resourceIconImage;
        [SerializeField] private TextMeshProUGUI _resourceCountText;
        [SerializeField] private TextMeshProUGUI _resourceNameText; 
        
        public ResourceData ResourceData => _resourceData;
        
        private void Start()
        {
            if (_resourceData != null)
            {
                if (_resourceIconImage != null)
                {
                    _resourceIconImage.sprite = _resourceData.ResourceIcon;
                }
                if (_resourceNameText != null)
                {
                    _resourceNameText.text = _resourceData.ResourceDisplayName;
                }
            }
            
            if (_resourceSpawner == null)
            {
                if (!TryGetComponent(out ResourceSpawner _))
                {
                    return;
                }
            }
            
            _resourceSpawner.ResourceProduced += UpdateResourceCountUI;
            
            UpdateResourceCountUI(_resourceSpawner.ResourceCount);
        }
        
        private void OnDestroy()
        {
            if (_resourceSpawner != null)
            {
                _resourceSpawner.ResourceProduced -= UpdateResourceCountUI;
            }
        }

        private void UpdateResourceCountUI(int count)
        {
            if (_resourceCountText != null)
            {
                _resourceCountText.text = count.ToString();
            }
        }
    }
}