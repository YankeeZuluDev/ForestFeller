using UnityEngine;

/// <summary>
/// This is a class for all resource deposits such as trees and rocks
/// </summary>
public class ResourceDeposit : Spawnable, IInteractable, IResettable
{
    [SerializeField] private StorageItem resourceItem;
    [SerializeField] private GameObject stackPrefab;

    public StorageItem ResourceItem => resourceItem;
    public bool DisableInteraction { get; set; }

    public void TakeDown()
    {
        DisableInteraction = true;

        // Spawn resource stack
        IResourceReceiver stackReceiver = Instantiate(stackPrefab, transform.position, transform.rotation).GetComponent<IResourceReceiver>();

        // Initialize resource stack
        stackReceiver.Receive(resourceItem.Resource, resourceItem.Amount);

        // Destroy
        Destroy(gameObject);
    }

    public void ResetGameObject()
    {
        Destroy(gameObject);
    }
}
