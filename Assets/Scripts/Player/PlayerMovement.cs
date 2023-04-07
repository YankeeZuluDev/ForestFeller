using UnityEngine;

/// <summary>
/// This class is responsible for handling player movement
/// </summary>

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour, IResettable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    private Joystick joystick;
    private CharacterController characterController;
    private Vector3 initialPlayerPosition;
    private Quaternion initialPlayerRotation;

    public void InitializePlayerMovement(Joystick joystick)
    {
        this.joystick = joystick;
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        initialPlayerPosition = transform.position;
        initialPlayerRotation = transform.rotation;
    }

    private void Update()
    {
        // Exit if no input
        if (!joystick.HasInput) return;

        // Get horizontal and vertical inputs
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 velocity = CalculateVelocity(horizontal, vertical);

        Move(velocity);

        Rotate(velocity);
    }

    private Vector3 CalculateVelocity(float horizontal, float vertical)
    {
        // Calculate the velocity depending on horizontal and vertical inputs
        Vector3 velocity = new Vector3(horizontal, 0, vertical);

        // Clamp the velocity magnitude to make movement more responsive  
        velocity = Vector3.ClampMagnitude(velocity, 1);

        return velocity;
    }

    private void Move(Vector3 velocity)
    {
        // Move
        characterController.Move(velocity * (moveSpeed * Time.deltaTime));
    }

    private void Rotate(Vector3 velocity)
    {
        // Rotate
        Vector3 rotateDirection = Vector3.RotateTowards(transform.forward, velocity, rotateSpeed * Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(rotateDirection);
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public void ResetGameObject()
    {
        transform.position = initialPlayerPosition;
        transform.rotation = initialPlayerRotation;
    }
}
