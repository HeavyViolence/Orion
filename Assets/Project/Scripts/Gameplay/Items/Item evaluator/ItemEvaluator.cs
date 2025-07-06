using Orion.Auxiliary;

using System;

using UnityEngine;

namespace Orion.Gameplay.Items
{
    public sealed class ItemEvaluator
    {
        private readonly ItemEvaluatorConfig _config;

        public ItemEvaluator(ItemEvaluatorConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            _config = config;
        }

        public Tier GetRandomTier() => _config.GetRandomTier();
        public Tier GetProbableTier() => _config.GetProbableTier();
        public Color32 GetTierColorCode(Tier tier) => _config.GetTierColorCode(tier);

        public int EvalueateProperty(Vector2Int range, Tier tier, ItemPropertyEvaluation evaluation)
        {
            TierData data = _config.GetTierData(tier);

            switch (evaluation)
            {
                case ItemPropertyEvaluation.LeftToRight:
                    return Mathf.RoundToInt(Mathf.Lerp(range.x, range.y, data.NormalizedPower));

                case ItemPropertyEvaluation.RightToLeft:
                    return Mathf.RoundToInt(Mathf.Lerp(range.y, range.x, data.NormalizedPower));

                case ItemPropertyEvaluation.Leftmost:
                    return range.x;

                case ItemPropertyEvaluation.Rightmost:
                    return range.y;

                case ItemPropertyEvaluation.Center:
                    return Mathf.RoundToInt((range.x + range.y) / 2f);

                case ItemPropertyEvaluation.Random:
                    return Mathf.RoundToInt(Mathf.Lerp(range.x, range.y, MyMath.RandomUnit));

                default:
                    goto case ItemPropertyEvaluation.LeftToRight;
            }
        }

        public float EvalueateProperty(Vector2 range, Tier tier, ItemPropertyEvaluation evaluation)
        {
            TierData data = _config.GetTierData(tier);

            switch (evaluation)
            {
                case ItemPropertyEvaluation.LeftToRight:
                    return Mathf.Lerp(range.x, range.y, data.NormalizedPower);

                case ItemPropertyEvaluation.RightToLeft:
                    return Mathf.Lerp(range.y, range.x, data.NormalizedPower);

                case ItemPropertyEvaluation.Leftmost:
                    return range.x;

                case ItemPropertyEvaluation.Rightmost:
                    return range.y;

                case ItemPropertyEvaluation.Center:
                    return (range.x + range.y) / 2f;

                case ItemPropertyEvaluation.Random:
                    return Mathf.Lerp(range.x, range.y, MyMath.RandomUnit);

                default:
                    goto case ItemPropertyEvaluation.LeftToRight;
            }
        }
    }
}