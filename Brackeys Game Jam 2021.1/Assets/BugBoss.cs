using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBoss : Enemy
{
    [Header("IntroPhase")]
    [SerializeField] Transform introMovingPoint;
    [SerializeField] float speedOnIntro;

    enum Boss { Intro, Words, RollingCoins, FallingBugs, OutOfScreen, Hand };

    Boss boss = Boss.Intro;

    [SerializeField] Transform firePoint;

    [Header("Words")]
    [SerializeField] GameObject wordPrefab;
    bool isShootingWords;
    [SerializeField] float durationOfWordsPhase;

    [Header("RollingCoins")]
    public GameObject rollingCoin;
    [SerializeField] float durationOfRollingCoins;

    [Header("FallingBugs")]
    [SerializeField] GameObject fallingBug;
    [SerializeField] float FallingBugsPhaseDuration;
    [SerializeField] Transform[] allBugSpawnPoints;
    [SerializeField] float howOftenBugShouldAppear;
    float bugAppearingCounter;

    [Header("Out of screen")]
    [SerializeField] Transform outOfScreenPoint;
    [SerializeField] float outOfScreenSpeed;

    [Header("The Hand")]
    [SerializeField] Transform rightHandMovePoint;

    Animator animator;

    float counter;

    bool isInvincible;

    protected override void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        switch (boss)
        {
            case Boss.Intro:

                transform.position = Vector2.MoveTowards(transform.position, introMovingPoint.position, speedOnIntro * Time.deltaTime);

                if (transform.position == introMovingPoint.position)
                {
                    boss = Boss.Words;
                    counter = durationOfWordsPhase;
                }

                break;
            case Boss.Words:
                if (counter > 0)
                {
                    animator.SetBool("IsShooting", true);
                    isShootingWords = true;
                    counter -= Time.deltaTime;
                }
                else
                {
                    boss = Boss.RollingCoins;
                    counter = durationOfRollingCoins;
                }

                break;

            case Boss.RollingCoins:
                if (counter > 0)
                {
                    isShootingWords = false;
                    counter -= Time.deltaTime;
                }
                else
                {
                    animator.SetBool("IsShooting", false);
                    boss = Boss.FallingBugs;

                    counter = FallingBugsPhaseDuration;
                }
                break;
            case Boss.FallingBugs:
                if (counter > 0)
                {
                    counter -= Time.deltaTime;

                    if (bugAppearingCounter > 0)
                    {
                        bugAppearingCounter -= Time.deltaTime;
                    }
                    else
                    {
                        int random = Random.Range(0, allBugSpawnPoints.Length);
                        Instantiate(fallingBug, allBugSpawnPoints[random].position, Quaternion.identity); ;
                        bugAppearingCounter = howOftenBugShouldAppear;
                    }
                }
                else
                {
                    boss = Boss.OutOfScreen;
                }
                break;

            case Boss.OutOfScreen:
                if (transform.position != outOfScreenPoint.position)
                {
                    transform.position = Vector2.MoveTowards(transform.position, outOfScreenPoint.position, outOfScreenSpeed * Time.deltaTime);
                }
                else
                {
                    isInvincible = true;
                    boss = Boss.Hand;
                }
                break;
            case Boss.Hand:

                break;

        }
    }

    public override void TakeDamage(int damage)
    {
        if (isInvincible) return;

        health -= damage;
        if (health <= 0)
            Destroy();
    }

    public void Shoot()
    {
        if (isShootingWords)
        {
            Instantiate(wordPrefab, firePoint.position, Quaternion.identity);
        }
        else
        {
            Instantiate(rollingCoin, firePoint.position, Quaternion.identity);
        }
    }
}
