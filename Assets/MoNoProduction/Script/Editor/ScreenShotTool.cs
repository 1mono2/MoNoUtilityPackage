using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScreenShotTools
{
    [MenuItem("Tools/Screenshot %F3", false)]
    public static void CaptureScreenshot()
    {
        string productName = Application.productName;
        if (string.IsNullOrEmpty(productName))
        {
            productName = "Unity";
        }
        string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), productName);
        DateTime now = DateTime.Now;
        string fileName = string.Format("{0}_{1}x{2}_{3}{4:D2}{5:D2}{6:D2}{7:D2}{8:D2}.png", productName, Screen.width, Screen.height, now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
        string path = Path.Combine(directory, fileName);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        ScreenCapture.CaptureScreenshot(path);
        Debug.LogFormat("Screenshot Save : {0}", path);
    }

}
