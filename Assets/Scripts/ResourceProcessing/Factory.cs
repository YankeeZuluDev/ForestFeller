using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is responsible for producing a ScriptableResource and dispensing it to a ResourceReceiver stack
/// </summary>

[RequireComponent(typeof(Storage))]
[RequireComponent(typeof(ResourceProvider))]

public class Factory : Spawnable, IInteractable, IResettable
{
    [SerializeField] private ScriptableResource produced;
    [SerializeField] private ResourceReceiver stackReceiver;
    [Space()]
    [SerializeField] private UnityEvent<ScriptableResource, int> onProduced;

    private IStorage storage;
    private IResourceProvider provider;

    public bool DisableInteraction { get; set; }

    private void Awake()
    {
        storage = GetComponent<Storage>();
        provider = GetComponent<ResourceProvider>();
    }

    public void Produce(ScriptableResource scriptableResource, int amount)
    {
        // Add produced to storage
        storage.Add(produced, amount);

        // Invoke onProduced event
        onProduced?.Invoke(produced, amount);

        // Dispanse to production stack
        provider.Dispense(stackReceiver);

        // Remove from storage
        storage.Remove(scriptableResource, amount);
    }

    public void ResetGameObject()
    {
        Destroy(gameObject);
    }
}
