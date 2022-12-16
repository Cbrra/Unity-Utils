using System.IO;
using UnityEngine;

public abstract class JsonFile<T> where T : new()
{
    private static string GetFilePath(string path) => Application.persistentDataPath + "/" + path;

    public void Save(string path)
    {
        File.WriteAllText(GetFilePath(path), JsonUtility.ToJson(this));
    }

    public static T Load(string path)
    {
        string filePath = GetFilePath(path);
        if (!File.Exists(filePath)) return new T();

        string data = File.ReadAllText(filePath);
        try
        {
            return JsonUtility.FromJson<T>(data);
        }
        catch
        {
            Debug.LogError($"File \"{filePath}\" cannot be parsed");
            return new T();
        }
    }
}
