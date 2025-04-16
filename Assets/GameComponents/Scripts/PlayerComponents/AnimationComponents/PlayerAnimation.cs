using UnityEngine;

namespace GameComponents.Scripts.PlayerComponents.AnimationComponents
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private int _walkingHash;
        private int _collectHash;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _walkingHash = AnimationDataParams.Params.Walking;
            _collectHash = AnimationDataParams.Params.Collect;
            
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