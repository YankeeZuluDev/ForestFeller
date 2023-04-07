using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This is a class for 2D resource icon in UI
/// </summary>
public class UIResourceIcon : MonoBehaviour, IResettable
{
    [SerializeField] private Image resourceIconImage;
    [SerializeField] private TextMeshProUGUI resourceIconText;

    private int resourceAmount = 0;

    public Image ResourceIconImage { get => resourceIconImage; set => resourceIconImage = value; }

    public void UpdateResourceAmountText(int amount)
    {
        resourceAmount += amount;
        resourceIconText.text = resourceAmount.ToString();
    }

    public void ResetGameObject()
    {
        resourceAmount = 0;
        resourceIconText.text = "0";
    }

}
