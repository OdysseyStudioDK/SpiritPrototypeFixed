using UnityEngine;

public class ReputationSystem : MonoBehaviour
{
    [Tooltip("Drag in your PlayerReputation SO")]
    public PlayerReputation reputation;

    void OnEnable()
        => SpiritPurifier.SpiritPurified += HandlePurified;

    void OnDisable()
        => SpiritPurifier.SpiritPurified -= HandlePurified;

    private void HandlePurified(SpiritStateController ctrl)
    {
        reputation.AddPoints(1);
        Debug.Log($"[Reputation] Now at {reputation.currentPoints} points");
    }
}