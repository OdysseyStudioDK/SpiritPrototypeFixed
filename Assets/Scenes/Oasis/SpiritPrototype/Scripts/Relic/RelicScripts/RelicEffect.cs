using UnityEngine;

public abstract class RelicEffect : ScriptableObject
{
    public abstract void Apply(PlayerStats stats);
}