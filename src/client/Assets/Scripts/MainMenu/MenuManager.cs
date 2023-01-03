using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SharedLibrary;
using System;

public class MenuManager : MonoBehaviour
{
    public static User user;
    void Start()
    {
        ReadFromFile("logindata.json");
    }

    private void ReadFromFile(string fileName)
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
        }
        catch (Exception) {
            return;
        }
        
        Debug.Log(user.Username);
    }


    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
