using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private float offsetX;
    private float offsetY;
    private bool isBeingDragged = false;

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isBeingDragged = true;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            offsetX = mousePosition.x - transform.localPosition.x;
            offsetY = mousePosition.y - transform.localPosition.y;
        }
    }

    private void OnMouseUp() 
    {
        isBeingDragged = false;
    }

    private void Update()
    {
        if (isBeingDragged)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x - offsetY, mousePosition.y - offsetY, 0);
        }
    }
}
