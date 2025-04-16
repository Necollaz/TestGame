using UnityEngine;

namespace GameComponents.Scripts.PlayerComponents.AnimationComponents
{
    public class AnimationDataParams
    {
        public static class Params
        {
            private const string WalkParameter = "isWalk";
            private const string CollectParameter = "isCollect";
            
            public static readonly int Walking = Animator.StringToHash(WalkParameter);
            public static readonly int Collect = Animator.StringToHash(CollectParameter);
        }
    }
}