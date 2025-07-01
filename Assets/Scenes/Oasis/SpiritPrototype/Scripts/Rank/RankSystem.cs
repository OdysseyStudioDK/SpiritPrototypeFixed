using UnityEngine;

public class RankSystem : MonoBehaviour
{
    public RankConfig config;
    public RelicSystem relicSystem;
    [SerializeField] private int purifiedCount;
    private RankConfig.RankEntry currentRank;
    public static event System.Action<RankConfig.RankEntry> OnRankUp;

    void Awake()
    {
        // Ensure we have a RelicSystem to hand off relics to
        if (relicSystem == null)
            relicSystem = FindObjectOfType<RelicSystem>();
    }

    void OnEnable()
    {
        SpiritPurifier.SpiritPurified += HandlePurified;
    }
    void OnDisable()
    {
        SpiritPurifier.SpiritPurified -= HandlePurified;
    }

    private int spentSkillPoints = 0;

    // How many purifications you’ve done minus how many points you’ve spent.
    public int AvailableSkillPoints => purifiedCount - spentSkillPoints;

    // Try to spend X points; returns true if you had enough.
    public bool SpendSkillPoints(int amount)
    {
        if (amount <= AvailableSkillPoints)
        {
            spentSkillPoints += amount;
            return true;
        }
        Debug.Log($"Not enough skill points: have {AvailableSkillPoints}, need {amount}");
        return false;
    }

    private void HandlePurified(SpiritStateController controller)
    {
        purifiedCount++;
        Debug.Log($"[RankSystem] PurifiedCount = {purifiedCount} ? AvailableSkillPoints = {AvailableSkillPoints}");

        var newRank = config.GetRankForCount(purifiedCount);
        if (newRank.title != currentRank.title)
        {
            currentRank = newRank;

            // dynamically bump relic slots
            if (relicSystem != null)
            {
                relicSystem.maxSlots = newRank.relicSlotCount;
                Debug.Log($"[RankSystem] max relic slots set to {newRank.relicSlotCount}");
            }

            Debug.Log($"[RankSystem] Ranked up to {newRank.title}");
            OnRankUp?.Invoke(newRank);

            if (relicSystem != null && newRank.rewards != null)
            {
                foreach (var relic in newRank.rewards)
                {
                    relicSystem.UnlockRelic(relic);
                    Debug.Log($"[RankSystem] Unlocked relic: {relic.relicName}");
                }
            }
        }
    }
}