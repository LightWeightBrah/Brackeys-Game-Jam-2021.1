using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBoss : MonoBehaviour
{
    [Header("IntroPhase")]
    [SerializeField] Transform introMovingPoint;
    [SerializeField] float speedOnIntro;

    enum Boss { Intro, Words, RollingCoins, Hand, FallingBugs};

    Boss boss = Boss.Intro;


    private void Update()
    {
        switch(boss)
        {
            case Boss.Intro:

                transform.position = Vector2.MoveTowards(transform.position, introMovingPoint.position, speedOnIntro * Time.deltaTime);

                if(transform.position == introMovingPoint.position)
                {
                    boss = Boss.Words;
                }

                break;
            case Boss.Words:

                break;
        }
    }

    public void Shoot()
    {

    }
}
