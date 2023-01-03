using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*enum ObjectType
{
    FireTower,
    Grass,
    Sand,
    GrassFireTower,
    Waypoints,
    GameManager
}*/
public abstract class SaveableObject : MonoBehaviour
{
    protected string save;


    [SerializeField]
    private string objectType;

    public string ObjectType
    {
        get
        {
            return objectType;
        }
        set
        {
            objectType = value;
        }
    }

    [SerializeField]
    private string towerPlaced;

    public string TowerPlaced
    {
        get
        {
            return towerPlaced;
        }
        set
        {
            towerPlaced = value;
        }
    }

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
        SaveGameManager.Instance.SaveableObjects.Remove(this);
        Destroy(gameObject);
    }

}
