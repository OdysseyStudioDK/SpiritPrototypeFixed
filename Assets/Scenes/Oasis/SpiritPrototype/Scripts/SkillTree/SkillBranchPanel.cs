using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillBranchPanel : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text branchNameText;
    public Image branchIconImage;
    public Transform nodeContainer;
    public GameObject nodeButtonPrefab;

    private SOSkillBranch branchData;

    public void Setup(SOSkillBranch branch)
    {
        branchData = branch;
        if (branchNameText != null) branchNameText.text = branch.branchName;
        if (branchIconImage != null) branchIconImage.sprite = branch.icon;

        // Clear any existing nodes
        foreach (Transform child in nodeContainer)
            Destroy(child.gameObject);

        // Recursively build nodes starting from root
        BuildNodeUI(branch.rootNode, nodeContainer);
    }

    private void BuildNodeUI(SkillNode node, Transform parent)
    {
        var nodeGO = Instantiate(nodeButtonPrefab, parent);
        var button = nodeGO.GetComponent<SkillNodeButton>();
        button.Setup(node);

        // Setup click callback
        button.onClick += () =>
        {
            FindObjectOfType<SkillTreeUI>().NodeClicked(node);
        };

        // Recursively for each child
        if (node.children != null && node.children.Length > 0)
        {
            var childContainer = new GameObject("ChildContainer").transform;
            childContainer.SetParent(nodeGO.transform);
            childContainer.localPosition = new Vector3(0, -75, 0);
            foreach (var child in node.children)
                BuildNodeUI(child, childContainer);
        }
    }
}