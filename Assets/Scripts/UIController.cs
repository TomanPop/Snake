using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour, IUIController
{
    [SerializeField] private GameObject pauseMenu;

    private IGameManager _gameManager;
    private ISnakeController _snakeController;
    private IAppSettingsService _appSettingsService;

    [Inject]
    public void Initialize(IGameManager gameManager, ISnakeController snakeController, IAppSettingsService appSettingsService)
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
            highScore = _appSettingsService.GameSaveData.highScore,
            lastScore = _gameManager.GetScore(),
            lastBodyParts = parts.ToArray(),
            lastMoveDirection = _snakeController.GetSnakeBody().GetMoveDirection()
        };

        _appSettingsService.SaveData(data);
    }
}
