using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillNodeButton : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text nodeNameText;
    public TMP_Text costText;
    public Image iconImage;
    public Button button;

    // Fired when this node button is clicked
    public event Action onClick;

    private SkillNode nodeData;

    public void Setup(SkillNode node)
    {
        nodeData = node;
        if (nodeNameText != null)
            nodeNameText.text = node.nodeName;
        if (costText != null)
            costText.text = node.costPoints.ToString();
        if (iconImage != null && node.icon != null)
            iconImage.sprite = node.icon;

        UpdateVisualState(node);

        nodeData.OnUnlocked += HandleUnlocked;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick?.Invoke());

        if (node.isUnlocked)
            button.interactable = false;

        if (node.parentNode != null)
        {
            if (!node.parentNode.isUnlocked)
            {
                button.interactable = false;
                var btnColor = button.colors;
                btnColor.disabledColor = Color.red;
                button.colors = btnColor;
            }
        }
    }

    private void HandleUnlocked(SkillNode node)
    {
        UpdateVisualState(node);
        UpdateBranch(node);
        UpdateButton();
    }

    private void UpdateVisualState(SkillNode node)
    {
        var colors = button.colors;
        colors.disabledColor = node.isUnlocked ? new Color(0.5f, 0.5f, 0.5f) : colors.disabledColor;
        button.colors = colors;
    }

    private void UpdateButton()
    {
        button.interactable = false;
    }

    private void UpdateBranch(SkillNode node)
    {
        if (node.children != null)
        {
            for (int i = 0; i < node.children.Length; i++)
            {
                var child = node.children[i];
                SkillNodeButton childButton = FindButtonForNode(child);
                if (childButton != null)
                {
                    childButton.button.interactable = true;
                }
            }
        }
    }

    private SkillNodeButton FindButtonForNode(SkillNode targetNode)
    {
        SkillNodeButton[] allButtons = FindObjectsOfType<SkillNodeButton>(true);
        foreach (var btn in allButtons)
        {
            if (btn != null && btn.nodeData == targetNode)
                return btn;
        }
        return null;
    }


    void OnDestroy()
    {
        if (nodeData != null)
            nodeData.OnUnlocked -= HandleUnlocked;
    }
}