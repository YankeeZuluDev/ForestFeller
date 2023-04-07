using System.Collections.Generic;

/// <summary>
/// This class is custom traversable stack data structure implementation
/// </summary>
public class TraversableStack
{
    private List<StackableResource> stackableResources = new List<StackableResource>();

    public List<StackableResource> StackableResources => stackableResources;

    public int Count => stackableResources.Count;

    /// <summary>
    /// Push StackableResource to the top of the stack
    /// </summary>
    public void Push(StackableResource stackableResource)
    {
        stackableResources.Add(stackableResource);
    }

    /// <summary>
    /// Get StackableResource on the top
    /// </summary>
    /// <returns></returns>
    public StackableResource Peek()
    {
        if (stackableResources.Count <= 0) throw new System.IndexOutOfRangeException("Stack count is less or equal to zero");

        return stackableResources[stackableResources.Count - 1];
    }

    public StackableResource Pop()
    {
        if (stackableResources.Count <= 0) throw new System.IndexOutOfRangeException("Stack count is less or equal to zero");

        int lastIndex = stackableResources.Count - 1;

        StackableResource poppedResource = stackableResources[lastIndex];
        stackableResources.RemoveAt(lastIndex);
        return poppedResource;
    }

    /// <summary>
    /// Traverse the stack from top to bottom and find the corresponding StackableResource
    /// </summary>
    /// <returns>Corresponding StackableResource</returns>
    public StackableResource Find(ScriptableResource scriptableResource)
    {
        int lastIndex = stackableResources.Count - 1;

        for (int i = lastIndex; i >= 0; i--)
        {
            if (stackableResources[i].ScriptableResource == scriptableResource)
            {
                return stackableResources[i];
            }
        }

        // If no resource was found return null
        return null;
    }

    /// <summary>
    /// Traverse the stack from top to bottom and find all corresponding StackableResources
    /// </summary>
    /// <returns>A list of all corresponding StackableResources</returns>
    public List<StackableResource> FindAll(ScriptableResource scriptableResource)
    {
        List<StackableResource> list = new List<StackableResource>();

        int lastIndex = stackableResources.Count - 1;

        for (int i = lastIndex; i >= 0; i--)
        {
            if (stackableResources[i].ScriptableResource == scriptableResource)
            {
                list.Add(stackableResources[i]);
            }
        }

        // Return list
        return list;
    }

    /// <summary>
    /// Remove particular StackableResource from stack
    /// </summary>
    public void Remove(StackableResource stackableResource)
    {
        stackableResources.Remove(stackableResource);
    }
}
