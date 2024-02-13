using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{
    public Vector2 resetPosition;

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("ResetObstacle"))
        {
            Debug.Log("Hit");
            transform.position = resetPosition;
        }

    }
}
