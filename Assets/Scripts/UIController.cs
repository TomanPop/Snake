using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private GameManager _gameManager;
    private SnakeController _snakeController;
    private AppSettingsService _appSettingsService;

    public void Initialize(GameManager gameManager, SnakeController snakeController, AppSettingsService appSettingsService)
    {
        _gameManager = gameManager;
        _snakeController = snakeController;
        _appSettingsService = appSettingsService;
    }

    public void ShowHideMenu()
    {
        var isEnabled = !pauseMenu.activeSelf;
        pauseMenu.SetActive(isEnabled);

        Time.timeScale = isEnabled ? 0 : 1;
    }

    public void ExitGame()
    {
        SaveGame();
        Application.Quit();
    }

    private void SaveGame()
    {
        var body = _snakeController.GetSnakeBody();
        var parts = new List<Vector2Int>();
        
        parts.Add(body.GetCurrentNode().NodePosition);
        var nextChild = body.GetChild();

        while (nextChild != null)
        {
            parts.Add(nextChild.GetCurrentNode().NodePosition);
            nextChild = nextChild.GetChild();
        }
        
        var data = new GameSaveData()
        {
            score = _gameManager.GetScore(),
            bodyParts = parts.ToArray(),
            moveDirection = _snakeController.GetSnakeBody().GetMoveDirection()
        };

        _appSettingsService.SaveData(data);
    }
}
