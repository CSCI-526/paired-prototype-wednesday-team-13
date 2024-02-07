using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class OnTriggerDisappear : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "boundary")
        {
            if (gameObject.name == "Player1") GlobalVariables.isDeadP1 = true;
            if (gameObject.name == "Player2") GlobalVariables.isDeadP2 = true;
            Destroy(gameObject);
           
        }
        
    }
}
