public interface IJsonService
{
    T LoadFromResource<T>(string path) where T : class;
    T LoadFromStreamingAssets<T>(string path) where T : class;
    void SaveStreamingAssets<T>(string path, T objectToSave) where T : class;
    bool ExistsFile(string path);
}