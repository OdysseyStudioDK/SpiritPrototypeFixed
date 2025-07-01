using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Config/PlayerReputation")]
public class PlayerReputation : ScriptableObject
{
    [Serializable]
    public struct Milestone
    {
        public int threshold;          // when to fire
        public TextAsset dialogClip;   // companion text/dialog
    }

    [Header("Reputation Data")]
    public int currentPoints = 0;
    public Milestone[] milestones;

    // Fired when crossing a milestone
    public event Action<TextAsset> OnMilestoneReached;

    public void AddPoints(int amount)
    {
        int previous = currentPoints;
        currentPoints += amount;

        // Check each milestone once
        foreach (var m in milestones)
        {
            if (previous < m.threshold && currentPoints >= m.threshold)
                OnMilestoneReached?.Invoke(m.dialogClip);
        }
    }
}