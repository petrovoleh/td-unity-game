using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool fastForward = false;
    [SerializeField]
    private Sprite[] fastForwardSprites;
    [SerializeField]
    private Image fastForwardButton;
    
    public static GameManager Instance;

    public ObjectPool Pool { get; set; }

    //Currency Variables
    [SerializeField]
    private int currency;

    [SerializeField]
    private int roundEndingCurrency;

    [SerializeField]
    private Text currencyTxt;

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

    //Player Health Variables
    [SerializeField]
    private int playerHP = 20;

    public int PlayerHP
    {
        get
        {
            return playerHP;
        }
        set
        {
            playerHP = value;
        }
    }

    // Wave Variables
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

    [HideInInspector]
    public int enemyCount;

    [SerializeField]
    private GameObject waveBtn;
    public void activateWaveBtn(){
        Debug.Log(enemyCount);
        if(!WaveActive && enemyCount <= 0)
        {
            Currency += roundEndingCurrency;
            waveBtn.SetActive(true);
        }
    }
    public List<Enemy> activeMonsters = new List<Enemy>();

    /*private Tower selectedTower;

    public void SelectTower(Tower tower)
    {
        selectedTower = tower;
        selectedTower.Select();
    }*/
    void Start()
    {
        Currency = currency;
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
                yield return new WaitForSeconds(1f);

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
                Enemy enemy = Pool.GetObject("Wolf").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);

            }
        }
        if (wave == 4)
        {
            enemyCount = 6;
            for (int e = 0; e < 3; e++)
            {
                
                Enemy enemy = Pool.GetObject("Wolf").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);
                enemy = Pool.GetObject("Zombie").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);

            }
        }
        if (wave == 5)
        {
            enemyCount = 9;
            for (int e = 0; e < 3; e++)
            {
                Enemy enemy = Pool.GetObject("Skeleton").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1f);
                enemy = Pool.GetObject("Zombie").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);
                enemy = Pool.GetObject("Wolf").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);

            }
        }
        if (wave == 6)
        {
            enemyCount = 15;
            for (int e = 0; e < 5; e++)
            {
                Enemy enemy = Pool.GetObject("Skeleton").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1f);
                enemy = Pool.GetObject("Zombie").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);
                enemy = Pool.GetObject("Wolf").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);
            }
        }
        if (wave == 7)
        {
            enemyCount = 16;
            for (int e = 0; e < 8; e++)
            {
                Enemy enemy = Pool.GetObject("Zombie").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);
                enemy = Pool.GetObject("Wolf").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);
            }
        }
        if (wave == 8)
        {
            enemyCount = 20;
            for (int e = 0; e < 10; e++)
            {
                Enemy enemy = Pool.GetObject("Skeleton").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);
                enemy = Pool.GetObject("Wolf").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);
            }
        }
        if (wave == 9)
        {
            enemyCount = 16;
            for (int e = 0; e < 16; e++)
            {
                Enemy enemy = Pool.GetObject("Wolf").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);
            }
        }
        if (wave == 10)
        {
            enemyCount = 40;
            for (int e = 0; e < 20; e++)
            {
                Enemy enemy = Pool.GetObject("Zombie").GetComponent<Enemy>();
                enemy.Spawn();
                yield return new WaitForSeconds(1.5f);
                enemy = Pool.GetObject("Wolf").GetComponent<Enemy>();
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
            Currency += roundEndingCurrency;
            waveBtn.SetActive(true);
        }
    }

    public void FastForward()
    {
        if(fastForward == false)
        {
            fastForwardButton.sprite = fastForwardSprites[1];
            Time.timeScale = 2f;
            fastForward = true;
        }
        else if (fastForward == true)
        {
            fastForwardButton.sprite = fastForwardSprites[0];
            Time.timeScale = 1f;
            fastForward = false;
        }

    }
}
