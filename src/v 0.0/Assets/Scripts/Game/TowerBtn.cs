using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour
{
    [SerializeField]
    private int price;

    public int Price
    {
        get
        {
            return price;
        }
    }
    [SerializeField]
    private Text priceTxt;

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }

    [SerializeField]
    private GameObject towerPrefab;
    public GameObject TowerPrefab
    {
        get
        {
            return towerPrefab;
        }
    }

    private void Start()
    {
        priceTxt.text = price + "";

        GameManager.Instance.Changed += new CurrencyChanged(PriceCheck);
    }

    void Update()
    {
        PriceCheck();
    }
    private void PriceCheck()
    {
        if(price <= GameManager.Instance.Currency)
        {
            GetComponent<Image>().color = Color.white;
            priceTxt.color = Color.yellow;
            
        }
        else
        {
            GetComponent<Image>().color = Color.grey;
            priceTxt.color = Color.grey;
            
        }
    }
}
