using System.Collections.Generic;
using UnityEngine;

public class RelicSystem : MonoBehaviour
{
    public int maxSlots = 4;
    public List<Relic> unlocked = new List<Relic>();
    public List<Relic> equipped = new List<Relic>();
    public PlayerStats playerStats;

    private bool baseCanTakeFallDamage;
    private float basePurificationSpeed;

    void Awake()
    {
        baseCanTakeFallDamage = playerStats.canTakeFallDamage;
        basePurificationSpeed = playerStats.purificationSpeedMultiplier;
    }

    public void UnlockRelic(Relic relic)
    {
        if (!unlocked.Contains(relic))
        {
            unlocked.Add(relic);
        }
    }

    public void Equip(Relic relic)
    {
        if (equipped.Count < maxSlots && unlocked.Contains(relic))
        {
            equipped.Add(relic);
            RecalculateAllEffects();
        }
    }

    public void Unequip(Relic relic)
    {
        if (equipped.Remove(relic))
        {
            RecalculateAllEffects();
        }
    }

    private void RecalculateAllEffects()
    {
        // Reset
        playerStats.canTakeFallDamage = baseCanTakeFallDamage;
        playerStats.detectEnemies = false;
        playerStats.autoReviveAvailable = false;
        playerStats.purificationSpeedMultiplier = basePurificationSpeed;

        // Apply all relic effects
        foreach (var relic in equipped)
        {
            foreach (var effect in relic.effects)
            {
                if (effect != null)
                    effect.Apply(playerStats);
            }
        }
    }
}