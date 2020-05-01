using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle1;
    [SerializeField] float launchBallVelocityX = 1f;
    [SerializeField] float launchBallVelocityY = 15f;
    [SerializeField] AudioClip[] ballSounds;

    Vector2 paddleToBallVector;
    private bool ballLaunched = false;

    AudioSource audioSource;
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        audioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!ballLaunched)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
        else
        {
            BombOnMouseClick();
        }
    }

    private void IfUserClicks(Action action)
    {
        if(Input.GetMouseButtonDown(0))
            action();
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }

    private void LaunchOnMouseClick() =>
        IfUserClicks(() => {
            ballLaunched = true;
            rigidbody2D.velocity = new Vector2(launchBallVelocityX, launchBallVelocityY);
        });

    private void BombOnMouseClick() =>
        IfUserClicks(() => {
            rigidbody2D.velocity = new Vector2(gameObject.transform.position.x, -(launchBallVelocityY * 1.5f));
        });

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(UnityEngine.Random.Range(0f, ballSounds.Length), UnityEngine.Random.Range(0f, ballSounds.Length));
        if(ballLaunched) 
        {
            audioSource.
                PlayOneShot(ballSounds[UnityEngine.Random.Range(0,ballSounds.Length)]);
            rigidbody2D.velocity += velocityTweak;
        } 
    }
}
