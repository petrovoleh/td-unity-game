using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveableEntity : SaveableObject
{
    [SerializeField] private string id;

    public string Id => id;

    /*
    void Start()
    {
        GenerateId();
    }*/
    void Update()
    {
        //GameObject gm = null;
        //gm.GetComponent<SaveGameManager>().Save();
    }
    [ContextMenu("Generate Id")]
    private void GenerateId()
    {
        id = Guid.NewGuid().ToString();
    }
    
    // find all Isaveable components on gameobject
    public object SaveState()
    {
        var state = new Dictionary<string, object>();
        foreach (var saveable in GetComponents<ISaveable>())
        {
            state[saveable.GetType().ToString()] = saveable.SaveState();
        }
        return state;
    }
    public void LoadState(object state)
    {
        var stateDictionary = (Dictionary<string, object>)state;

        foreach(var saveable in GetComponents<ISaveable>())
        {
            string typeName = saveable.GetType().ToString();
            if(stateDictionary.TryGetValue(typeName, out object savedState))
            {
                saveable.LoadState(savedState);
            }
        }
    }

    public override void Save(int id)
    {
        base.Save(id);
    }

    public override void Load(string[] values)
    {
        base.Load(values);
    }
}