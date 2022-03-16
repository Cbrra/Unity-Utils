using System.IO;
using UnityEngine;

public abstract class JsonFile
{
    private static string getFilePath(string path) => Application.persistentDataPath + "/" + path;

    public void Save(string path)
    {
        File.WriteAllText(getFilePath(path), JsonUtility.ToJson(this));
    }

    public static T Load<T>(string path) where T : new()
    {
        string filePath = getFilePath(path);
        if(!File.Exists(filePath)) return new T();

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
