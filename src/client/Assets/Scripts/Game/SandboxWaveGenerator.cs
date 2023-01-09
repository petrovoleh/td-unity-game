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
        {"GreenSlime", 5},
        {"YellowSlime", 6},
        {"Murloc", 7},
        {"Ghost", 8},
        {"Golem", 9},
        {"Wolf", 10},
        {"Goblin", 11},
        {"HobGoblin", 12},
        {"PurpleSlime", 13},
        {"Spider", 14},
        {"OrangeSlime", 15},
        {"Ogre", 20},
        {"SlimeKing", 30},
        {"GolemLord", 40},
        {"Dragon", 50},
        {"Dracula", 60},
        {"GoblinKing", 90},
    };
    

    private void firstWave()
    {
        this.gameObject.GetComponent<GameManager>().enemyCount = 5;
        string[] mobs = { "RedSlime" };
        StartCoroutine(this.gameObject.GetComponent<GameManager>().WaveGenerator(5, mobs, 1f));
    }
    private int calculateAmount(){
        difficulty+=10;
        if (previusHP>GameManager.Instance.PlayerHP)
            difficulty -= previusHP-GameManager.Instance.PlayerHP;
        else
            difficulty += wave;
        Debug.Log (difficulty);
        return  difficulty;

    }
    public void spawnWave()
    {
        waveBtn.SetActive(false);
        this.gameObject.GetComponent<GameManager>().wave +=1;
        wave +=1;
        if (this.gameObject.GetComponent<GameManager>().wave == 1)
            firstWave();
        else
            StartCoroutine(GenerateWave());
        previusHP=GameManager.Instance.PlayerHP;
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
