using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void CurrencyChanged();

public class GameManager : Singleton<GameManager>
{

    public event CurrencyChanged Changed;

    private bool fastForward = false;
    [SerializeField]
    private Sprite[] fastForwardSprites;
    [SerializeField]
    private Image fastForwardButton;
 
    public ObjectPool Pool { get; set; }

    [SerializeField]
    private GameObject WinningTheGameScreen;

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
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        Currency = currency;
    }

    void Update()
    {
        HandleEscape();
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

            waveTxt.text = string.Format("Wave: {0}/10", wave);
            StartCoroutine(SpawnWave());

            waveBtn.SetActive(false);
        }
        
    }

    
    public void DeselectTower()
    {
        if(selectedTower != null)
        {
            selectedTower.Select();
        }
        upgradePanel.SetActive(false);

        selectedTower = null;

    }
    
    public void SelectTower(Tower tower)
    {
        if(selectedTower != null)
        {
            selectedTower.Select();
        }
        
        selectedTower = tower;

        selectedTower.Select();

        sellText.text = " + " + (selectedTower.Price / 2).ToString();
        
        upgradePanel.SetActive(true);
    }

    public void PickTower(TowerBtn towerBtn)
    {
        if(Currency >= towerBtn.Price)
        {
            this.ClickedBtn = towerBtn;
            Hover.Instance.Activate(towerBtn.Sprite);
        }
        
    }

    public void BuyTower()
    {
        if(Currency >= ClickedBtn.Price)
        {
            Currency -= ClickedBtn.Price;

            Hover.Instance.Deactivate();
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
        if(selectedTower != null)
        {
            Currency += selectedTower.Price / 2;

            selectedTower.GetComponentInParent<TileScript>().IsEmpty = true;

            Destroy(selectedTower.transform.parent.gameObject);

            DeselectTower();
        }
    }
     

    private void HandleEscape()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }

    private IEnumerator WaveGenerator(int count, string[] names, float delay){
            for (int e = 0; e < count/names.Length; e++)
            {
                for (int f = 0; f < names.Length; f++){
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
            string[] mobs =  {"Skeleton"};
            StartCoroutine(WaveGenerator(5,mobs, 1f));
        }
        if (wave == 2)
        {
            enemyCount = 5;
            string[] mobs =  {"Zombie"};
            StartCoroutine(WaveGenerator(5, mobs, 1.5f));
        }

        if (wave == 3)
        {
            enemyCount = 4;
            string[] mobs =  {"Wolf"};
            StartCoroutine(WaveGenerator(4, mobs, 1.5f));
        }
        if (wave == 4)
        {
            enemyCount = 8;
            string[] mobs =  {"Wolf","Zombie"};
            StartCoroutine(WaveGenerator(8, mobs, 0.5f));

        }
        if (wave == 5)
        {
            enemyCount = 16;
            string[] mobs = {"Skeleton","Spider"};
            StartCoroutine(WaveGenerator(6, mobs, 1f));
            yield return new WaitForSeconds(3f);
            string[] mobs2 = {"RedSlime"};
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
                enemy = Pool.GetObject("YellowSlime").GetComponent<Enemy>();
                enemy.Spawn(); activeMonsters.Add(enemy);
                yield return new WaitForSeconds(1f);
            }
        }
        if (wave == 7)
        {
            enemyCount = 16;
            string[] mobs = {"GreenSlime"};
            StartCoroutine(WaveGenerator(8, mobs, 0.5f));
            string[] mobs2 = {"YellowSlime"};
            StartCoroutine(WaveGenerator(10, mobs2, 0.5f));
            
        }
        if (wave == 8)
        {
            enemyCount = 15;
            string[] mobs =  {"Spider"};
            StartCoroutine(WaveGenerator(5, mobs, 0.25f));
            string[] mobs2 = {"YellowSlime"};
            StartCoroutine(WaveGenerator(10, mobs2, 1f));
        }
        if (wave == 9)
        {
            enemyCount = 24;
            for (int e = 0; e < 8; e++)
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
            enemyCount = 31;

            Enemy enemy = Pool.GetObject("SlimeKing").GetComponent<Enemy>();
            enemy.Spawn(); activeMonsters.Add(enemy);
            yield return new WaitForSeconds(1.5f);
            for (int e = 0; e < 30; e++)
            {
                int monsterIndex = Random.Range(0, 5);

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
                enemy = Pool.GetObject(type).GetComponent<Enemy>();
                enemy.Spawn();
                activeMonsters.Add(enemy);
                yield return new WaitForSeconds(0.5f);
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

        if(!WaveActive && enemyCount <= 0 && activeMonsters.Count == 0)
        {
            if(wave == 10)
            {
                Time.timeScale = 0f;
                PauseMenu.GameIsPaused = true;
                WinningTheGameScreen.SetActive(true);
            }
            else 
            {
                Currency += roundEndingCurrency;
                waveBtn.SetActive(true);
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

    
}
