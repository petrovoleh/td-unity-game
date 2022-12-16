using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{

    private static SaveGameManager instance;

    public List<SaveableObject> SaveableObjects { get; private set; }

    public static SaveGameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<SaveGameManager>();
            }
            return instance;
        }
       
    }

    void Awake()
    {
        SaveableObjects = new List<SaveableObject>();
    }
    public void Save()
    {
        PlayerPrefs.SetInt("ObjectCount", SaveableObjects.Count);
        for(int i = 0; i < SaveableObjects.Count; i++)
        {
            SaveableObjects[i].Save(i);
        }
    }
    public void Load()
    {
        foreach(SaveableObject obj in SaveableObjects)
        {
            if(obj != null)
            {
                Destroy(obj.gameObject);
            }
        }

        SaveableObjects.Clear();

        int objectCount = PlayerPrefs.GetInt("ObjectCount");

        for(int i = 0; i < objectCount; i++)
        {
            string[] value = PlayerPrefs.GetString(i.ToString()).Split('_');
            GameObject tmp = null;
            switch (value[0])
            {
                case "Tower":
                    tmp = Instantiate(Resources.Load("Tower1(Clone)") as GameObject);
                    break;
                case "Grass":
                    tmp = Instantiate(Resources.Load("Grass(Clone)") as GameObject);
                    break;
                case "Sand":
                    tmp = Instantiate(Resources.Load("Sand(Clone)") as GameObject);
                    break;
                case "GameManager":
                    tmp = Instantiate(Resources.Load("GameManager") as GameObject);
                    break;

            }

            if(tmp != null)
            {
                tmp.GetComponent<SaveableObject>().Load(value);
            }

        }
    }

    public Vector3 StringToVector(string value)
    {
        //(1, 23, 3)
        value = value.Trim(new char[] { '(',')' });
        //1, 23, 3
        value = value.Replace(" ", "");
        //1,23,3
        string[] pos = value.Split(',');
        //[0]=1 [1]=23 [2]=3
        return new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
    }

    public Quaternion StringToQuaternion(string value)
    {
        return Quaternion.identity;
    }
    
    
}
