using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    //[SerializeField]
    //private GameObject waypointPrefabs;

    [SerializeField]
    private string mapNumber;

    public string MapNumber 
    { 
        get
        {
            return mapNumber;
        }
    }

    [SerializeField]
    private Transform background;

    public float TileSize 
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
 
    }
    public Dictionary<Point, TileScript> Tiles { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
        GetLevelID();
    }
    private void GetLevelID(){
        if (mapNumber[0]=='M')
            SaveProgress.mapID = Int32.Parse(mapNumber.Remove(0,3));
        else
            SaveProgress.challengeID = Int32.Parse(mapNumber.Remove(0,9));
    }


    private void CreateLevel()
    {
        if(mapNumber == "-")
        {
            
        }
        else
        {
            Tiles = new Dictionary<Point, TileScript>();

            string[] mapData = ReadLevelText();

            //calculates the map x size
            int mapX = mapData[0].ToCharArray().Length;

            //calculates the map y size
            int mapY = mapData.Length;

            //Calculates the world start point, this is the top left corner of screen
            Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height-(Screen.height/13) ));

            for (int y = 0; y < mapY; y++) //y position of tiles
            {
                char[] newTiles = mapData[y].ToCharArray(); //gets the tile type

                for (int x = 0; x < mapX; x++)//x possition of tiles
                {
                    PlaceTile(newTiles[x].ToString(), x, y, worldStart);

                }
            }
        }
        
    }


    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        //parses the tiletype to an int, so that we can use it as an int when we create a tile
        int tileIndex = int.Parse(tileType);

        //Creates a new tile and makes reference to that tile in the newTile variable
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        /* //failed atempt at making waypoints automatic
        if (tileIndex == 2)
        {
            Vector3 waypointSpawnPosition = new Vector3(25, -30, 0);
            GameObject newObject = Instantiate(waypointPrefabs, waypointSpawnPosition, Quaternion.identity, newTile.transform);
            Waypoints.Instance.AddWaypoints(newObject);
        }*/

        //Uses the new tile variable to change the position of the tile
        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), background);

        
    }

    private string[] ReadLevelText()
    {
        if (mapNumber == "-")
        {
            return null;
        }
        else
        {
            TextAsset bindData = Resources.Load(mapNumber) as TextAsset;

            string data = bindData.text.Replace(Environment.NewLine, string.Empty);

            return data.Split("-");
        }
        
    }

   
}
