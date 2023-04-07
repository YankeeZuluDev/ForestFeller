using UnityEngine;

/// <summary>
/// This class is used to rotate the circular saw on the factory
/// </summary>
public class RotateSaw : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
