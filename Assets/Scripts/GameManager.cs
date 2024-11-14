using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game Manager
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject endScreen;
    private int totalScore;

    private void Start()
    {
        UpdateScore();
    }

    /// <summary>
    /// Add value to total score
    /// </summary>
    /// <param name="score">added score</param>
    public void AddScore(int score)
    {
        totalScore += score;

        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + totalScore;
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Called when game is finished
    /// </summary>
    public void GameOver()
    {
        endScreen.SetActive(true);
        Invoke("Restart", 2);
    }
}
