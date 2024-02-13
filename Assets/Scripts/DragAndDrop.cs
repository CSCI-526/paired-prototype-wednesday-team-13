using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop: MonoBehaviour
{
    Vector3 offset;
    Collider2D collider2d;
    public string destinationTag = "DropArea";
    bool isDraggable = true;

    void Awake()
    {
        collider2d = GetComponent<Collider2D>();
    }

    void OnMouseDown()
    {
        if (isDraggable)
            offset = transform.position - MouseWorldPosition();
    }

    void OnMouseDrag()
    {
        if (isDraggable)
            transform.position = MouseWorldPosition() + offset;
    }

    void OnMouseUp()
    {
        if (isDraggable)
        {
            var rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hitInfo = Physics2D.Raycast(rayOrigin, Vector2.zero);

            if (hitInfo.collider != null && hitInfo.transform.CompareTag(destinationTag))
            {
                transform.position = hitInfo.point;

            }
            isDraggable = false;
        }
    }

    void deactiveDrag()
    { 
        
       isDraggable = false;
        
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
