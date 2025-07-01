using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Effects/Support/SupportEffect")]
public class SupportEffect : SkillEffect
{
    [Tooltip("Multiply the player's healing and shielding output by this amount")]
    public float supportMultiplier = 1.3f;

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
            Debug.LogWarning("SupportEffect: No PlayerStats found in scene.");
    }
}