using UnityEngine;

[CreateAssetMenu(menuName = "Relics/Effects/Auto Revive (Once/Day)")]
public class AutoReviveEffect : RelicEffect
{
    public override void Apply(PlayerStats stats)
    {
        stats.autoReviveAvailable = true;
    }
}