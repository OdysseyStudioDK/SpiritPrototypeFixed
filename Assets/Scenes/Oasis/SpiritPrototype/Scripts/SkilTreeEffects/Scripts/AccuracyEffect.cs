using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Effects/Accuracy/AccuracyEffect")]
public class AccuracyEffect : SkillEffect
{
    [Tooltip("Flat bonus added to the player's accuracy stat")]
    public float bonusAccuracy = 10f;

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
            Debug.LogWarning("AccuracyEffect: No PlayerStats found in scene.");
    }
}
