using UnityEngine;

/// <summary>
/// This is a class for resources, that are able to be placed on stack
/// </summary>
public class StackableResource : MonoBehaviour
{
    [SerializeField] private ScriptableResource scriptableResource;

    public ScriptableResource ScriptableResource => scriptableResource;
}