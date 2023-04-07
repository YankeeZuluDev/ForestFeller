using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for displaying resource icons in UI
/// </summary>
public class UIIconDisplay : MonoBehaviour, IResettable
{
    [SerializeField] private List<ScriptableResource> resources;
    [SerializeField] private List<UIResourceIcon> resourceIcons;

    private void Awake() => SetInitialIconSprites();

    private void SetInitialIconSprites()
    {
        for (int i = 0; i < resourceIcons.Count; i++)
        {
            resourceIcons[i].ResourceIconImage.sprite = resources[i].Icon;
        }
    }

    public void UpdateResourceAmountText(ScriptableResource scriptableResource, int amount)
    {
        // Get corresponding icon
        UIResourceIcon resourceIcon = GetCorrespondingIcon(scriptableResource);

        // Update text on corresponding icon
        resourceIcon.UpdateResourceAmountText(amount);
    }

    private UIResourceIcon GetCorrespondingIcon(ScriptableResource scriptableResource)
    {
        foreach (UIResourceIcon resourceIcon in resourceIcons)
        {
            if (resourceIcon.ResourceIconImage.sprite == scriptableResource.Icon)
                return resourceIcon;
        }

        // Throw an exception if nothing was found
        throw new KeyNotFoundException($"No icon for {scriptableResource} found in icon list");
    }

    public void ResetGameObject()
    {
        foreach (UIResourceIcon resourceIcon in resourceIcons)
        {
            resourceIcon.ResetGameObject();
        }
    }
}
