using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject winPanel;
    public GameObject gameOverPanel;

    /* — UI — */
    [Header("UI")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI oxygenText;

    /* — Gameplay — */
    [Header("Gameplay")]
    public float maxOxygen = 5f;
    public float currentOxygen;
    public int scoreNeededToWin = 10;

    int score;
    bool ended;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        currentOxygen = maxOxygen;
        UpdateUI();
    }
    void Update()
    {
        if (ended) return;

        currentOxygen -= Time.deltaTime;
        if (currentOxygen <= 0) EndGame(false);

        oxygenText.text = $"Time {currentOxygen:0.0}";
        
    }

    /* — public API — */
    public void AddOxygen(float sec)
    {
        currentOxygen = Mathf.Min(currentOxygen + sec, maxOxygen);
        UpdateUI();
    }
    public void AddScore(int pts)
    {
        score += pts;
        UpdateUI();
        if (score >= scoreNeededToWin) EndGame(true);
    }
    
    void UpdateUI()
    {
        scoreText.text   = $"Score {score}";
    }
    
    public void EndGame(bool win)
    {
        if (ended) return;
        ended = true;

        if (win && winPanel != null)
        {
            winPanel.SetActive(true);
            Debug.Log("You win!");
        }
        else if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("Game Over");
        }
        
    }
    
    public void RestartGame()
    {
        Debug.Log("RESTART button clicked!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    
}

