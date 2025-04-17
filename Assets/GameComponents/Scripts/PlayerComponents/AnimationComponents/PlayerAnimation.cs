using UnityEngine;

namespace GameComponents.Scripts.PlayerComponents.AnimationComponents
{
    [DefaultExecutionOrder(-100)]
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private readonly int _walkingHash = AnimationDataParams.Params.Walking;
        private readonly int _collectHash = AnimationDataParams.Params.Collect;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            _animator.SetBool(_walkingHash, false);
            _animator.SetBool(_collectHash, false);
        }

        public void SetWalking(bool isWalking)
        {
            _animator.SetBool(_collectHash, false);
            _animator.SetBool(_walkingHash, isWalking);
        }

        public void SetCollecting(bool isCollecting)
        {
            _animator.SetBool(_walkingHash, !isCollecting);
            _animator.SetBool(_collectHash, isCollecting);
        }
    }
}