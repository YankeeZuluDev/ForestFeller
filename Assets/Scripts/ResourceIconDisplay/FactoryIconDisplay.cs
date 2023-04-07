using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for displaying resource icons above the factory
/// </summary>
public class FactoryIconDisplay : MonoBehaviour
{
    [SerializeField] private List<Sprite> iconSprites;
    [SerializeField] private List<ResourceIcon> resourceIcons;

    private void Start()
    {
        DisplayIcons();
    }

    private void DisplayIcons()
    {
        for (int i = 0; i < resourceIcons.Count; i++)
        {
            resourceIcons[i].IconSpriteRenderer.sprite = iconSprites[i];
        }
    }
}
