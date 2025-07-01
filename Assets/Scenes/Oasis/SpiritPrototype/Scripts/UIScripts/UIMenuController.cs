using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using StarterAssets;

public class UIMenuController : MonoBehaviour
{
    public GameObject uiMenuPanel;
    public Image[] relicSlots = new Image[4];
    public InputActionReference menuToggleAction;

    public FirstPersonController fpc;
    public RelicPickerUI relicPickerUI;

    private void Awake()
    {
        foreach (var slot in relicSlots)
        {
            slot.gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        menuToggleAction.action.Enable();
        menuToggleAction.action.performed += OnMenuToggle;
        RankSystem.OnRankUp += UpdateSlotDisplay;
    }

    void OnDisable()
    {
        menuToggleAction.action.performed -= OnMenuToggle;
        menuToggleAction.action.Disable();
        RankSystem.OnRankUp -= UpdateSlotDisplay;
    }

    private void OnMenuToggle(InputAction.CallbackContext _)
    {
        bool isOpening = !uiMenuPanel.activeSelf;
        if (!isOpening && relicPickerUI != null)
            relicPickerUI.Hide();

        uiMenuPanel.SetActive(!uiMenuPanel.activeSelf);
        Cursor.lockState = uiMenuPanel.activeSelf
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        fpc.enabled = !uiMenuPanel.activeSelf ? true : false;
    }

    private void UpdateSlotDisplay(RankConfig.RankEntry entry)
    {
        int slots = entry.relicSlotCount;
        for (int i = 0; i < relicSlots.Length; i++)
            relicSlots[i].gameObject.SetActive(i < slots);
    }
}