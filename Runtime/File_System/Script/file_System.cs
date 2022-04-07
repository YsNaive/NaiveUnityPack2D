using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace NaiveAPI
{
    public static class file_System
    {
        public static void SaveDataAsJson<T>(T saveObject, string directory, string fileName) where T : new()
        {
            if (directory[directory.Length - 1] != '/') directory += '/';
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            string jsonData;
            if (saveObject.GetType().ToString().Contains("List"))
            {
                jsonData = JsonUtility.ToJson(saveObject);
            }
            else
                jsonData = JsonUtility.ToJson(saveObject);
            File.WriteAllText(directory + fileName, jsonData);
        }

        // 從路徑讀取檔案
        public static T LoadDataAsJson<T>(string dataPath) where T : new()
        {
            if (File.Exists(dataPath))
            {
                string jsonData = File.ReadAllText(dataPath);
                return JsonUtility.FromJson<T>(jsonData);
            }
            else
            {
                Debug.Log("DataFile Not Found , please Check your path");
                return default;
            }
        }

        public static void LoadDataAsJson<T>(string dataPath ,T overWriteObject) where T : new()
        {
            if (File.Exists(dataPath))
            {
                string jsonData = File.ReadAllText(dataPath);
                JsonUtility.FromJsonOverwrite(jsonData , overWriteObject);
            }
            else
            {
                Debug.Log("DataFile Not Found , please Check your path");
            }
        }
    }
}

