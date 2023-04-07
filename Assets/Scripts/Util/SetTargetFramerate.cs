using UnityEngine;

/// <summary>
/// This class is used to set the target framerate of the application
/// </summary>
public class SetTargetFramerate : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
