using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class OnTriggerWin : MonoBehaviour
{
    public Text WinText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Flag1" || collision.name == "Flag2")
        {
            if (this.gameObject.name == "Player1" || this.gameObject.name == "Player2")
            {
                WinText.gameObject.SetActive(true); 
                Time.timeScale = 0;
                
            }
        }
    }
}
