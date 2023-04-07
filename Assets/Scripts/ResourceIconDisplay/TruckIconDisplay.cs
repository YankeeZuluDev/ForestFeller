using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// This class is responsible for displaying resource icons above the truck and updating text on it
/// </summary>

[RequireComponent(typeof(ResourceReceiver))]
[RequireComponent(typeof(LevelGoalChecker))]

public class TruckIconDisplay : MonoBehaviour
{
    [SerializeField] private GameObject iconBackgroundPrefab;
    [SerializeField] private float iconAreaYOffset;

    private List<StorageItem> levelGoal;
    private List<ResourceIcon> resourceIcons = new();

    public void InitialzieTruckIconDisplay(List<StorageItem> levelGoal)
    {
        this.levelGoal = levelGoal;
    }

    private void Start()
    {
        List<Sprite> iconSrites = GetIcons();
        SpawnIcons(iconSrites);
        DestributeIcons();
        SetInitialIResourceAmount();
    }

    private List<Sprite> GetIcons()
    {
        List<Sprite> iconSprites = new();

        // Get icons from the level goal
        foreach (StorageItem levelGoal in levelGoal)
        {
            // Add to the list of icon sprites
            iconSprites.Add(levelGoal.Resource.Icon);
        }

        return iconSprites;
    }

    private void SpawnIcons(List<Sprite> iconSprites)
    {
        for (int i = 0; i < iconSprites.Count; i++)
        {
            // Calculate icon posiiton
            Vector3 iconPosition = new Vector3(transform.position.x, transform.position.y + iconAreaYOffset, transform.position.z);

            // Spawn icon 
            ResourceIcon resourceIcon = Instantiate(iconBackgroundPrefab, iconPosition, iconBackgroundPrefab.transform.rotation, transform).GetComponent<ResourceIcon>();

            // Set icon sprite
            resourceIcon.IconSpriteRenderer.sprite = iconSprites[i];

            // Add icon transform to the list
            resourceIcons.Add(resourceIcon);
        }
    }

    private void DestributeIcons()
    {
        // Calculate area width
        float iconAreaWidth = iconBackgroundPrefab.transform.localScale.x * (resourceIcons.Count - 1);
        float halfAreaWidth = iconAreaWidth / 2f;

        // Get center x
        float centerPositionX = transform.position.x;

        // Calculate area start and end x positions
        float startPositionX = centerPositionX - halfAreaWidth;
        float endPositionX = centerPositionX + halfAreaWidth;

        // Distribute icon transforms equally along the icon area
        for (int i = 0; i < resourceIcons.Count; i++)
        {
            // Calculate distance fraction
            float distanceFraction = i / (float)(resourceIcons.Count - 1);

            // Set icon position
            Vector3 iconPosition = resourceIcons[i].transform.position;
            iconPosition.x = Mathf.Lerp(startPositionX, endPositionX, distanceFraction);
            resourceIcons[i].transform.position = iconPosition;
        }
    }

    private void SetInitialIResourceAmount()
    {
        // Set initial icon text
        foreach (StorageItem storageItem in levelGoal)
        {
            // Get corresponding icon
            ResourceIcon resourceIcon = GetCorrespondingIcon(storageItem.Resource);

            // Set amount
            resourceIcon.ResourceAmount = storageItem.Amount;

            // Set amount text
            resourceIcon.IconText.text = storageItem.Amount.ToString();
        }
    }

    public void DecreaseResourceAmount(ScriptableResource scriptableResource, int decreaseAmount)
    {
        // Exit if decrease amount is zero
        if (decreaseAmount == 0) return;

        // Get corresponding icon
        ResourceIcon resourceIcon = GetCorrespondingIcon(scriptableResource);

        resourceIcon.DecreaseResourceAmountText(decreaseAmount);
    }

    private ResourceIcon GetCorrespondingIcon(ScriptableResource scriptableResource)
    {
        foreach (ResourceIcon resourceIcon in resourceIcons)
        {
            if (resourceIcon.IconSpriteRenderer.sprite == scriptableResource.Icon)
                return resourceIcon;
        }

        // Throw an exception if nothing was found
        throw new KeyNotFoundException($"No icon for {scriptableResource} found in icon list");
    }
}
