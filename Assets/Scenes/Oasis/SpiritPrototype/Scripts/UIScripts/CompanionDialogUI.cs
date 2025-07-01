using UnityEngine;
using TMPro;
using System.Collections;

public class CompanionDialogUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogPanel;  // Can be found in the hiarchy
    public TextMeshProUGUI dialogText;  // TextMeshPro under dialog panel
    public float displayDuration = 3f;

    private PlayerReputation _rep;

    void Awake()
    {
        dialogPanel.SetActive(false);
    }

    void Start()
    {
        // Assumes ReputationSystem is in the scene
        _rep = FindObjectOfType<ReputationSystem>().reputation;
        _rep.OnMilestoneReached += ShowDialog;
    }

    void OnDestroy()
    {
        _rep.OnMilestoneReached -= ShowDialog;
    }

    private void ShowDialog(TextAsset clip)
    {
        dialogText.text = clip.text;
        dialogPanel.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        dialogPanel.SetActive(false);
    }
}