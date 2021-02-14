using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;

    [SerializeField] float speed;

    Vector3 nextPos;

    void Start()
    {
        nextPos = startPos.position;
    }

    void Update()
    {
        if(transform.position == startPos.position)
        {
            nextPos = endPos.position;
        }
        if(transform.position == endPos.position)
        {
            nextPos = startPos.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
