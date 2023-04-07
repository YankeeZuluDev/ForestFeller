using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An interface for resource receiver class
/// </summary>
public interface IResourceReceiver
{
    List<ScriptableResource> Receivables { get; set; }
    Transform ReceiverTransform { get; }
    IInteractable Interactable { get; }
    void Receive(ScriptableResource scriptableResource, int amount);
    void RemoveReceivable(ScriptableResource scriptableResource);
}
