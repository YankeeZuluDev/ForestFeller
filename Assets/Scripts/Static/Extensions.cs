using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A collection of extension methods
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Convert a list of storage items to the list of scriptable resoruces
    /// </summary>
    /// <returns>A list of scriptable resouces</returns>
    public static List<ScriptableResource> ToResourceList(this List<StorageItem> storageItems)
    {
        List<ScriptableResource> scriptableResources = new(storageItems.Count);

        foreach (StorageItem storageItem in storageItems)
        {
            scriptableResources.Add(storageItem.Resource);
        }

        return scriptableResources;
    }

    /// <summary>
    /// An extension method, that checks if a list of storage items has any duplicate scriptable resources
    /// </summary>
    /// <returns>True if duplicate resource was found, false if not</returns>
    public static bool HasAnyDuplicateResources(this List<StorageItem> storageItemsList, out ScriptableResource duplicateResource)
    {
        for (int i = 0; i < storageItemsList.Count; i++)
        {
            // Get current storage item
            ScriptableResource currentResource = storageItemsList[i].Resource;

            for (int j = i + 1; j < storageItemsList.Count; j++)
            {
                // Get other resource
                ScriptableResource otherResource = storageItemsList[j].Resource;

                // Return true if audio IDs are matching
                if (currentResource == otherResource)
                {
                    duplicateResource = otherResource;
                    return true;
                }
            }
        }

        // Return false if nothing was found
        duplicateResource = null;
        return false;
    }

    /// <summary>
    /// An extension method, that checks if a list of audio ID-Clip pairs list has any duplicate audioID`s
    /// </summary>
    /// <returns>True if duplicate audio ID was found, false if not</returns>
    public static bool HasAnyDuplicateAudioIDs(this List<AudioIDClipPair> audioIDClipPairsList, out AudioID? duplicateID)
    {
        for (int i = 0; i < audioIDClipPairsList.Count; i++)
        {
            // Get current ID
            AudioID currentID = audioIDClipPairsList[i].ID;

            for (int j = i + 1; j < audioIDClipPairsList.Count; j++)
            {
                // Get other ID
                AudioID otherID = audioIDClipPairsList[j].ID;

                // Return true if audio IDs are matching
                if (currentID == otherID)
                {
                    duplicateID = otherID;
                    return true;
                }
            }
        }

        // Return false if nothing was found
        duplicateID = null;
        return false;
    }

    /// <summary>
    /// An extension method, that checks if a list of storage items has empty resources
    /// </summary>
    /// <returns>True if empty resource was found, false if not</returns>
    public static bool HasEmptyResource(this List<StorageItem> storageItemsList)
    {
        for (int i = 0; i < storageItemsList.Count; i++)
        {
            ScriptableResource currentResource = storageItemsList[i].Resource;

            // Return true if empty
            if (currentResource == null)
                return true;
        }

        // Return false if nothing was found
        return false;
    }
}
