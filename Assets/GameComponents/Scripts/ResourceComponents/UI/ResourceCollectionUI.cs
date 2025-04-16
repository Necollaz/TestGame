using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GameComponents.Scripts.ResourceComponents.UI
{
    public class ResourceCollectionUI : MonoBehaviour
    {
        [SerializeField] private List<ResourceEntryUI> _resourceEntries = new List<ResourceEntryUI>();
        [SerializeField] private RectTransform _panel;
        [SerializeField] private float _slideDuration = 0.5f;
        [SerializeField] private float _hiddenYPos = -300f;
        [SerializeField] private float _visibleYPos = 0f; 
        [SerializeField] private float _hideDelay = 3f;
        
        private Coroutine _hideCoroutine;
        private bool _isVisible = false;
        
        private void Awake()
        {
            if (_panel != null)
            {
                _panel.anchoredPosition = new Vector2(_panel.anchoredPosition.x, _hiddenYPos);
            }
        }
        
        public void ShowPanel()
        {
            if (_panel == null)
            {
                return;
            }
            
            if (_hideCoroutine != null)
            {
                StopCoroutine(_hideCoroutine);
                _hideCoroutine = null;
            }
            
            if (!_isVisible)
            {
                _panel.DOAnchorPosY(_visibleYPos, _slideDuration).SetEase(Ease.OutQuad);
                _isVisible = true;
            }
        }
        
        public void UpdateResourceCount(ResourceType resourceType, int newCount)
        {
            foreach (ResourceEntryUI entry in _resourceEntries)
            {
                if (entry.ResourceType == resourceType && entry.CollectedCountText != null)
                {
                    entry.CollectedCountText.text = newCount.ToString();
                    
                    break;
                }
            }
        }
        
        public void HidePanelWithDelay()
        {
            if (_hideCoroutine != null)
            {
                StopCoroutine(_hideCoroutine);
            }
            _hideCoroutine = StartCoroutine(HideAfterDelay());
        }
        
        private void HidePanel()
        {
            if (_isVisible && _panel != null)
            {
                _panel.DOAnchorPosY(_hiddenYPos, _slideDuration).SetEase(Ease.InQuad);
                
                _isVisible = false;
            }
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSeconds(_hideDelay);
            
            HidePanel();
        }
    }
}