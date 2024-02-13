using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnTriggerWin : MonoBehaviour
{
    public Text WinText;
    public Text Toxic;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Flag1" || collision.name == "Flag2")
        {
            if (this.gameObject.name == "Player1" || this.gameObject.name == "Player2")
            {
                Toxic.gameObject.SetActive(false);
                WinText.gameObject.SetActive(true); 
                Time.timeScale = 0;
                
            }
        }
    }
}
