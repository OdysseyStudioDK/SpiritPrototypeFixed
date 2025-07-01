using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Effects/Agility/AgilityEffect")]
public class AgilityEffect : SkillEffect
{
    [Tooltip("Multiply the player's movement speed by this amount")]
    public float speedMultiplier = 1.2f;

    private bool _applied = false;

    public override void Apply()
    {
        if (_applied) return;
        _applied = true;

        var player = Object.FindFirstObjectByType<PlayerStats>();   // The script controlling your stats
        if (player != null)
        {
            // Do the effect
        }
        else
            Debug.LogWarning("AgilityEffect: No PlayerStats found in scene.");
    }
}
