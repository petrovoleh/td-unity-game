using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;



public delegate void CurrencyChanged();

public class GameManager : Singleton<GameManager>, ISaveable
{
    [SerializeField]
    private float spawnpointX;
    public float SpawnpointX { get { return spawnpointX; } }
    [SerializeField]
    private float spawnpointY;
    public float SpawnpointY { get { return spawnpointY; }  }
    public event CurrencyChanged Changed;

    private bool fastForward = false;
    [SerializeField]
    private Sprite[] fastForwardSprites;
    [SerializeField]
    private Image fastForwardButton;
 
    public ObjectPool Pool { get; set; }

    [SerializeField]
    private GameObject WinningTheGameScreen;

    [SerializeField]
    private GameObject CancelBtn;

    public TowerBtn ClickedBtn { get; set; }

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

            OnCurrencyChanged();
        }
    }

    //Player Health Variables
    [SerializeField]
    private int playerHP;

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

    //current selected tower
    private Tower selectedTower;

    private GameObject selectedTowerPlace;
    public GameObject SelectedTowerPlace
    {
        get
        {
            return selectedTowerPlace;
        }
        set
        {
            selectedTowerPlace = value;
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

    [SerializeField]
    private GameObject upgradePanel;

    [SerializeField]
    private Text sellText;

    public void activateWaveBtn(){
        Debug.Log(enemyCount);
        if(!WaveActive && enemyCount <= 0)
        {
            Currency += roundEndingCurrency;
            waveBtn.SetActive(true);
        }
    }
    public List<Enemy> activeMonsters = new List<Enemy>();

   

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if(sceneName == "LoadMap")
        {
            SaveGameManager.Instance.Load();
            SaveLoadSystem.Instance.Load();
        }
        
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        Currency = currency;
    }

    void Update()
    {
        HandleEscape();
        waveTxt.text = string.Format("Wave: {0}/20", wave);
        currencyTxt.text = currency.ToString();
    }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    public void StartWave()
    {
        if (PauseMenu.GameIsPaused == false)
        {
            wave++;

            waveTxt.text = string.Format("Wave: {0}/20", wave);
            StartCoroutine(SpawnWave());

            waveBtn.SetActive(false);
        }
        
    }

    
    public void DeselectTower()
    {
        if (PauseMenu.GameIsPaused == false)
        {
            if (selectedTower != null)
            {
                selectedTower.Select();
            }
            upgradePanel.SetActive(false);

            selectedTower = null;
        }   

    }
    
    public void SelectTower(Tower tower)
    {
        if (PauseMenu.GameIsPaused == false)
        {
            if (selectedTower != null)
            {
                selectedTower.Select();
            }

            selectedTower = tower;

            selectedTower.Select();

            sellText.text = " + " + (selectedTower.Price / 2).ToString();

            upgradePanel.SetActive(true);
        }
            
    }

    public void PickTower(TowerBtn towerBtn)
    {
        if (PauseMenu.GameIsPaused == false)
        {
            Hover.Instance.Deactivate();
            if (Currency >= towerBtn.Price)
            {
                this.ClickedBtn = towerBtn;
                if(towerBtn.name == "ElectroWizard Button")
                {
                    Hover.Instance.SniperActivate(towerBtn.Sprite);
                }
                else
                {
                    Hover.Instance.Activate(towerBtn.Sprite);
                }

                CancelBtn.SetActive(true);
            }
        } 
    }
    public void CancelPicking()
    {
        Hover.Instance.Deactivate();
        CancelBtn.SetActive(false);
    }

    public void BuyTower()
    {
        if (PauseMenu.GameIsPaused == false)
        {
            if (Currency >= ClickedBtn.Price)
            {
                Currency -= ClickedBtn.Price;

                Hover.Instance.Deactivate();
                CancelBtn.SetActive(false);
            }
        }

    }
    
    public void OnCurrencyChanged()
    {
        if(Changed != null)
        {
            Changed();
        }
    }
    
    public void SellTower()
    {
        if (PauseMenu.GameIsPaused == false)
        {
            if (selectedTower != null)
            {
                Currency += selectedTower.Price / 2;

                selectedTower.GetComponentInParent<TileScript>().IsEmpty = true;

                selectedTower.GetComponentInParent<SpecificObject>().ObjectType = "Grass";
                //Destroy(selectedTower.transform.parent.gameObject);

                Destroy(selectedTower.transform.parent.gameObject);

                DeselectTower();
            }
        }
            
    }
     

    private void HandleEscape()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }

    public IEnumerator SlimeKingDamagedSpawn()
    {
        int monsterIndex = UnityEngine.Random.Range(0, 5);

        string type = string.Empty;

        switch (monsterIndex)
        {
            case 0:
                type = "RedSlime";
                break;
            case 1:
                type = "BlueSlime";
                break;
            case 2:
                type = "GreenSlime";
                break;
            case 3:
                type = "PurpleSlime";
                break;
            case 4:
                type = "YellowSlime";
                break;
        }
        
        Enemy enemy = Pool.GetObject(type).GetComponent<Enemy>();
        enemy.Spawn();
        activeMonsters.Add(enemy);
        yield return new WaitForSeconds(0.5f);
    }


    public void RemoveMonster(Enemy enemy)
    {
        activeMonsters.Remove(enemy);

        if(!WaveActive && enemyCount <= 0 && activeMonsters.Count == 0)
        {
            if(wave == 20)
            {
                Time.timeScale = 0f;
                PauseMenu.GameIsPaused = true;
                WinningTheGameScreen.SetActive(true);
                this.gameObject.GetComponent<SaveProgress>().SaveMap();
            }
            else 
            {
                Currency += roundEndingCurrency;
                waveBtn.SetActive(true);
                PlayerPrefs.DeleteKey("ObjectCount");
                SaveGameManager.Instance.Save();
                SaveLoadSystem.Instance.Save();
            }
            
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

    public object SaveState()
    {
        Debug.Log("wave " + wave);
        return new SaveData()
        {
            wave = this.wave,
            playerHP = this.playerHP,
            currency = this.currency
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        Debug.Log("wave " + wave);
        wave = saveData.wave;
        playerHP = saveData.playerHP;
        currency = saveData.currency;
    }

    [Serializable]
    private struct SaveData
    {
        public int wave;
        public int playerHP;
        public int currency;
    }

    private IEnumerator WaveGenerator(int count, string[] names, float delay)
    {
        for (int e = 0; e < count / names.Length; e++)
        {
            for (int f = 0; f < names.Length; f++)
            {
                Enemy enemy = Pool.GetObject(names[f]).GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(delay);
            }
        }
    }

    private IEnumerator SpawnWave()
    {
        if (wave == 1)
        {
            enemyCount = 5;
            string[] mobs = { "RedSlime" };
            StartCoroutine(WaveGenerator(5, mobs, 1f));
        }
        if (wave == 2)
        {
            enemyCount = 5;
            string[] mobs = { "BlueSlime" };
            StartCoroutine(WaveGenerator(5, mobs, 1.5f));
        }

        if (wave == 3)
        {
            enemyCount = 6;
            for (int e = 0; e < 3; e++)
            {
                Enemy enemy = Pool.GetObject("BlueSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(1f);
                enemy = Pool.GetObject("RedSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(1f);

            }
        }

        if (wave == 4)
        {
            enemyCount = 8;
            string[] mobs = { "GreenSlime", "BlueSlime" };
            StartCoroutine(WaveGenerator(8, mobs, 0.5f));

        }
        if (wave == 5)
        {
            enemyCount = 16;
            string[] mobs = { "RedSlime", "Spider" };
            StartCoroutine(WaveGenerator(6, mobs, 1f));
            yield return new WaitForSeconds(3f);
            string[] mobs2 = { "RedSlime" };
            StartCoroutine(WaveGenerator(10, mobs2, 0.2f));

        }
        if (wave == 6)
        {
            enemyCount = 18;
            for (int e = 0; e < 3; e++)
            {
                Enemy enemy = Pool.GetObject("GreenSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(0.75f);
                enemy = Pool.GetObject("BlueSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(0.5f);
                enemy = Pool.GetObject("RedSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(0.5f);
                enemy = Pool.GetObject("BlueSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(0.75f);
                enemy = Pool.GetObject("GreenSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(1f);
                enemy = Pool.GetObject("PurpleSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(1f);
            }
        }
        if (wave == 7)
        {
            enemyCount = 8;
            string[] mobs = { "GreenSlime" };
            StartCoroutine(WaveGenerator(4, mobs, 0.5f));
            string[] mobs2 = { "PurpleSlime" };
            StartCoroutine(WaveGenerator(4, mobs2, 0.5f));

        }
        if (wave == 8)
        {
            enemyCount = 11;
            string[] mobs = { "Goblin" };
            StartCoroutine(WaveGenerator(5, mobs, 0.25f));
            string[] mobs2 = { "PurpleSlime" };
            StartCoroutine(WaveGenerator(6, mobs2, 1f));
        }
        if (wave == 9)
        {
            enemyCount = 18;
            for (int e = 0; e < 6; e++)
            {
                Enemy enemy = Pool.GetObject("PurpleSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(1f);
                enemy = Pool.GetObject("YellowSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(0.25f);
                enemy = Pool.GetObject("YellowSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(1f);
            }
        }
        if (wave == 10)
        {
            enemyCount = 1;

            Enemy enemy = Pool.GetObject("SlimeKing").GetComponent<Enemy>();
            enemy.Spawn(); activeMonsters.Add(enemy);
            yield return new WaitForSeconds(1.5f);
        }

        if (wave == 11)
        {
            enemyCount = 11;

            for (int e = 0; e < 5; e++)
            {
                Enemy enemy = Pool.GetObject("Goblin").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(1f);
                
                enemy = Pool.GetObject("YellowSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(0.25f); 
            }

            string[] mobs = { "Murloc" };
            StartCoroutine(WaveGenerator(4, mobs, 0.5f));

        }

        if (wave == 12)
        {
            enemyCount = 12;
            for (int e = 0; e < 6; e++)
            {
                Enemy enemy = Pool.GetObject("PurpleSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(1f);
                enemy = Pool.GetObject("OrangeSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(0.25f);;
            }
        }

        if (wave == 13)
        {
            enemyCount = 20;
            for (int e = 0; e < 10; e++)
            {
                Enemy enemy = Pool.GetObject("Murloc").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(0.1f);
                enemy = Pool.GetObject("Goblin").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(0.25f); ;
            }
        }

        if (wave == 14)
        {
            enemyCount = 8;
            for (int e = 0; e < 8; e++)
            {
                Enemy enemy = Pool.GetObject("HobGoblin").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(0.25f); ;
            }
        }

        if (wave == 15)
        {
            enemyCount = 7;

            Enemy enemy = Pool.GetObject("SlimeKing").GetComponent<Enemy>();
            enemy.Spawn(); activeMonsters.Add(enemy);
            yield return new WaitForSeconds(1.5f);

            for (int e = 0; e < 6; e++)
            {
                enemy = Pool.GetObject("OrangeSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(0.25f);
            }
        }

        if (wave == 16)
        {
            enemyCount = 1;

            string[] mobs = { "Ogre" };
            StartCoroutine(WaveGenerator(6, mobs, 1f));  
  
        }

        if (wave == 17)
        {
            enemyCount = 1;

            string[] mobs = { "Murloc" };
            StartCoroutine(WaveGenerator(6, mobs, 1f));

        }

        if (wave == 18)
        {
            enemyCount = 12;

            for (int e = 0; e < 6; e++)
            {
                Enemy enemy = Pool.GetObject("Ogre").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(2f);

            }
            for (int e = 0; e < 6; e++)
            {
                Enemy enemy = Pool.GetObject("Murloc").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(1f);
            }

        }
        if (wave == 19)
        {
            enemyCount = 1;

            string[] mobs = { "SlimeKing" };
            StartCoroutine(WaveGenerator(2, mobs, 4f));

        }
        if (wave == 20)
        {
            enemyCount = 1;

            Enemy enemy = Pool.GetObject("GoblinKing").GetComponent<Enemy>();
            enemy.Spawn(); activeMonsters.Add(enemy);
            yield return new WaitForSeconds(1.5f);
        }

        if (wave == 26)
        {
            enemyCount = 1;

            Enemy enemy = Pool.GetObject("Golem").GetComponent<Enemy>();
            enemy.Spawn(); activeMonsters.Add(enemy);
            yield return new WaitForSeconds(1.5f);
        }

        if (wave == 30)
        {
            enemyCount = 1;

            Enemy enemy = Pool.GetObject("GolemLord").GetComponent<Enemy>();
            enemy.Spawn(); activeMonsters.Add(enemy);
            yield return new WaitForSeconds(1.5f);
        }

        if (wave == 33)
        {
            enemyCount = 1;

            Enemy enemy = Pool.GetObject("Ghost").GetComponent<Enemy>();
            enemy.Spawn(); activeMonsters.Add(enemy);
            yield return new WaitForSeconds(1.5f);
        }

        if (wave == 40)
        {
            enemyCount = 1;

            Enemy enemy = Pool.GetObject("Dracula").GetComponent<Enemy>();
            enemy.Spawn(); activeMonsters.Add(enemy);
            yield return new WaitForSeconds(1.5f);
        }

        if (wave == 50)
        {
            enemyCount = 1;

            Enemy enemy = Pool.GetObject("Dragon").GetComponent<Enemy>();
            enemy.Spawn(); activeMonsters.Add(enemy);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
