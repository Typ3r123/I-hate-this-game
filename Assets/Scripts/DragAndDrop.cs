using UnityEngine;

public class DragSprite : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector2 offset;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        // Получаем позицию мыши в мировых координатах
        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        // Считаем смещение между объектом и курсором
        offset = (Vector2)transform.position - mouseWorldPos;
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mouseWorldPos + offset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
}