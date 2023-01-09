using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SharedLibrary;
using System;

public class UserData : MonoBehaviour
{
    public static User user;
    //public static PlayerProgress progress;
    void Start()
    {
        ReadFromFile<User>("logindata.json", user);
    //  ReadFromFile<PlayerProgress>("progress.json", progress);
    }

    private void ReadFromFile<T>(string fileName, object obj)
    {
        string path = GetFilePath(fileName);
        try {
            using FileStream fs = File.OpenRead(path);
            using var sr = new StreamReader(fs);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                user = JsonUtility.FromJson<User>(line);
            }
            Debug.Log(string.Format("File {0} exists", fileName));
        }
        catch (Exception) {
            Debug.Log(string.Format("File {0} does not exist", fileName));
        }
        
    }

    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
