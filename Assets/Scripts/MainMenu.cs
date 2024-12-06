using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScore;
    [SerializeField] private Button continueButton;

    private IAppSettingsService _appSettingsService;

    [Inject]
    public void Initialize(IAppSettingsService appSettingsService)
    {
        _appSettingsService = appSettingsService;
        LoadData();
    }

    private void LoadData()
    {
        var gameSaveData = _appSettingsService.GameSaveData;

        highScore.text = gameSaveData.highScore.ToString();
        
        continueButton.interactable = gameSaveData.lastBodyParts != null &&
                                      gameSaveData.lastBodyParts.Length > 0;
    }

    public void OnNewGameClick()
    {
        GameContext.BeginScore = 0;
        GameContext.BeginDirection = MoveDirection.Right;
        GameContext.SnakePositions = new[] { new Vector2Int(5, 5) };
        
        CreateGame();
    }

    public void OnContinueClick()
    {
        var gameSaveData = _appSettingsService.GameSaveData;
        
        GameContext.BeginScore = gameSaveData.lastScore;
        GameContext.BeginDirection = gameSaveData.lastMoveDirection;
        GameContext.SnakePositions = gameSaveData.lastBodyParts;

        CreateGame();
    }

    private void CreateGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
