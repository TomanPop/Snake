using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game Manager
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private Map map;
    [SerializeField] private UIController uiController;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private SnakeController snakeController;
    [SerializeField] private SnakeFactory snakeFactory;
    [SerializeField] private FoodManager foodManager;
    [SerializeField] private FoodFactory foodFactory;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject endScreen;
    
    private int totalScore;

    private void Start()
    {
        InitializeGame();
        UpdateScore();
    }

    private void InitializeGame()
    {
        var jsonService = new JsonService();
        var appSettingsService = new AppSettingsService(jsonService);
        var commandInvoker = new CommandInvoker();
        
        map.Initialize(appSettingsService);

        var snake = CreateSnake();
        
        uiController.Initialize(this, snakeController, appSettingsService);
        inputHandler.Initialize(snakeController, uiController, commandInvoker);
        foodFactory.Initialize(snakeController, snakeFactory);
        foodManager.Initialize(map, this, foodFactory, appSettingsService);
        snakeController.Initialize(this, snake, appSettingsService);
    }

    private IBody CreateSnake()
    {
        var head = (IBody)null;
        
        foreach (var snakePosition in GameContext.SnakePositions)
        {
            var node = map.GetNode(snakePosition);
            var snakeBody = snakeFactory.CreateSnakeBodyPart(node);

            if (head == null)
            {
                head = snakeBody;
            }
            else
            {
                head.AddBodyPart(snakeBody);
            }
        }

        return head;
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
        var highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (totalScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", totalScore);
            PlayerPrefs.Save();
        }

        endScreen.SetActive(true);
        Invoke("Restart", 2);
    }

    public int GetScore()
    {
        return totalScore;
    }
}
