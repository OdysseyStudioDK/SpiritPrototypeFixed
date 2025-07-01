using UnityEngine;

[CreateAssetMenu(menuName = "Relics/Effects/NoFallDamage")]
public class NoFallDamageEffect : RelicEffect
{
    public override void Apply(PlayerStats stats)
    {
        stats.canTakeFallDamage = false;
    }
}