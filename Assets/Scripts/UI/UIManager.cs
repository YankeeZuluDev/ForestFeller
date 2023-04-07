using UnityEngine;

/// <summary>
/// This is a class for UI manager
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Joystick joystick;
    [SerializeField] private UIIconDisplay uIIconDisplay;

    public Joystick Joystick { get => joystick; set => joystick = value; }
    public UIIconDisplay UIIconDisplay { get => uIIconDisplay; set => uIIconDisplay = value; }

    public void InitializeUIManager(Camera cam)
    {
        canvas.worldCamera = cam;
        joystick.InitializeJoystick(cam, canvas);
    }
}
