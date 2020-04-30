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

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!ballLaunched)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }     
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }

    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ballLaunched = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(launchBallVelocityX, launchBallVelocityY);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(ballLaunched)
            audioSource.
                PlayOneShot(ballSounds[UnityEngine.Random.Range(0,ballSounds.Length)]);
    }
}
