using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

/// <summary>
/// Game Manager
/// </summary>
public class GameManager : MonoBehaviour, IGameManager
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject endScreen;
    
    private IAppSettingsService _appSettingsService;
    private int _totalScore;
    
    [Inject]
    public void Initialize(IAppSettingsService appSettingsService)
    {
        _appSettingsService = appSettingsService;
        UpdateScore();
    }

    /// <summary>
    /// Add value to total score
    /// </summary>
    /// <param name="score">added score</param>
    public void AddScore(int score)
    {
        _totalScore += score;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + _totalScore;
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
        var highScore = _appSettingsService.GameSaveData.highScore;
        
        //clear save
        var data = new GameSaveData()
        {
            highScore = _totalScore > highScore ? _totalScore : highScore,
            lastScore = 0,
            lastBodyParts = null,
            lastMoveDirection = MoveDirection.None
        };

        _appSettingsService.SaveData(data);

        endScreen.SetActive(true);
        Invoke("Restart", 2);
    }

    public int GetScore()
    {
        return _totalScore;
    }
}
