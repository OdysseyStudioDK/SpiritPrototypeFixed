using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class RankUpUI : MonoBehaviour
{
    [Header("Panel & Text")]
    [Tooltip("The parent panel (disabled by default)")]
    public GameObject rankUpPanel;
    [Tooltip("TMP for the 'Rank Up! You are now X' text")]
    public TextMeshProUGUI rankUpText;

    [Header("Single Relic Reward UI")]
    [Tooltip("TMP for the relic's name under the panel")]
    public TextMeshProUGUI relicName;
    [Tooltip("Image for the relic's icon under the panel")]
    public Image relicIcon;
    [Tooltip("TMP for the relic's description under the panel")]
    public TextMeshProUGUI relicDescription;

    [Tooltip("How long to show the panel before hiding")]
    public float displayDuration = 2f;

    void OnEnable()
    {
        RankSystem.OnRankUp += HandleRankUp;
    }

    void OnDisable()
    {
        RankSystem.OnRankUp -= HandleRankUp;
    }

    private void HandleRankUp(RankConfig.RankEntry entry)
    {
        // 1) Show the rank title
        rankUpText.text = $"Rank Up!\nYou are now {entry.title}";

        // 2) If there's at least one relic reward, populate those fields
        if (entry.rewards != null && entry.rewards.Length > 0)
        {
            var r = entry.rewards[0];  // just the first
            relicName.text = r.relicName;
            relicIcon.sprite = r.icon;
            relicDescription.text = r.description;

            relicName.gameObject.SetActive(true);
            relicIcon.gameObject.SetActive(true);
            relicDescription.gameObject.SetActive(true);
        }
        else
        {
            // hide them if no reward
            relicName.gameObject.SetActive(false);
            relicIcon.gameObject.SetActive(false);
            relicDescription.gameObject.SetActive(false);
        }

        // 3) Show the entire panel
        rankUpPanel.SetActive(true);

        // 4) Hide after a delay
        StopAllCoroutines();
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        rankUpPanel.SetActive(false);
    }
}