using System;

using UnityEngine;

namespace Orion.Gameplay.Items
{
    public readonly struct TierData
    {
        public Color32 ColorCode { get; }
        public float NormalizedPower { get; }
        public float SpawnProbability { get; }

        public TierData(Color32 colorCode, float normalizedPower, float spawnProbability)
        {
            if (normalizedPower < 0 || normalizedPower > 1f)
            {
                throw new ArgumentOutOfRangeException($"{nameof(normalizedPower)} must be in range [0;1]! " +
                                                      $"{normalizedPower} encountered instead!");
            }

            if (spawnProbability < 0 || spawnProbability > 1f)
            {
                throw new ArgumentOutOfRangeException($"{nameof(SpawnProbability)} must be in range [0:1]! " +
                                                      $"{spawnProbability} encountered instead!");
            }

            ColorCode = colorCode;
            NormalizedPower = normalizedPower;
            SpawnProbability = spawnProbability;
        }

        public override string ToString() => $"Color code = {ColorCode:n2}, " +
                                             $"normalized power = {NormalizedPower:n2}, " +
                                             $"spawn probability = {SpawnProbability:n2}";
    }
}