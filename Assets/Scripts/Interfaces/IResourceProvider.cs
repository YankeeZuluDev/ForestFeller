using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An interface for resource provider class
/// </summary>
public interface IResourceProvider
{
    List<ScriptableResource> Dispansables { get; }
    Transform ProviderTransform { get; }
    void Dispense(IResourceReceiver otherReceiver);
}
