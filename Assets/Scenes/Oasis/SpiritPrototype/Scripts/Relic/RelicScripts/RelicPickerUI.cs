using UnityEngine;
using UnityEngine.UI;

public class RelicPickerUI : MonoBehaviour
{
    [Tooltip("Container for the instantiated picker buttons")]
    public Transform content;

    [Tooltip("Prefab: a Button (with Image) representing one unlocked relic")]
    public GameObject pickerButtonPrefab;

    [Tooltip("Reference to your RelicSystem")]
    public RelicSystem relicSystem;

    [Header("Slot Icon Images")]
    [Tooltip("Assign the child 'Icon' Image from each of your 4 slot GameObjects here, in order 0–3")]
    public Image[] slotIcons = new Image[4];

    // Keeps track of which slot the player clicked (03)
    private int currentSlotIndex;

    // Cache of this panel's RectTransform for outside?click detection
    private RectTransform _rect;

    void Awake()
    {
        relicSystem = relicSystem ?? FindObjectOfType<RelicSystem>();
        _rect = GetComponent<RectTransform>();
        Hide();
    }

    /// <summary>
    /// Opens the picker for a given slot index (0–3), rebuilds the list
    /// but only shows relics that are unlocked and not already equipped.
    /// </summary>
    public void Show(int slotIndex)
    {
        currentSlotIndex = slotIndex;

        // 1) Clear out any old buttons
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }

        // 2) Loop through unlocked relics, but skip any that are already equipped
        foreach (var relic in relicSystem.unlocked)
        {
            if (relicSystem.equipped.Contains(relic))
                continue;

            // 3) Instantiate a new button for this relic
            var btnGO = Instantiate(pickerButtonPrefab, content);
            var img = btnGO.GetComponent<Image>();
            img.sprite = relic.icon;

            var btn = btnGO.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                // If that slot already had something, unequip it first
                if (currentSlotIndex < relicSystem.equipped.Count)
                {
                    var old = relicSystem.equipped[currentSlotIndex];
                    relicSystem.Unequip(old);
                }

                // Equip the newly chosen relic
                relicSystem.Equip(relic);

                // Immediately update the slot?icon graphic
                UpdateSlotIcon(relic.icon);

                // And then hide the picker
                Hide();
            });
        }

        gameObject.SetActive(true);
    }

    private void UpdateSlotIcon(Sprite icon)
    {
        if (currentSlotIndex < 0 || currentSlotIndex >= slotIcons.Length)
        {
            Debug.LogWarning($"RelicPickerUI: invalid slot index {currentSlotIndex}");
            return;
        }

        var iconImg = slotIcons[currentSlotIndex];
        if (iconImg == null)
        {
            Debug.LogWarning($"RelicPickerUI: slotIcons[{currentSlotIndex}] is null!");
            return;
        }

        iconImg.sprite = icon;
        iconImg.color = Color.white;
        iconImg.enabled = true;
    }

    /// <summary>
    /// Hides the entire picker panel.
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}