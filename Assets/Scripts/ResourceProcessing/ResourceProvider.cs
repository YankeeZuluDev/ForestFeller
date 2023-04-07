using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

/// <summary>
/// This class is responsible for sending resources to a receiver. Raises the "onDispensed" UnityEvent when the resources are sent
/// </summary>

[RequireComponent(typeof(Storage))]

public class ResourceProvider : MonoBehaviour, IResourceProvider
{
    [SerializeField] private List<ScriptableResource> dispansables;
    [SerializeField] private float dispenseAnimationDuration;
    [Space()]
    [SerializeField] private UnityEvent<ScriptableResource, int> onDispensed;

    private IStorage storage;
    private IInteractable interactable;

    public Transform ProviderTransform => transform;
    public List<ScriptableResource> Dispansables => dispansables;

    private void Awake()
    {
        storage = GetComponent<Storage>();
        interactable = GetComponent<IInteractable>();
    }

    public void Dispense(IResourceReceiver otherReceiver)
    {
        // Get list of receivable storage items
        List<StorageItem> receivableStorageItems = GetReceivableStorageItems(otherReceiver);

        // Exit if list is empty
        if (receivableStorageItems.Count == 0) return;

        // Send resources to other receiver
        SendToOtherReceiver(receivableStorageItems, otherReceiver);
    }

    private void SendToOtherReceiver(List<StorageItem> itemsToSend, IResourceReceiver otherReceiver)
    {
        // Send to other receiver
        foreach (StorageItem receivableStorageItem in itemsToSend)
        {
            StartCoroutine(PlayDispenseAnimation(receivableStorageItem.Resource, receivableStorageItem.Amount, otherReceiver));

            // Send to other receiver
            otherReceiver.Receive(receivableStorageItem.Resource, receivableStorageItem.Amount);

            // Invoke onDispensed event
            onDispensed?.Invoke(receivableStorageItem.Resource, receivableStorageItem.Amount);

            // Remove from the storage
            storage.Remove(receivableStorageItem.Resource, receivableStorageItem.Amount);
        }
    }

    private IEnumerator PlayDispenseAnimation(ScriptableResource scriptableResource, int amount, IResourceReceiver otherReceiver)
    {
        // Disable interaction on provider and other receiver while dispense animation is playing
        interactable.DisableInteraction = true;
        otherReceiver.Interactable.DisableInteraction = true;

        // Get pool for given resource
        ObjectPool<GameObject> pool = ResourcePools.Instance.GetCorrespondingPool(scriptableResource);

        // Play animation
        for (int i = 0; i < amount; i++)
        {
            GameObject tweenResource = pool.Get();

            tweenResource.transform.position = transform.position;

            yield return Tweens.SmoothlyMoveAndRotateTowards(tweenResource.transform, otherReceiver.ReceiverTransform, dispenseAnimationDuration);

            AudioManager.Instance.PlaySFX(AudioID.Pop);

            pool.Release(tweenResource);
        }

        // Enable interaction
        interactable.DisableInteraction = false;
        otherReceiver.Interactable.DisableInteraction = false;
    }

    private List<StorageItem> GetReceivableStorageItems(IResourceReceiver otherReceiver)
    {
        List<StorageItem> receivableStorageItems = new();

        // Get list of corresponding storage items
        foreach (ScriptableResource receivable in otherReceiver.Receivables)
        {
            if (storage.TryGetCorrespondingStorageItem(receivable, out StorageItem receivableStorageItem))
            {
                receivableStorageItems.Add(receivableStorageItem);
            }
        }

        return receivableStorageItems;
    }
}
