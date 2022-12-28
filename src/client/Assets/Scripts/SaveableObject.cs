using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ObjectType { FireTower, Grass, Sand, Waypoints, GameManager}
public abstract class SaveableObject : MonoBehaviour
{
    protected string save;


    [SerializeField]
    private ObjectType objectType;
    // Start is called before the first frame update
    private void Start()
    {
        SaveGameManager.Instance.SaveableObjects.Add(this);
    }

    public virtual void Save(int id)
    {
        PlayerPrefs.SetString(id.ToString(), objectType + "_" + transform.position.ToString());
    }
    public virtual void Load(string[] values)
    {
        transform.localPosition = SaveGameManager.Instance.StringToVector(values[1]);
    }
    public void DestroySaveable()
    {

    }

}
