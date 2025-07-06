using UnityEngine;

namespace Orion.Auxiliary.Configs
{
    [CreateAssetMenu(fileName = "Animation curve config",
                     menuName = "Orion/Configs/General/Animation curve config")]
    public class AnimationCurveConfig : ScriptableObject
    {
        [SerializeField]
        private AnimationCurve _curve;

        public float Evaluate(float t) => _curve.Evaluate(t);
    }
}