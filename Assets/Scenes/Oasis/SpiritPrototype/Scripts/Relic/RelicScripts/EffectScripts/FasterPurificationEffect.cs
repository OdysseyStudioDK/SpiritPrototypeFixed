using UnityEngine;

[CreateAssetMenu(menuName = "Relics/Effects/Faster Purification")]
public class FasterPurificationEffect : RelicEffect
{
    public float speedMultiplier = 1.5f;

    public override void Apply(PlayerStats stats)
    {
        stats.purificationSpeedMultiplier *= speedMultiplier;
    }
}