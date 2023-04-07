using UnityEngine;

/// <summary>
/// This is a helper class for resource stack, that manages position of elements in local space
/// </summary>
public class StackPositionHelper
{
    private Transform topTransform;
    private Vector3 initialTopTransformLocalPosition;
    private float spawnOffset;
    private int stackHeight;
    private int stackWidth;

    public StackPositionHelper(Transform topTransform, float spawnOffset, int stackHeight, int stackWidth)
    {
        this.topTransform = topTransform;
        this.initialTopTransformLocalPosition = topTransform.localPosition;
        this.spawnOffset = spawnOffset;
        this.stackHeight = stackHeight;
        this.stackWidth = stackWidth;
    }

    public void IncreaseTopPosition()
    {
        float topY = topTransform.localPosition.y + spawnOffset;
        topTransform.localPosition = new Vector3(topTransform.localPosition.x, topY, 0);
    }

    public void DecreaseTopPosition()
    {
        float topY = topTransform.localPosition.y - spawnOffset;
        topTransform.localPosition = new Vector3(topTransform.localPosition.x, topY, 0);
    }

    public void NextStackColumn()
    {
        float topY = initialTopTransformLocalPosition.y;
        float topX = topTransform.localPosition.x + spawnOffset;
        topTransform.localPosition = new Vector3(topX, topY, 0);
    }

    public void PreviousStackColumn()
    {
        float topY = initialTopTransformLocalPosition.y + (stackHeight * spawnOffset);
        float topX = topTransform.localPosition.x - spawnOffset;
        topTransform.localPosition = new Vector3(topX, topY, 0);
    }

    // Traverse stack and update resources position
    public void UpdateResourcePositions(TraversableStack stack) // поменять
    {
        int index = 0;
        foreach (StackableResource resource in stack.StackableResources)
        {
            resource.transform.localPosition = GetPostionFromIndex(index);
            index++;
        }
    }

    private Vector3 GetPostionFromIndex(int index)
    {
        // Get quotient
        int column = index / stackHeight;

        // Get remainder
        int row = index % stackHeight;

        // Calculate x position
        float resourceX = column * spawnOffset;

        // Calculate y position
        float resourceY = row * spawnOffset;

        return new Vector3(resourceX, resourceY, 0);
    }
}
