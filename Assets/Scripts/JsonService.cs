using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

public class JsonService : IJsonService
{
    public T LoadFromResource<T>(string path) where T : class
    {
        var data = Resources.Load<TextAsset>(path);
        return JsonConvert.DeserializeObject<T>(data.text, new StringEnumConverter());
    }
        
    public T LoadFromStreamingAssets<T>(string path) where T : class
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, path);
        string data = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<T>(data, new StringEnumConverter());
    }
        
    public void SaveStreamingAssets<T>(string path, T objectToSave) where T : class
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, path);
        string data = JsonConvert.SerializeObject(objectToSave, new StringEnumConverter());
        File.WriteAllText(filePath, data);
    }

    public bool ExistsFile(string path)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, path);
        return File.Exists(filePath);
    }
}