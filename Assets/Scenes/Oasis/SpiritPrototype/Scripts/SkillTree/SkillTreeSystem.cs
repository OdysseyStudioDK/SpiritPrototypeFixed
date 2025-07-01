using UnityEngine;

public class SkillTreeSystem : MonoBehaviour
{
    public RankSystem rankSystem;
    public SkillTreeUI skillTreeUI;
    public SOSkillTree skillTreeData;

    private void Awake()
    {
        // Initialize the UI with configured branches
        skillTreeUI.Initialize(skillTreeData);
        // Refresh available points display
        skillTreeUI.UpdateAvailablePoints(rankSystem.AvailableSkillPoints);
    }

    void OnEnable()
    {
        skillTreeUI.OnNodePurchased += TryPurchase;
        RankSystem.OnRankUp += HandleRankUp;
    }
    void OnDisable()
    {
        skillTreeUI.OnNodePurchased -= TryPurchase;
        RankSystem.OnRankUp -= HandleRankUp;
    }

    private void HandleRankUp(RankConfig.RankEntry entry)
    {
        // Called whenever the player ranks up: update points display
        skillTreeUI.UpdateAvailablePoints(rankSystem.AvailableSkillPoints);
    }

    private void TryPurchase(SkillNode node)
    {
        if (node.isUnlocked) 
            return;

        if (node.parentNode != null && node.parentNode.isUnlocked == false)
            return;

        if (!rankSystem.SpendSkillPoints(node.costPoints))
            return;

        node.Unlock();
        skillTreeUI.UpdateAvailablePoints(rankSystem.AvailableSkillPoints);
    }
}