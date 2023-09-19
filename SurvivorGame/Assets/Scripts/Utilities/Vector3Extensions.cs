using System.Collections;
using UnityEngine;

namespace SaitoGames.Utilities
{
    public static class Vector3Extensions
    {
        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }

        public static Vector2 With(this Vector2 original, float? x = null, float? y = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y);
        }

        public static Vector3 Add(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            var newX = (float)(x == null ? original.x : original.x + x);
            var newY = (float)(y == null ? original.y : original.y + y);
            var newZ = (float)(z == null ? original.z : original.z + z);

            return new Vector3(newX, newY, newZ);
        }

        public static Vector2 Add(this Vector2 original, float? x = null, float? y = null)
        {
            var newX = (float)(x == null ? original.x : original.x + x);
            var newY = (float)(y == null ? original.y : original.y + y);

            return new Vector2(newX, newY);
        }

        public static Vector3 RemapToXZPlane(this Vector2 original, float y = 0f)
        {
            return new Vector3(original.x, y, original.y).normalized;
        }
        
        public static Vector2 DirectionFromXZPlane(this Vector3 original)
        {
            return new Vector2(original.x, original.z).normalized;
        }

        public static Vector3 GetAngledVector(Vector3 origin, Vector3 point, float angle)
        {
            var localPoint = point - origin;
            var angleRadians = Mathf.Deg2Rad * angle;

            var newX = localPoint.x * Mathf.Cos(angleRadians) - localPoint.z * Mathf.Sin(angleRadians);
            var newZ = localPoint.x * Mathf.Sin(angleRadians) + localPoint.z * Mathf.Cos(angleRadians);

            var angledPointLocal = new Vector3(newX, localPoint.y, newZ);

            return origin + angledPointLocal;
        }

    }

}