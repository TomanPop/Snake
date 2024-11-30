using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game Manager
/// </summary>
public class GameManager : MonoBehaviour, IGameManager
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

    private AppSettingsService _appSettingsService;
    private int _totalScore;

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        var jsonService = new JsonService();
        _appSettingsService = new AppSettingsService(jsonService);
      
        map.Initialize(_appSettingsService);

        var commandInvoker = new CommandInvoker();
        var snake = CreateSnake();
        
        uiController.Initialize(this, snakeController, _appSettingsService);
        inputHandler.Initialize(snakeController, uiController, commandInvoker);
        foodFactory.Initialize(snakeController, snakeFactory);
        foodManager.Initialize(map, this, foodFactory, _appSettingsService);
        snakeController.Initialize(this, snake, _appSettingsService);
        
        UpdateScore();
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
