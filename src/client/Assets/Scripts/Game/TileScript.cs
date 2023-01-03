using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }

    SpecificObject specificObject;

    [SerializeField]
    private bool isEmpty;
    public bool IsEmpty 
    {
        get
        {
            return isEmpty;
        }
        set
        {
            this.isEmpty = value;
        }
    }

    public enum TileType { Tile, Path};
    [SerializeField]
    private TileType tileType;

    private Tower myTower;

    private Color32 fullColor = new Color32(255, 64, 64, 255);

    private Color32 emptyColor = new Color32(0, 168, 0, 255);

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        IsEmpty = true;
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPos, this);
        
    }

    private void OnMouseOver()
    {

        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {
            if (tileType == TileType.Tile)
            {
                if (IsEmpty)//colors the tile green if the is no tower on tile
                {
                    ColorTile(emptyColor);
                }
                if (!IsEmpty)//colors the tile red when the tile has already a tower
                {
                    ColorTile(fullColor);
                }
                else if (Input.GetMouseButtonDown(0))//places a tower if there is no other towers on the tile
                {
                    PlaceTower();
                }
            }
        }
        else if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn == null && Input.GetMouseButtonDown(0))
        {
            if (myTower != null)
            {
                GameManager.Instance.SelectTower(myTower);
            }
            else
            {
                GameManager.Instance.DeselectTower();
            }
        }
    }

    private void OnMouseExit()
    {
        ColorTile(Color.white);
    }

    private void PlaceTower()
    {

            GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);

            tower.transform.SetParent(transform);

            this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();

        if(myTower.TowerType == "FireWizard")
        {
            specificObject = GetComponent<SpecificObject>();
            specificObject.ObjectType = "FireGrass";
        }
        if (myTower.TowerType == "ElectricWizard")
        {
            specificObject = GetComponent<SpecificObject>();
            specificObject.ObjectType = "ElectricGrass";
        }
        if (myTower.TowerType == "RocketWizard")
        {
            specificObject = GetComponent<SpecificObject>();
            specificObject.ObjectType = "RocketGrass";
        }
        if (myTower.TowerType == "GooWizard")
        {
            specificObject = GetComponent<SpecificObject>();
            specificObject.ObjectType = "GooGrass";
        }
        if (myTower.TowerType == "IceWizard")
        {
            specificObject = GetComponent<SpecificObject>();
            specificObject.ObjectType = "IceGrass";
        }

        IsEmpty = false;
            
            myTower.Price = GameManager.Instance.ClickedBtn.Price;

        //Destroy(this.gameObject); 
        ColorTile(Color.white);

            GameManager.Instance.BuyTower();

    }

    public void SavePlaceTower(string towerType)
    {
        if(towerType == "FireGrass")
        {
            specificObject = GetComponent<SpecificObject>();
            specificObject.ObjectType = "FireGrass";

            GameObject tower = Instantiate(Resources.Load("Tower1") as GameObject);
            tower.transform.SetParent(transform);
            tower.transform.localPosition = Vector2.zero;
            this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();
            myTower.Price = 55;
        }
        if (towerType == "ElectricGrass")
        {
            specificObject = GetComponent<SpecificObject>();
            specificObject.ObjectType = "ElectricGrass";

            GameObject tower = Instantiate(Resources.Load("Tower2") as GameObject);
            tower.transform.SetParent(transform);
            tower.transform.localPosition = Vector2.zero;
            this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();
            myTower.Price = 60;
        }
        if (towerType == "RocketGrass")
        {
            specificObject = GetComponent<SpecificObject>();
            specificObject.ObjectType = "RocketGrass";

            GameObject tower = Instantiate(Resources.Load("Tower3") as GameObject);
            tower.transform.SetParent(transform);
            tower.transform.localPosition = Vector2.zero;
            this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();
            myTower.Price = 70;
        }
        if (towerType == "GooGrass")
        {
            specificObject = GetComponent<SpecificObject>();
            specificObject.ObjectType = "GooGrass";

            GameObject tower = Instantiate(Resources.Load("Tower4") as GameObject);
            tower.transform.SetParent(transform);
            tower.transform.localPosition = Vector2.zero;
            this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();
            myTower.Price = 65;
        }
        if (towerType == "IceGrass")
        {
            specificObject = GetComponent<SpecificObject>();
            specificObject.ObjectType = "IceGrass";

            GameObject tower = Instantiate(Resources.Load("Tower5") as GameObject);
            tower.transform.SetParent(transform);
            tower.transform.localPosition = Vector2.zero;
            this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();
            myTower.Price = 90;
        }

    }



    private void ColorTile(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

}
