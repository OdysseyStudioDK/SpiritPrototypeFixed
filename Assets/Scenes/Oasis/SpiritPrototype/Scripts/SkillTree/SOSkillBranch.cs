using UnityEngine;

[CreateAssetMenu(menuName = "Skills/SOSkillBranch")]
public class SOSkillBranch : ScriptableObject
{
    [Header("Branch Info")]
    public string branchName;
    public Sprite icon;
    [TextArea] public string description;
    [Tooltip("The root SkillNode for this branch")]
    public SkillNode rootNode;
}