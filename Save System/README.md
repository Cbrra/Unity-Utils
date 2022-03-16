# **Save System**

`English`
<br>Basic backup system working with Unity. Data is stored in JSON format but does not need to be in a file with `.json` extension.

`Français`
<br>Système de sauvegarde basique fonctionnant avec Unity. Les données sont stockées en format JSON mais il n'est pas nécessaire qu'elles soient dans un fichier avec une extension `.json`.

## **Example**

```c#
// UserData.cs
// Create your class extended of JsonFile

public class UserData : JsonFile
{
    public string username;
    public int id;
}
```

```c#
// GameManager.cs
// Example of a GameManager for easily load and save the player data

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UserData userData;

    void Awake()
    {
        // Load the player data
        userData = UserData.Load<UserData>("filename.json");

        // Log the player username
        Debug.Log(userData.username);

        // Change the player username
        userData.username = "New Username";

        // Save the player data
        userData.Save("filename.json");
    }
}
```
