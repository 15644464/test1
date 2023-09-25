using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Paths
{
    public static string uiConfigPath = $"{Application.streamingAssetsPath}/Configs/UIConfig.json";
    public static string loaderConfigPath = $"{Application.streamingAssetsPath}/Configs/LoaderConfig.json";
    public static string projectConfigPath = $"{Application.streamingAssetsPath}/Configs/ProjectConfig.json";
    public static string packName = "";
    public const string AndroidExternalPath = "/sdcard";

    public static string getFilePath(string name, bool isUnityWebRequest = true)
    {
        {
            return getInnerPath(name);
        }
    }
    private static string getExternalPath(string name, bool isUnityWebRequest)
    {
        string path = "";
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        path = $"{Path.GetFullPath("..", Application.dataPath)}/Data/{packName}/{name}";
#elif UNITY_ANDROID
            if (isUnityWebRequest)
            {
                path = $"jar:file://{AndroidExternalPath}/{packName}/{name}";
            } else
            {
                path = $"{AndroidExternalPath}/{packName}/{name}";
            }
#endif
        return path;
    }

    private static string getInnerPath(string name)
    {
        return $"{Path.GetFullPath("..", Application.dataPath)}/Data/{name}";
        //return $"{Application.streamingAssetsPath}/Data/{packName}/{name}";
    }

    public static string getWarsPath(bool isUnityWebRequest = true)
    {
        string path = "";
        {
            return $"{Path.GetFullPath("..", Application.dataPath)}/Data";
            //return $"{Application.streamingAssetsPath}/Data";
        }
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        path = $"{Path.GetFullPath("..", Application.dataPath)}/Data";
#elif UNITY_ANDROID
            if (isUnityWebRequest)
            {
                path = $"jar:file://{AndroidExternalPath}";
            } else
            {
                path = $"{AndroidExternalPath}";
            }
#else
			path = $"{Application.streamingAssetsPath}/Data";
#endif
        return path;
    }
}