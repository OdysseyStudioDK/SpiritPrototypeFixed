using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Relics/Relic")]
public class Relic : ScriptableObject
{
    [Header("UI")]
    public string relicName;
    public Sprite icon;
    [TextArea] public string description;

    [Header("Effects")]
    public List<RelicEffect> effects = new List<RelicEffect>();
}