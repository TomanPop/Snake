using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScore;
    [SerializeField] private Button continueButton;

    private AppSettingsService _appSettingsService;

    private void Start()
    {
        LoadData();
    }

    private void LoadData()
    {
        var jsonService = new JsonService();
        _appSettingsService = new AppSettingsService(jsonService);

        highScore.text = _appSettingsService.GameSaveData.highScore.ToString();
        
        continueButton.interactable = _appSettingsService.GameSaveData.lastBodyParts != null &&
                                      _appSettingsService.GameSaveData.lastBodyParts.Length > 0;
    }

    public void OnNewGameClick()
    {
        GameContext.BeginScore = 0;
        GameContext.BeginDirection = MoveDirection.Right;
        GameContext.SnakePositions = new[] { new Vector2Int(5, 5) };
        
        LoadGame();
    }

    public void OnContinueClick()
    {
        var gameSaveData = _appSettingsService.GameSaveData;
        
        GameContext.BeginScore = gameSaveData.lastScore;
        GameContext.BeginDirection = gameSaveData.lastMoveDirection;
        GameContext.SnakePositions = gameSaveData.lastBodyParts;

        LoadGame();
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
