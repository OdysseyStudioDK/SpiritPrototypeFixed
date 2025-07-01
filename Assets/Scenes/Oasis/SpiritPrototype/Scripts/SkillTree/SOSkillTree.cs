using UnityEngine;

[CreateAssetMenu(menuName = "Skills/SOSkillTree")]
public class SOSkillTree : ScriptableObject
{
    [Header("All Skill Branches")]
    public SOSkillBranch[] branches;
}