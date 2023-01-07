using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharedLibrary;
using System;
using System.IO;
using _Scripts;
using System.Linq;
public class SaveProgress : MonoBehaviour
{
    
    private PlayerProgress progress;
    
    public static int mapID;
    private User user;
        private void ReadFromFile(string fileName)
    {
        string path = getFilePath(fileName);
        try {
            using FileStream fs = File.OpenRead(path);
            using var sr = new StreamReader(fs);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                if (fileName=="progress.json")
                progress = JsonUtility.FromJson<PlayerProgress>(line);
                else
                user = JsonUtility.FromJson<User>(line);
            }
        }
        catch (Exception) {
            Debug.Log("file does not exist"); 
        }

    }
    private void saveToFile(object obj, string fileName)
    {
        string json = JsonUtility.ToJson(obj);
        writeToFile(fileName, json);
    }


    private void writeToFile(string fileName, string json)
    {
        string path = getFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string getFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
    public async void SaveMap(){
        progress = new PlayerProgress();
        progress.maps = new List<Map>();
        ReadFromFile("progress.json");
        ReadFromFile("logindata.json");
        //UserData.progress.maps contains mapID;
        Debug.Log(mapID);
        var map = progress.maps.FirstOrDefault(maps => maps.Map_id == mapID);
        if (map != null){
            Debug.Log("exists");
        }
        else{
            Map newmap = new Map();
            newmap.Map_id = mapID;
            newmap.Difficulty = 1;
            progress.maps.Add(newmap);
            saveToFile(progress,"progress.json");
        }
        //open user token
        //if usr token is not availible do nothing
        //else post
        Debug.Log(progress.maps[3].Map_id);
        //progress = await HttpClient.Post<PlayerProgress>("playermaps", progress, user.Token);
    }
}
