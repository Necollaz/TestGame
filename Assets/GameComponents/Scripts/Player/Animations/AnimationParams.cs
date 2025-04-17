using UnityEngine;

namespace GameComponents.Scripts.Player.Animations
{
    public class AnimationParams
    {
        private static class ClipNames
        {
            public const string Walk = "isWalk";
            public const string Collect = "isCollect";
        }

        public static class Hashes
        {
            public static readonly int Walk = Animator.StringToHash(ClipNames.Walk);
            public static readonly int Collect = Animator.StringToHash(ClipNames.Collect);
        }
    }
}