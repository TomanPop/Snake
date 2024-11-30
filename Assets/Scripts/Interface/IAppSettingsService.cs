public interface IAppSettingsService
{
    GameSettings GameSettings { get; }
    GameSaveData GameSaveData { get; }

    public void SaveData(GameSaveData saveData);
}