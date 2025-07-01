using UnityEngine;

[DisallowMultipleComponent]
public class SpiritStateController : MonoBehaviour
{
    public SpiritState state;

    // Call in the Editor or at runtime to update the layer:
    void OnValidate() => ApplyLayer();
    void Awake() => ApplyLayer();

    public void SetState(SpiritState newState)
    {
        if (state == newState) return;
        state = newState;
        ApplyLayer();
    }

    private void ApplyLayer()
    {
        // must exactly match your layer names
        switch (state)
        {
            case SpiritState.Ghost:
                gameObject.layer = LayerMask.NameToLayer("Ghost");
                break;
            case SpiritState.Familiar:
                gameObject.layer = LayerMask.NameToLayer("Familiar");
                break;
            case SpiritState.Purified:
                gameObject.layer = LayerMask.NameToLayer("Purified");
                break;
        }
    }
}