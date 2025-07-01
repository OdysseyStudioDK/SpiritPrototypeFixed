using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Config/RankConfig")]
public class RankConfig : ScriptableObject
{
    [Serializable]
    public struct RankEntry
    {
        public int threshold; // Rank threshold 
        public string title;
        public Relic[] rewards;
        //public float purificationSpeedMultiplier;     For future installment
        public TextAsset companionDialog; 
        [Tooltip("How many relic slots when you reach this rank")]
        public int relicSlotCount;
    }

    public RankEntry[] ranks;

    public RankEntry GetRankForCount(int count)
    {
        RankEntry last = ranks[0];
        foreach (var entry in ranks)
        {
            if (count >= entry.threshold) last = entry;
            else break;
        }
        return last;
    }
}