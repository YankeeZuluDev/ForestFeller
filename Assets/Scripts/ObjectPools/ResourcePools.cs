using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// This class is the collection of resource prefab pools
/// </summary>
public class ResourcePools : MonoBehaviour
{
    private static ResourcePools instance;

    [SerializeField] private List<ScriptableResource> pooledResourcesList;

    // Map scriptable resource to object pool
    private Dictionary<ScriptableResource, ObjectPool<GameObject>> resourcePoolsDictionary;

    public static ResourcePools Instance => instance;

    private void Awake()
    {
        // Singleton
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        InitializeResourcePoolsDictionary();
    }

    private void InitializeResourcePoolsDictionary()
    {
        resourcePoolsDictionary = new(pooledResourcesList.Count);

        foreach (ScriptableResource scriptableResource in pooledResourcesList)
        {
            ObjectPool<GameObject> resourcePool = new(() => Instantiate(scriptableResource.Prefab, transform), OnGet, OnRelease, OnKill, true, 3, 40);
            resourcePoolsDictionary.Add(scriptableResource, resourcePool);
        }
    }

    #region Pool

    private void OnGet(GameObject poolObject)
    {
        poolObject.SetActive(true);
    }

    private void OnRelease(GameObject poolObject)
    {
        poolObject.transform.SetParent(transform);
        poolObject.SetActive(false);
    }

    private void OnKill(GameObject poolObject)
    {
        Destroy(poolObject);
    }

    #endregion

    /// <summary>
    /// Get corresponding pool from scriptable resource
    /// </summary>
    public ObjectPool<GameObject> GetCorrespondingPool(ScriptableResource scriptableResource)
    {
        if (resourcePoolsDictionary.TryGetValue(scriptableResource, out ObjectPool<GameObject> pool))
            return pool;

        // Throw an exception if nothing was found
        throw new KeyNotFoundException($"No pool for {scriptableResource} found in dictionary");
    }
}
