using UnityEngine;
using UnityEngine.InputSystem;

public class SpiritPurifier : MonoBehaviour
{
    [Tooltip("Drag in your Input Action for Purify (from your generated controls).")]
    public InputActionReference purifyAction;

    [Tooltip("Max distance for purify action.")]
    public float purificationRange = 5f;

    // Fires when a spirit is successfully purified
    public static event System.Action<SpiritStateController> SpiritPurified;

    void OnEnable()
    {
        purifyAction.action.Enable();
        purifyAction.action.performed += OnPurify;
    }

    void OnDisable()
    {
        purifyAction.action.performed -= OnPurify;
        purifyAction.action.Disable();
    }

    private void OnPurify(InputAction.CallbackContext ctx)
    {
        TryPurify();
    }

    private void TryPurify()
    {
        // Find all colliders in a sphere around the purifier
        Collider[] hits = Physics.OverlapSphere(Camera.main.transform.position, purificationRange);

        // Optionally, sort by distance so you purify the closest ghost first
        System.Array.Sort(hits, (a, b) =>
            (a.transform.position - transform.position).sqrMagnitude
            .CompareTo((b.transform.position - transform.position).sqrMagnitude)
        );

        // Loop through each collider
        foreach (var col in hits)
        {
            // Try to find your SpiritStateController on that object (or its parent)
            var controller = col.GetComponentInParent<SpiritStateController>();
            if (controller == null)
            {
                continue;
            }

            // Only purify if it's still a Ghost
            if (controller.state == SpiritState.Ghost)
            {
                controller.SetState(SpiritState.Purified);
                SpiritPurified?.Invoke(controller);
                return;
            }
        }
    }
}