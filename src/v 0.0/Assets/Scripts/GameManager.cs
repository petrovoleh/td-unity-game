using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    public ObjectPool Pool { get; set; }

    public int Currency
    {
        get
        {
            return currency;
        }
        set
        {
            this.currency = value;
            this.currencyTxt.text = value.ToString();
        }
    }
    private int currency;

    [SerializeField]
    private Text currencyTxt;

    public bool WaveActive
    {
        get 
        { 
            return activeMonsters.Count > 0; 
        }
    }

    private int wave = 0;

    [SerializeField]
    private Text waveTxt;

    [SerializeField]
    public int enemyCount;

    [SerializeField]
    private GameObject waveBtn;

    public List<Enemy> activeMonsters = new List<Enemy>();

    /*private Tower selectedTower;

    public void SelectTower(Tower tower)
    {
        selectedTower = tower;
        selectedTower.Select();
    }*/
    void Start()
    {
        Currency = 5;
    }

    private void Awake()
    {
        Instance = this;
        Pool = GetComponent<ObjectPool>();
    }

    public void StartWave()
    {
        if (PauseMenu.GameIsPaused == false)
        {
            wave++;

            waveTxt.text = string.Format("Wave: {0}/10", wave);
            StartCoroutine(SpawnWave());

            waveBtn.SetActive(false);
        }
        
    }
    private IEnumerator SpawnWave()
    {
        if (wave == 1)
        {
            enemyCount = 3;
            for (int e = 0; e < 3; e++)
            {
                Enemy enemy = Pool.GetObject("Skeleton").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);

            }
        }
        if (wave == 2)
        {
            enemyCount = 3;
            for (int e = 0; e < 3; e++)
            {
                Enemy enemy = Pool.GetObject("Zombie").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);

            }
        }

        if (wave == 3)
        {
            enemyCount = 3;
            for (int e = 0; e < 3; e++)
            {
                Enemy enemy = Pool.GetObject("Spider").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);

            }
        }

        /*
        for (int i = 0; i < wave; i++)
        {
            int monsterIndex = Random.Range(0, 5);

            string type = string.Empty;

            switch (monsterIndex)
            {
                case 0:
                    type = "Skeleton";
                    break;
                case 1:
                    type = "Zombie";
                    break;
                case 2:
                    type = "Spider";
                    break;
                case 3:
                    type = "Murloc";
                    break;
                case 4:
                    type = "Goblin";
                    break;
            }
            Enemy enemy = Pool.GetObject(type).GetComponent<Enemy>();

            enemy.Spawn();

              could be used for difficulty levels or increasing difficulty while playing
            if(wave % 3 == 0)
            {
                health += 5;
            }
            

            activeMonsters.Add(enemy);
            yield return new WaitForSeconds(1.5f);
    }*/

    }
    public void RemoveMonster(Enemy enemy)
    {
        activeMonsters.Remove(enemy);

        if(!WaveActive && enemyCount <= 0)
        {
            waveBtn.SetActive(true);
        }
    }
}
