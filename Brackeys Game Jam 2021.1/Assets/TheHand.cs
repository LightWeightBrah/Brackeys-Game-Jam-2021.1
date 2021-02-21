using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHand : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    static int counter = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            counter++;
            if (counter >= 2)
            {
                deathScreen.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            counter--;
        }
    }
}
