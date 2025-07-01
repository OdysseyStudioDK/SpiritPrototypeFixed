using UnityEngine;

[CreateAssetMenu(menuName = "Relics/Effects/Detect Enemies")]
public class DetectEnemiesEffect : RelicEffect
{
    public override void Apply(PlayerStats stats)
    {
        stats.detectEnemies = true;
    }
}