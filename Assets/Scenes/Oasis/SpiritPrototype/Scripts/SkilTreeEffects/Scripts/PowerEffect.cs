using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Effects/Power/PowerEffect")]
public class PowerEffect : SkillEffect
{
    [Tooltip("Multiply the player's base damage by this amount")]
    public float damageMultiplier = 1.5f;

    private bool _applied = false;

    public override void Apply()
    {
        if (_applied) return;
        _applied = true;

        var player = Object.FindFirstObjectByType<PlayerStats>(); // The script controlling your stats
        if (player != null)
        {
            // Do the effect
        }
        else
            Debug.LogWarning("PowerEffect: No PlayerStats found in scene.");
    }
}