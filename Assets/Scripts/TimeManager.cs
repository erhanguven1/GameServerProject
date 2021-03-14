using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    void Awake()
    {
        instance = this;
    }

    public readonly float gameDuration = 120.0f, countdownDuration = 6.0f;
    private float gameTime, countdownTime;
    public float remainingGameTime;
    public bool ticking, countdownTicking;

    private float lastTime;

    // Update is called once per frame
    private void Update()
    {
        if (ticking)
        {
            gameTime += Time.deltaTime;
            remainingGameTime = gameDuration - gameTime;

            if (remainingGameTime == gameDuration -1)
            {
                lastTime = Time.time;
            }

            if (Mathf.Abs(lastTime - Time.time) > 1)
            {
                lastTime = Time.time;
                ServerSend.SendServerTime(remainingGameTime);
            }

            if (remainingGameTime <= 1)
            {
                StopTicking();
            }
        }

        if (countdownTicking)
        {
            countdownTime -= Time.deltaTime;
            ServerSend.SendServerTime(countdownTime);
            if (countdownTime <= 1)
            {
                GameplayManager.instance.cantMove = false;
                GameplayManager.instance.isWaiting = false;
                StartTicking();
            }
        }
    }

    public void StartTicking()
    {
        countdownTicking = false;

        gameTime = 0;
        ticking = true;
    }

    public void StopTicking()
    {
        ticking = false;
        GameplayManager.instance.StartCoroutine(GameplayManager.instance.WaitAndRespawn());
    }

    public void StartCountdown()
    {
        countdownTime = countdownDuration;
        GameplayManager.instance.cantMove = true;
        GameplayManager.instance.isWaiting = true;
        countdownTicking = true;
        BallController.instance.rb.isKinematic = false;
    }
}
