using UnityEngine;

/// <summary>
/// This is a class for the camera, that smoothly follows player 
/// </summary>
public class FollowCamera : MonoBehaviour, IResettable
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSmoothness;

    private PlayerMovement playerMovement;
    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;

    public void InitializeFollowCamera(PlayerMovement playerMovement)
    {
        this.playerMovement = playerMovement;
    }

    private void Awake()
    {
        initialCameraPosition = transform.position;
        initialCameraRotation = transform.rotation;
    }

    private void LateUpdate() => FollowPlayer();

    private void FollowPlayer()
    {
        Vector3 targetPosition = playerMovement.GetPlayerPosition() + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSmoothness);
    }

    public void ResetGameObject()
    {
        transform.position = initialCameraPosition;
        transform.rotation = initialCameraRotation;
    }
}
