using UnityEngine;

namespace DefaultNamespace.Helpers
{
    public static class TransformExtension
    {
        public static void Flip(this Transform transform, ref bool lookRight)
        {
            lookRight = !lookRight;

            var localScale = transform.localScale;

            localScale.x *= -1;

            transform.localScale = localScale;
        }
    }
}