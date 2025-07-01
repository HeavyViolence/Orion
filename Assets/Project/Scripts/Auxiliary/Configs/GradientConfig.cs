using System;

using UnityEngine;

namespace Orion.Auxiliary.Configs
{
    [CreateAssetMenu(fileName = "Gradient config",
                     menuName = "Orion/Configs/Gradient config")]
    public sealed class GradientConfig : ScriptableObject
    {
        [SerializeField]
        private Gradient _gradient;

        public Color32 Evaluate(float f)
        {
            if (f < 0f || f > 1f)
            {
                throw new ArgumentException($"Evaluator {nameof(f)} must be in range [0;1]! " +
                                            $"{f} encountered instead!");
            }

            return _gradient.Evaluate(f);
        }
    }
}