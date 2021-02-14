using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    PlatformEffector2D effector;
    bool isPlaying;
    void Start()
    {
        effector = GetComponentInParent<PlatformEffector2D>();
    }

    IEnumerator FallDown()
    {
        isPlaying = true;
        effector.rotationalOffset = 180f;
        yield return new WaitForSeconds(0.15f);
        effector.rotationalOffset = 0f;
        isPlaying = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Input.GetKey(KeyCode.S) && isPlaying == false)
            {
                StartCoroutine(FallDown());
            }
        }
    }
}
