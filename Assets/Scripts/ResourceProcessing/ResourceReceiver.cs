using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is responsible for receiving resources. Adds the received resources to the storage. Raises the "onReceived" UnityEvent when the resources are received
/// </summary>

[RequireComponent(typeof(Storage))]

public class ResourceReceiver : MonoBehaviour, IResourceReceiver
{
    [SerializeField] private List<ScriptableResource> receivables;
    [Space()]
    [SerializeField] private UnityEvent<ScriptableResource, int> onReceived;

    private IStorage storage;
    private IInteractable interactable;

    public Transform ReceiverTransform => transform;
    public IInteractable Interactable => interactable;
    public List<ScriptableResource> Receivables { get => receivables; set => receivables = value; }

    private void Awake()
    {
        storage = GetComponent<Storage>();
        interactable = GetComponent<IInteractable>();
    }

    public void Receive(ScriptableResource scriptableResource, int amount)
    {
        // Check if resource is valid
        if (!receivables.Contains(scriptableResource)) return;

        // Add to storage
        storage.Add(scriptableResource, amount);

        // Invoke onReceivedMultiple event
        onReceived?.Invoke(scriptableResource, amount);
    }

    public void RemoveReceivable(ScriptableResource scriptableResource)
    {
        Debug.Log("removing receivable " + scriptableResource);
        receivables.Remove(scriptableResource);
    }
}
