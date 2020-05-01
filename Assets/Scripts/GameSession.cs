using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [Range(0.1f, 10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 3;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] int currentScore = 0;
    [SerializeField] bool isAutoPlayEnabled;

    private void SetScoreText() => scoreText.text = currentScore.ToString();

    void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if(gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        SetScoreText();
    }

    public void AddToScore() => currentScore += pointsPerBlockDestroyed;

    public void ResetGame()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled() => isAutoPlayEnabled;
}
