using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.Utilities
{
    public static class MathUtilities
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static bool CoinFlip(float trueRatio = .5f)
        {
            return Random.Range(0f, 1f) < trueRatio;
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

        public static List<Vector3> FermatSpiralPlacement(int count)
        {
            var radiusFactor = 2f;
            var angleFactor = 1f;
            var positions = new List<Vector3>();
            var goldenAngle = 137.5f * angleFactor;

            for (int i = 0; i < count; i++)
            {
                float x = radiusFactor * Mathf.Sqrt(i + 1) * Mathf.Cos(Mathf.Deg2Rad * goldenAngle * (i + 1));
                float z = radiusFactor * Mathf.Sqrt(i + 1) * Mathf.Sin(Mathf.Deg2Rad * goldenAngle * (i + 1));

                var runnerLocalPosition = new Vector3(x, 0, z);
                positions.Add(runnerLocalPosition);
            }

            return positions;
        }

        public static float Remap(this float value, Vector2 originalRange, Vector2 newRange)
        {
            var percentage = (value - originalRange.x) / (originalRange.y - originalRange.x);
            return Mathf.Lerp(newRange.x, newRange.y, percentage);
        }

        public static float Remap(this float value, float currentA, float currentB, float targetA, float targetB)
        {
            var percentage = (value - currentA) / (currentB - currentA);
            return Mathf.Lerp(targetA, targetB, percentage);
        }

        public static int Remap(this int value, Vector2 originalRange, Vector2 newRange)
        {
            var percentage = (value - originalRange.x) / (originalRange.y - originalRange.x);
            return (int)Mathf.Lerp(newRange.x, newRange.y, percentage);
        }

        public static int Remap(this int value, float currentA, float currentB, float targetA, float targetB)
        {
            var percentage = (value - currentA) / (currentB - currentA);
            return (int)Mathf.Lerp(targetA, targetB, percentage);
        }

        public static bool IsInRange(this float value, float min, float max)
        {
            return (value >= min && value <= max);
        }

        public static bool IsInRange(this float value, Vector2 range)
        {
            return (value >= range.x && value <= range.y);
        }

        public static bool IsInRange(this int value, float min, float max)
        {
            return value >= min && value <= max;
        }

        public static bool IsInRange(this int value, Vector2 range)
        {
            return value >= range.x && value <= range.y;
        }
    }

}