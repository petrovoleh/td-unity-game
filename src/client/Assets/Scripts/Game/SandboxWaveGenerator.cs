using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SandboxWaveGenerator : MonoBehaviour
{
    private int difficulty=0;
    private int wave = 0;
    [SerializeField]
    private GameObject waveBtn;
    private int previusHP=20;
    
    IDictionary<string, int> Enemies = new Dictionary<string, int>(){
        {"RedSlime", 1},
        {"Skeleton", 2},
        {"Zombie", 3},
        {"BlueSlime", 4},
        {"GreenSlime", 8},
        {"YellowSlime", 16},
        {"Murloc", 12},
        {"Ghost", 20},
        {"Golem", 20},
        {"Wolf", 12},
        {"Goblin", 20},
        {"HobGoblin", 30},
        {"PurpleSlime", 30},
        {"Spider", 25},
        {"OrangeSlime", 40},
        {"Ogre", 80},
        {"SlimeKing", 80},
        {"GoblinKing", 90},
        {"GolemLord", 150},
        {"Dracula", 150},
        {"Dragon", 200},
        
    };
    

    private void firstWave()
    {
        this.gameObject.GetComponent<GameManager>().enemyCount = 5;
        string[] mobs = { "RedSlime" };
        StartCoroutine(this.gameObject.GetComponent<GameManager>().WaveGenerator(5, mobs, 1f));
    }
    private int calculateAmount(){
        difficulty+=2;
        if (previusHP>GameManager.Instance.PlayerHP)
        {
            difficulty -= previusHP - GameManager.Instance.PlayerHP;
        }
        else
        {
            difficulty += wave;
        }
        if(difficulty <= 0)
        {
            difficulty = 2;
        }
        Debug.Log (difficulty);
        return  difficulty;

    }
    public void spawnWave()
    {
        if (PauseMenu.GameIsPaused == false)
        {
            waveBtn.SetActive(false);
            this.gameObject.GetComponent<GameManager>().wave += 1;
            wave += 1;
            if (this.gameObject.GetComponent<GameManager>().wave == 1)
                firstWave();
            else
                StartCoroutine(GenerateWave());
            previusHP = GameManager.Instance.PlayerHP;
        }

    }
    private IEnumerator GenerateWave(){
        List<string> listEnemies = new List<string>{};
        int amount = calculateAmount();
        Debug.Log (amount);
        string enemy;
        int target=0;
        while (amount>0){
            enemy= null;
            while (enemy==null){
                target = Random.Range(1, amount);
                enemy= Enemies.FirstOrDefault(x => x.Value == target).Key;
            }
            amount -=target;
            listEnemies.Add(enemy);
        }
        this.gameObject.GetComponent<GameManager>().enemyCount = listEnemies.Count;
        for (int i = 0; i < listEnemies.Count; i++){
            string[] mobs = {listEnemies[i]};
            StartCoroutine(this.gameObject.GetComponent<GameManager>().WaveGenerator(1, mobs, 1f));
            yield return new WaitForSeconds(1f);
        }
    }
    //проверить как много жизней потратилось, если 0 то добавить больше мобов, если одна то столько же
    //если 2 то меньше
}
