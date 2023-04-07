using UnityEngine;

/// <summary>
/// This class is responsible for passing resource data from player to UI icon display
/// </summary>
public class PlayerIconDisplay : MonoBehaviour
{
    private UIIconDisplay uIIconDisplay;

    public void InitializePlayerIconDisplay(UIIconDisplay uIIconDisplay)
    {
        this.uIIconDisplay = uIIconDisplay;
    }

    public void AddResourceUIAmount(ScriptableResource scriptableResource, int amount)
    {
        // Add amount of resource to resource UI icon text
        uIIconDisplay.UpdateResourceAmountText(scriptableResource, amount);
    }

    public void RemoveResourceUIAmount(ScriptableResource scriptableResource, int amount)
    {
        // Remove amount of resource from resource UI icon text
        uIIconDisplay.UpdateResourceAmountText(scriptableResource, -amount);
    }
}
