using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBoss : Enemy
{
    [Header("IntroPhase")]
    [SerializeField] Transform introMovingPoint;
    [SerializeField] float speedOnIntro;

    enum Boss { Intro, Words, RollingCoins, FallingBugs, OutOfScreen, Hand, HandsOutOfScreen };

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
    [SerializeField] float handsSpeed;
    [SerializeField] Vector3 rightHandStartPos;
    [SerializeField] Vector3 leftHandStartPos;
    [SerializeField] GameObject rightHand;
    [SerializeField] Transform rightHandMovePoint;
    [SerializeField] GameObject leftHand;
    [SerializeField] Transform leftHandMovePoint;

    [SerializeField] float theHandWaitTimeduration;
    [SerializeField] float theHandWaitCounter;
    Animator animator;

    float counter;

    bool isInvincible;
    FMOD.Studio.EventInstance shootWordSound;
    FMOD.Studio.EventInstance shootCoinSound;
    FMOD.Studio.EventInstance bossDamageSound;

    protected override void Awake()
    {
        rightHandStartPos = rightHand.transform.localPosition;
        leftHandStartPos = leftHand.transform.localPosition;
        animator = GetComponentInParent<Animator>();
        theHandWaitCounter = theHandWaitTimeduration;
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

                transform.position = Vector2.MoveTowards(transform.position, outOfScreenPoint.position, outOfScreenSpeed * Time.deltaTime);
                if (transform.position == outOfScreenPoint.position)
                {
                    isInvincible = true;
                    boss = Boss.Hand;
                }
                break;
            case Boss.Hand:
                rightHand.transform.localPosition = Vector3.MoveTowards(rightHand.transform.position, rightHandMovePoint.transform.localPosition, handsSpeed * Time.deltaTime);
                leftHand.transform.localPosition = Vector3.MoveTowards(leftHand.transform.position, leftHandMovePoint.transform.localPosition, handsSpeed * Time.deltaTime);

                if (rightHand.transform.localPosition == rightHandMovePoint.localPosition && leftHand.transform.localPosition == leftHandMovePoint.localPosition)
                {


                    if (theHandWaitCounter > 0)
                    {
                        theHandWaitCounter -= Time.deltaTime;
                        TheHand.canDie = true;
                    }
                    else
                    {
                        theHandWaitCounter = theHandWaitTimeduration;
                        boss = Boss.HandsOutOfScreen;
                    }
                }
                break;
            case Boss.HandsOutOfScreen:
                TheHand.counter = 0;

                TheHand.canDie = false;

                rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rightHandStartPos, handsSpeed * Time.deltaTime);
                leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, leftHandStartPos, handsSpeed * Time.deltaTime);

                if (rightHand.transform.position == rightHandStartPos && leftHand.transform.position == leftHandStartPos)
                {
                    boss = Boss.Intro;
                }
                break;


        }
    }

    public override void TakeDamage(int damage)
    {
        if (isInvincible) return;

        bossDamageSound = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/en_die");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(bossDamageSound, transform, GetComponent<Rigidbody2D>());
        bossDamageSound.start();
        bossDamageSound.release();
        health -= damage;
        if (health <= 0)
            Destroy();
    }

    public void Shoot()
    {
        if (isShootingWords)
        {
            Instantiate(wordPrefab, firePoint.position, Quaternion.identity);
            shootWordSound = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/boss_words");
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(shootWordSound, transform, GetComponent<Rigidbody2D>());
            shootWordSound.start();
            shootWordSound.release();
        }
        else
        {
            Instantiate(rollingCoin, firePoint.position, Quaternion.identity);
            shootCoinSound = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/boss_coins");
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(shootCoinSound, transform, GetComponent<Rigidbody2D>());
            shootCoinSound.start();
            shootCoinSound.release();
        }
    }
}
