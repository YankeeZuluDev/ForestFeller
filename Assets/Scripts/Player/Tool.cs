using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for showing the right tool for the given resource deposit
/// </summary>
public class Tool : MonoBehaviour
{
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject pickaxe;

    private GameObject currentTool;

    private void Awake()
    {
        currentTool = axe;
        currentTool.SetActive(true);
    }


    public void SetTool(ResourceDeposit resourceDeposit)
    {
        // Set current tool depending on ResourceDeposit type

        // Hide old tool
        currentTool.SetActive(false);

        currentTool = resourceDeposit.ResourceItem.Resource switch // пусть принемает ресурс как аргумент, а не deposit
        {
            Wood => axe,
            Stone => pickaxe,
            _ => throw new KeyNotFoundException($"No tool for {resourceDeposit} resource deposit was found"),
        };

        // Show new tool
        currentTool.SetActive(true);
    }
}
