using UnityEngine;
using TMPro;

/// <summary>
/// This is a class for 3D resource icon, floating above factory and truck
/// </summary>
public class ResourceIcon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer iconSpriteRenderer;
    [SerializeField] private TextMeshPro iconText;

    private int resourceAmount;

    public SpriteRenderer IconSpriteRenderer { get => iconSpriteRenderer; set => iconSpriteRenderer = value; }
    public TextMeshPro IconText { get => iconText; set => iconText = value; }
    public int ResourceAmount { get => resourceAmount; set => resourceAmount = value; }

    public void DecreaseResourceAmountText(int amount)
    {
        // Decrease amount from current amount
        resourceAmount -= amount;

        // Ensurem that displayed resource amount is not negative
        resourceAmount = Mathf.Max(0, resourceAmount);

        // Update text
        iconText.text = resourceAmount.ToString();
    }
}
