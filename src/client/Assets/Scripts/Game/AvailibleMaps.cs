using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharedLibrary;
using System;
using System.IO;
using System.Linq;
public class AvailibleMaps : MonoBehaviour
{
    public GameObject map2;
    private PlayerProgress progress;
    private string getFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
    private void ReadFromFile(string fileName)
    {
        string path = getFilePath(fileName);
        try {
            using FileStream fs = File.OpenRead(path);
            using var sr = new StreamReader(fs);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                progress = JsonUtility.FromJson<PlayerProgress>(line);
            }
        }
        catch (Exception) {
            Debug.Log("file does not exist"); 
        }

    }


    void Start()
    {
        ReadFromFile("progress.json");
        var map = progress.maps.FirstOrDefault(maps => maps.Map_id == 1);
        Debug.Log(getFilePath("progress.json"));
        if (map != null)
        Debug.Log("2");
            map2.SetActive(false);
    }
}
