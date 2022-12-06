using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;
    private float nextActionTime = 0.0f;
    private float period = 2f;
    //private GameObject[] enemies = new GameObject[100];

    // Update is called once per frame
    void Update()
    {
    if (Time.time > nextActionTime ) {
            nextActionTime += period;
            GameObject Enemy = Instantiate(enemy);
            Enemy.transform.position = new Vector3(320, 135);
            Enemy.SetActive(true);
        }
        //Destroy(Enemy);
    }
}