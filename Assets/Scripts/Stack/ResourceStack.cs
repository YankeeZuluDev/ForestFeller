using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// This class handles adding and removing resources from traversable stack
/// </summary>
public class ResourceStack : MonoBehaviour, IResourceStack, IInteractable, IResettable
{
    [SerializeField] private float spawnOffset;
    [SerializeField] private Transform stackParent;
    [SerializeField] private Transform topTransform;

    [Header("Adding and removing duration")]
    [SerializeField] private float addingDuration;
    [SerializeField] private float removingDuration;

    [Header("Stack height and width")]
    [SerializeField] private int stackHeight;
    [SerializeField] private int stackWidth;

    private TraversableStack traversableStack = new TraversableStack();
    private StackPositionHelper stackPositionHelper;

    public bool DisableInteraction { get; set; }

    private void Awake()
    {
        // Initialize
        stackPositionHelper = new StackPositionHelper(topTransform, spawnOffset, stackHeight, stackWidth);
    }

    public void Add(ScriptableResource scriptableResource, int amount)
    {
        StartCoroutine(AddCoroutine(scriptableResource, amount));
    }

    public void Remove(ScriptableResource scriptableResource, int amount)
    {
        StartCoroutine(RemoveCoroutine(scriptableResource, amount));
    }

    public IEnumerator AddCoroutine(ScriptableResource scriptableResource, int amount) // from interactable
    {
        // Get corresponding pool for scriptable resource
        ObjectPool<GameObject> resourcePool = ResourcePools.Instance.GetCorrespondingPool(scriptableResource);

        for (int i = 0; i < amount; i++)
        {
            // Wait before adding again
            yield return new WaitForSeconds(addingDuration);

            SpawnResourceOnTop(resourcePool);
        }
    }

    public IEnumerator RemoveCoroutine(ScriptableResource scriptableResource, int amount) // to interactabe
    {
        // Get all stackable resources to remove
        List<StackableResource> stackableResourcesToRemove = traversableStack.FindAll(scriptableResource);

        if (stackableResourcesToRemove.Count <= 0) yield break;

        // Get corresponding pool for scriptable resource
        ObjectPool<GameObject> resourcePool = ResourcePools.Instance.GetCorrespondingPool(scriptableResource);

        foreach (StackableResource stackableResource in stackableResourcesToRemove)
        {
            DeleteStackableResource(resourcePool, stackableResource);

            // wait before removing again
            yield return new WaitForSeconds(removingDuration);
        }

        stackPositionHelper.UpdateResourcePositions(traversableStack);
    }

    private void SpawnResourceOnTop(ObjectPool<GameObject> resourcePool)
    {
        // Get stackable resource from pool
        StackableResource stackableResource = resourcePool.Get().GetComponent<StackableResource>(); // TODO: kepp StackableResources in pool instead of GameObjects

        // Place stackable resource on top and set it as a child of stack
        stackableResource.transform.SetPositionAndRotation(topTransform.position, topTransform.rotation);
        stackableResource.transform.SetParent(stackParent);

        // Add to stack
        traversableStack.Push(stackableResource);

        // Increase top
        stackPositionHelper.IncreaseTopPosition();

        // Go to next stack column if height limit reached
        if (traversableStack.Count % stackHeight == 0) // 0 % 3 = 0
        {
            stackPositionHelper.NextStackColumn();
        }
    }

    private void DeleteStackableResource(ObjectPool<GameObject> resourcePool, StackableResource stackableResource)
    {
        // Go to previous stack column if height limit reached
        if (traversableStack.Count % (stackHeight ) == 0 && traversableStack.Count != 0)
        {
            stackPositionHelper.PreviousStackColumn();
        }

        // Remove from stack
        traversableStack.Remove(stackableResource);

        // Return stackable resource to pool
        resourcePool.Release(stackableResource.gameObject);

        // Decrease top
        stackPositionHelper.DecreaseTopPosition();
    }

    public void ResetGameObject()
    {
        // Clear traversable stack, iterate backwards to allow collection modification while iterating
        for (int i = traversableStack.Count - 1; i >= 0; i--)
        {
            // Pop the resource gameobject
            GameObject poppedGameObject = traversableStack.Pop().gameObject;

            // Get popped resource
            ScriptableResource poppedScriptableResource = poppedGameObject.GetComponent<StackableResource>().ScriptableResource;

            // Get pool for popped resoruce
            ObjectPool<GameObject> resourcePool = ResourcePools.Instance.GetCorrespondingPool(poppedScriptableResource);

            // Release popped resource
            resourcePool.Release(poppedGameObject);
        }

        stackPositionHelper.ResetTopTransformLocalPosition();
    }
}
