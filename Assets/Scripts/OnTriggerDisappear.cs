using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnTriggerDisappear : MonoBehaviour
{
    public Text player1IsDead;
    public Text player2IsDead;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "boundary")
        {
            if (gameObject.name == "Player1") GlobalVariables.isDeadP1 = true;
            if (gameObject.name == "Player2") GlobalVariables.isDeadP2 = true;
            Destroy(gameObject);
            if (GlobalVariables.isDeadP1)
            {
                player1IsDead.gameObject.SetActive(true);
            }
            if (GlobalVariables.isDeadP2)
            {
                player2IsDead.gameObject.SetActive(true);
            }
            if (GlobalVariables.isDeadP1 && GlobalVariables.isDeadP2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                GlobalVariables.isDeadP1 = false;
                GlobalVariables.isDeadP2 = false;
            }
        }
        
    }
}
