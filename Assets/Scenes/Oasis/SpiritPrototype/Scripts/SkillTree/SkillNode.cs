using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Skills/SkillNode")]
public class SkillNode : ScriptableObject
{
    public string nodeName;
    public Sprite icon;
    [TextArea] public string description;
    public int costPoints;
    public SkillNode[] children;
    [Tooltip("Which node must be unlocked before this one can be purchased.")]
    public SkillNode parentNode;
    public bool isUnlocked;

    public SkillEffect effect;

    public event Action<SkillNode> OnUnlocked;

    public void Unlock()
    {
        if (isUnlocked) return;
        isUnlocked = true;
        effect?.Apply();
        OnUnlocked?.Invoke(this);
    }
}
