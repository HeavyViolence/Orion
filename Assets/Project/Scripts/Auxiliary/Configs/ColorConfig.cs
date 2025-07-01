using UnityEngine;

namespace Orion.Auxiliary.Configs
{
    [CreateAssetMenu(fileName = "Color config",
                     menuName = "Orion/Configs/Color config")]
    public sealed class ColorConfig : ScriptableObject
    {
        [SerializeField]
        private Color32 _color;

        public Color32 Value => _color;
    }
}