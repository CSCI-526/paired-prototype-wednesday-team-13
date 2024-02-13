using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{
    public Vector2 resetPosition;

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("ResetObstacle"))
        {
            Debug.Log("Hit");
            yield return new WaitForSeconds(0.25f);
            transform.position = resetPosition;
        }

    }
}
