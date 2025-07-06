using Orion.Auxiliary;
using Orion.Auxiliary.Configs;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Orion.Gameplay.Items
{
    [CreateAssetMenu(fileName = "Item evaluator config",
                     menuName = "Orion/Configs/Items/Item evaluator config")]
    public sealed class ItemEvaluatorConfig : ScriptableObject
    {
        private readonly int _tiersAmount = Enum.GetValues(typeof(Tier)).Length;

        [SerializeField, Tooltip("Item color codes from highest to lowest tier")]
        private List<ColorConfig> _tierColorCodes;

        [SerializeField, Tooltip("Item power distribution from highest to lowest tier")]
        private AnimationCurveConfig _powerDistribution;

        [SerializeField, Tooltip("Item pawn probability from highest to lowest tier")]
        private AnimationCurveConfig _spawnProbability;

        private Dictionary<Tier, TierData> _tiersDataCache;

        private void OnEnable()
        {
            BuildCache();
        }

        public void BuildCache()
        {
            if (_tierColorCodes is null ||
                _tierColorCodes.Count != _tiersAmount ||
                _powerDistribution == null ||
                _spawnProbability == null)
            {
                _tiersDataCache = new(Enumerable.Empty<KeyValuePair<Tier, TierData>>());
                return;
            }

            _tiersDataCache = new(_tiersAmount);

            for (int i = 0; i < _tiersAmount; i++)
            {
                float t = (float)i / (_tiersAmount - 1);

                Tier tier = (Tier)i;
                Color32 colorCode = _tierColorCodes[i].Value;
                float normalizedPower = _powerDistribution.Evaluate(t);
                float spawnProbability = _spawnProbability.Evaluate(t);

                _tiersDataCache.Add(tier, new(colorCode, normalizedPower, spawnProbability));
            }
        }

        public IEnumerable<KeyValuePair<Tier, TierData>> GetCurrentCache() => _tiersDataCache;

        public TierData GetTierData(Tier tier) => _tiersDataCache[tier];

        public Tier GetRandomTier() => MyMath.RandomEnum<Tier>();

        public Tier GetProbableTier()
        {
            float mappedSeed = _spawnProbability.Evaluate(MyMath.RandomUnit);
            int tierCode = _tiersAmount - 1 - Mathf.RoundToInt(mappedSeed * (_tiersAmount - 1));
            return (Tier)tierCode;
        }

        public Color32 GetTierColorCode(Tier tier) => _tiersDataCache[tier].ColorCode;
    }
}