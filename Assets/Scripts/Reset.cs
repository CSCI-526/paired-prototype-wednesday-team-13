using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public Vector3 resetPosition = new Vector3(-8.35f, 1.57f, 0f);

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("obstacle1"))
        {
            Debug.Log("Hit");
            transform.position = resetPosition;
        }

    }
}

