using UnityEngine;
using UnityEngine.UI;
using GameComponents.Scripts.Resource;
using GameComponents.Scripts.Resource.Spawner;
using TMPro;

namespace GameComponents.Scripts.Building
{
    public class BuildingUI : MonoBehaviour
    {
        [SerializeField] private Image _resourceIconImage;
        [SerializeField] private TextMeshProUGUI _resourceCountText;
        [SerializeField] private TextMeshProUGUI _resourceNameText; 
        
        private ResourceSpawner _resourceSpawner;
        
        private void Start()
        {
            _resourceSpawner = GetComponent<ResourceSpawner>();
            
            ResourceData data = _resourceSpawner.ResourceData;

            if(_resourceIconImage != null)
            {
                _resourceIconImage.sprite = data.ResourceIcon;
            }

            if(_resourceNameText != null)
            {
                _resourceNameText.text = data.ResourceDisplayName;
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