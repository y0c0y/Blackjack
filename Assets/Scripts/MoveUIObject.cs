using UnityEngine;

public class MoveUIObject : MonoBehaviour
{
    public RectTransform uiObject;  // Reference to the UI object's RectTransform

    void Start()
    {
        // Move the UI object to a new position (x, y) in the canvas
        MoveObject(new Vector2(100f, 200f));  // Example new position
    }

    public void MoveObject(Vector2 newPosition)
    {
        if (uiObject != null)
        {
            uiObject.anchoredPosition = newPosition;
        }
        else
        {
            Debug.LogError("UI object is not assigned.");
        }
    }
}
