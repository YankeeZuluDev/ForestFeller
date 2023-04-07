using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This is a class for joystick
/// </summary>
public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IResettable
{
    [SerializeField] private float handleRange;
    [SerializeField] private float deadZone;
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;

    private RectTransform rectTransform;
    private Camera cam;
    private Canvas canvas;
    private Vector2 input = Vector2.zero;

    public float Horizontal => input.x;
    public float Vertical => input.y;
    public bool HasInput => Horizontal != 0 || Vertical != 0;

    public void InitializeJoystick(Camera cam, Canvas canvas)
    {
        this.cam = cam;
        this.canvas = canvas;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Set background position to the touch position
        background.anchoredPosition = GetAnchoredPosition(eventData.position);

        // Show background 
        background.gameObject.SetActive(true);

        // Call OnDrag to move handle positon
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert background position to screen space
        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);

        // Calculate radius
        Vector2 radius = background.sizeDelta / 2f;

        // Calculate input based on position of touch and radius
        input = (eventData.position - position) / (radius * canvas.scaleFactor);

        // Normalize input
        input = NormalizeInput(input);

        // Set handle position
        handle.anchoredPosition = input * (radius * handleRange);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetJoystick();
    }

    private Vector2 NormalizeInput(Vector2 originalInput)
    {
        // Check if original input magnitude is greater than dead zone
        if (originalInput.magnitude > deadZone)
        {
            // Check if original input magnitude needs to be normalized
            if (originalInput.magnitude > 1f)
                return originalInput.normalized;
            else
                return originalInput;
        }
        else
            return Vector2.zero;
    }

    private Vector2 GetAnchoredPosition(Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPosition, cam, out localPoint);
        return localPoint - rectTransform.anchoredPosition;
    }

    private void ResetJoystick()
    {
        // Reset input
        input = Vector2.zero;

        // Hide background 
        background.gameObject.SetActive(false);
    }

    public void ResetGameObject()
    {
        ResetJoystick();
    }
}
