using System.Collections;
using UnityEngine;

/// <summary>
/// A collection of tweens
/// </summary>
public static class Tweens
{
    /// <summary>
    /// Smoothly change position and rotation to the targetPosition and targetRotation
    /// </summary>
    public static IEnumerator SmoothlyMoveAndRotateTowards(Transform transform, Transform targetTransform, float time)
    {
        float elapsedTime = 0;

        Vector3 startingPosition = transform.position;
        Quaternion startingRotation = transform.rotation;

        while (elapsedTime < time)
        {
            // Move
            transform.position = Vector3.Lerp(startingPosition, targetTransform.position, elapsedTime / time);

            // Rotate
            transform.rotation = Quaternion.Slerp(startingRotation, targetTransform.rotation, elapsedTime / time);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that position and rotation are set to their target values at the end
        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
    }

    /// <summary>
    /// Smoothly change rotation to look at targetTransform
    /// </summary>
    public static IEnumerator SmoothlyLookAt(Transform transform, Transform targetTransform, float time)
    {
        float elapsedTime = 0;

        Quaternion startingRotation = transform.localRotation;
        Quaternion targetRotation = Quaternion.LookRotation(targetTransform.position - transform.position);

        while (elapsedTime < time)
        {
            // Rotate
            transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, elapsedTime / time);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that rotation is set to target value at the end
        transform.rotation = targetRotation;
    }
}