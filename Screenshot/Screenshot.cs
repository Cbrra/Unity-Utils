using System;
using System.Collections;
using System.IO;
using UnityEngine;

public static class Screenshot
{
    public static readonly string path = $"{Application.persistentDataPath}/screenshots";

    public static byte[] TakeFromCamera(Camera cam)
    {
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cam.targetTexture = renderTexture;

        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        cam.Render();

        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

        cam.targetTexture = null;
        RenderTexture.active = null;

        UnityEngine.Object.Destroy(renderTexture);

        return screenshot.EncodeToPNG();
    }

    public static void SaveScreenshot(byte[] bytes)
    {
        DateTime time = DateTime.Now;
        string filename = time.ToString("dd-MM-yyyy.HH.mm.ss");

        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        File.WriteAllBytes($"{path}/{filename}.png", bytes);
    }

    public static void TakeAndSaveScreenshotFromCamera(Camera cam)
    {
        SaveScreenshot(TakeFromCamera(cam));
    }

    public static IEnumerator Take()
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
        texture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);

        SaveScreenshot(texture.EncodeToPNG());
        UnityEngine.Object.Destroy(texture);
    }
}
