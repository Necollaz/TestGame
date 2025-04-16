using UnityEngine;
using DG.Tweening;

namespace GameComponents.Scripts.PlayerComponents.IndicatorComponents
{
    public class MoveIndicator : MonoBehaviour
    {
        [SerializeField] private Vector3 _targetScaleOffset = new Vector3(0.05f, 0f, 0.05f);
        [SerializeField] private float _rotationDuration = 2f;
        [SerializeField] private float _scaleAnimationDuration = 0.5f;
        [SerializeField] private float _yOffset = 1f;
        
        private Vector3 _defaultScale;
        private Quaternion _defaultRotation;
        
        private void Awake()
        {
            _defaultScale = transform.localScale;
            _defaultRotation = transform.rotation;
        }
        
        public void PlayAnimation()
        {
            transform.DORotate(new Vector3(0, 360f, 0), _rotationDuration, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
            
            Vector3 targetScale = new Vector3(_defaultScale.x - _targetScaleOffset.x, _defaultScale.y, _defaultScale.z - _targetScaleOffset.z);
            
            transform.DOScale(targetScale, _scaleAnimationDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
        
        public void ResetIndicator()
        {
            transform.DOKill(true);
            transform.rotation = _defaultRotation;
            transform.localScale = _defaultScale;
        }
        
        public float GetYOffset()
        {
            return _yOffset;
        }
    }
}