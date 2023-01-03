using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SharedLibrary;
using System;

public class MenuManager : MonoBehaviour
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
                obj = JsonUtility.FromJson<T>(line);
            }
        }
        catch (Exception) {
            return;
        }
        
    }


    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
