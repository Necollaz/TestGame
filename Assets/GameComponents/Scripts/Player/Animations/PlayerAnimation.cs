using UnityEngine;

namespace GameComponents.Scripts.Player.Animations
{
    [DefaultExecutionOrder(-100)]
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            _animator.SetBool(AnimationParams.Hashes.Walk, false);
            _animator.SetBool(AnimationParams.Hashes.Collect, false);
        }

        public void SetWalking(bool isWalking)
        {
            _animator.SetBool(AnimationParams.Hashes.Walk, isWalking);

            if(isWalking)
            {
                _animator.SetBool(AnimationParams.Hashes.Collect, false);
            }
        }

        public void SetCollecting(bool isCollecting)
        {
            _animator.SetBool(AnimationParams.Hashes.Collect, isCollecting);

            if(isCollecting)
            {
                _animator.SetBool(AnimationParams.Hashes.Walk, false);
            }
        }
    }
}