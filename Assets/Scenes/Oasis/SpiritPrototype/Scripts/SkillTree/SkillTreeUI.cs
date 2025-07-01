using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeUI : MonoBehaviour
{
    [Header("UI References")]
    public Transform branchContainer; // where branch panels will be instantiated
    public TMP_Text availablePointsText;
    public GameObject branchPanelPrefab; // prefab to display a branch

    private SOSkillTree skillTreeData;

    /// <summary>
    /// Call once at startup to build UI from ScriptableObject data
    /// </summary>
    public void Initialize(SOSkillTree tree)
    {
        skillTreeData = tree;
        // Clear existing children
        foreach (Transform child in branchContainer)
            Destroy(child.gameObject);

        // Instantiate a panel for each branch
        foreach (var branch in skillTreeData.branches)
        {
            var panelGO = Instantiate(branchPanelPrefab, branchContainer);
            var panel = panelGO.GetComponent<SkillBranchPanel>();
            panel.Setup(branch);
            Debug.Log(tree.name);
        }
    }

    /// <summary>
    /// Update the displayed available skill points
    /// </summary>
    public void UpdateAvailablePoints(int points)
    {
        if (availablePointsText != null)
            availablePointsText.text = $"Points: {points}";
    }

    // Event to notify when user clicks a node
    public event System.Action<SkillNode> OnNodePurchased;
    public void NodeClicked(SkillNode node)
    {
        OnNodePurchased?.Invoke(node);
    }
}