using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }

    public bool IsEmpty { get; set; }

    private Tower myTower;

    private Color32 fullColor = new Color32(255, 64, 64, 255);

    private Color32 emptyColor = new Color32(0, 168, 0, 255);

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        IsEmpty = true;
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
        if (PauseMenu.GameIsPaused == false)
        {
            GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);

            tower.transform.SetParent(transform);

            this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();

            IsEmpty = false;

            myTower.Price = GameManager.Instance.ClickedBtn.Price;

            ColorTile(Color.white);

            GameManager.Instance.BuyTower();
        }

    }

    private void ColorTile(Color newColor)
    {
        spriteRenderer.color = newColor;
    }
}
