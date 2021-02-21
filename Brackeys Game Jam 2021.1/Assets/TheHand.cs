using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHand : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    public static int counter = 0;

    public static bool canDie;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            counter++;
            if (counter >= 2)
            {
                if (canDie)
                {
                    deathScreen.SetActive(true);

                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            counter--;
        }
    }
}
