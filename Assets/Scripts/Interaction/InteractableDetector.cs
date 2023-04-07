using UnityEngine;

/// <summary>
/// This class is used to detect interactables near the player
/// </summary>
public class InteractableDetector : MonoBehaviour, IResettable
{
    private IInteractable currentIntractable;

    public IInteractable CurrentInteractable => currentIntractable;

    private void OnTriggerEnter(Collider other)
    {
        currentIntractable = other.GetComponent<IInteractable>();
    }

    private void OnTriggerExit(Collider other)
    {
        currentIntractable = null;
    }

    public void ResetGameObject()
    {
        currentIntractable = null;
    }
}
