using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// This class is used to store received resources
/// </summary>
public class Storage : MonoBehaviour, IStorage, IResettable
{
    // link scriptable resource and storage item
    private Dictionary<ScriptableResource, StorageItem> resourceToStorageItemDictionary = new();
    //private List<StorageItem> storage = new();

    public void Add(ScriptableResource scriptableResource, int amount)
    {
        // Get corresponding storage item in storage
        if (!TryGetCorrespondingStorageItem(scriptableResource, out StorageItem storageItem))
        {
            // Create new item if it doesn`t exist in storage
            storageItem = new StorageItem();
            storageItem.Resource = scriptableResource;
            resourceToStorageItemDictionary.Add(scriptableResource, storageItem);
        }

        // Increase amount of storage item
        storageItem.Amount += amount;
    }

    public void Remove(ScriptableResource scriptableResource, int amount)
    {
        // Get corresponding item in storage
        if (TryGetCorrespondingStorageItem(scriptableResource, out StorageItem storageItem))
        {
            // Decerease amount of storage item
            storageItem.Amount -= amount;
        }
    }

    public bool TryGetCorrespondingStorageItem(ScriptableResource scriptableResource, out StorageItem correspondingStorageItem)
    {
        if (resourceToStorageItemDictionary.TryGetValue(scriptableResource, out StorageItem storageItem))
        {
            correspondingStorageItem = storageItem;
            return true;
        }

        // Return null if nothing was founds
        correspondingStorageItem = null;
        return false;
    }

    public void ResetGameObject()
    {
        resourceToStorageItemDictionary.Clear();
    }
}
