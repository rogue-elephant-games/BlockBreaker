using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 14f;
    [SerializeField] float screenWidthInUnits = 16f;

    GameSession gameSession;
    Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePosition = new Vector2(Mathf.Clamp(GetXPosition(), minX, maxX), 0.5f);
        transform.position = paddlePosition;
    }

    private float GetXPosition() =>
        gameSession.IsAutoPlayEnabled() ?
            ball.transform.position.x
            : Input.mousePosition.x / Screen.width * screenWidthInUnits;
}
