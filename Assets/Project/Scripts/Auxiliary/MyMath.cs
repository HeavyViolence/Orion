using System;
using System.Linq;

namespace Orion.Auxiliary
{
    public static class MyMath
    {
        public static float RandomNormal => UnityEngine.Random.Range(-1f, 1f);
        public static float RandomUnit => UnityEngine.Random.Range(0f, 1f);
        public static bool RandomBool => RandomNormal > 0f;
        public static float RandomSign => RandomBool ? 1f : -1f;

        public static T RandomEnum<T>() where T : Enum
        {
            T[] values = (T[])Enum.GetValues(typeof(T));
            int seed = UnityEngine.Random.Range(0, values.Count());

            return values[seed];
        }
    }
}