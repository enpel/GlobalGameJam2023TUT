using System.Collections.Generic;
using UnityEngine;

namespace Gekkou
{

    public static class RandomExt
    {
        public static bool GetRandomBool()
        {
            return Random.Range(0, 2) == 0;
        }

        public static int GetRandomPosNega()
        {
            return GetRandomBool() ? 1 : -1;
        }

        public static int GetRandom(this int value)
        {
            return Random.Range(-value, value + 1);
        }

        public static int GetRandomTo0(this int value)
        {
            return Random.Range(0, value + 1);
        }

        public static float GetRandom(this float value)
        {
            return Random.Range(-value, value);
        }

        public static float GetRandomTo0(this float value)
        {
            return Random.value * value;
        }

        public static Vector2 GetRandom(this Vector2 value)
        {
            return new Vector2(value.x.GetRandom(), value.y.GetRandom());
        }

        public static float GetRandomRange(this Vector2 value)
        {
            return Random.Range(value.x, value.y);
        }

        public static Vector3 GetRandom(this Vector3 value)
        {
            return new Vector3(value.x.GetRandom(), value.y.GetRandom(), value.z.GetRandom());
        }

        public static T GetRandom<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static T GetRandom<T>(this T[] list)
        {
            return list[Random.Range(0, list.Length)];
        }

        public static T GetRemoveRandom<T>(this List<T> list)
        {
            var value = list[Random.Range(0, list.Count)];
            list.Remove(value);
            return value;
        }

        public static int GetRandomIndex<T>(this List<T> list)
        {
            return Random.Range(0, list.Count);
        }

        public static int GetRandomIndex<T>(this T[] list)
        {
            return Random.Range(0, list.Length);
        }

        /// <summary>
        /// Cumulative distribution function
        /// </summary>
        public static int GetRandomIndexOfCumulative(this float[] list)
        {
            var total = 0.0f;
            foreach (var i in list)
            {
                total += i;
            }

            var randomPoint = total.GetRandomTo0();

            for (int i = 0; i < list.Length; i++)
            {
                if (randomPoint < list[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= list[i];
                }
            }

            return list.Length - 1;
        }

        /// <summary>
        /// Cumulative distribution function
        /// </summary>
        public static int GetRandomIndexOfCumulative(this List<float> list)
        {
            return GetRandomIndexOfCumulative(list.ToArray());
        }
    }

}
