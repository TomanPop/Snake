public class AppSettingsService : IAppSettingsService
{
    public GameSaveData GameSaveData { get; }
    public GameSettings GameSettings { get; }

    private IJsonService JsonService { get; }

    public AppSettingsService(IJsonService jsonService)
    {
        JsonService = jsonService;
        GameSettings = JsonService.LoadFromStreamingAssets<GameSettings>(GameConstants.GameSettingsPath);

        if (JsonService.ExistsFile(GameConstants.GameSaveDataPath))
            GameSaveData = JsonService.LoadFromStreamingAssets<GameSaveData>(GameConstants.GameSaveDataPath);
        else
            GameSaveData = new GameSaveData();
    }

    public void SaveData(GameSaveData saveData)
    {
        JsonService.SaveStreamingAssets(GameConstants.GameSaveDataPath, saveData);
    }
}