using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{

    public class CellData{
        public bool passable;
        public CellObject containedObject;
    }
    public ExitCellObject ExitCellPrefab;
    public WallObject WallPrefab;
    public FoodObject[] FoodPrefabs;

    private CellData[,] m_BoardData;
    private Tilemap m_Tilemap;

    private Grid m_Grid;

    private List<Vector2Int> m_EmptyCellsList; 

    public PlayerController Player;

    public int Height;
    public int Width;
    public int FoodCountRange;
    public Tile[] GroundTiles; 

    public Tile[] WallTiles;

    void AddObject(CellObject cellObject,Vector2Int position){
        CellData data = m_BoardData[position.x,position.y];
        cellObject.transform.position = CellToWorld(position);
        data.containedObject = cellObject;
        cellObject.Init(position);

    }
    void GenerateWall(){
        int wallCount = Random.Range(6,10);
        for(int i = 0 ; i < wallCount ; i++){
            int randomIndex = Random.Range(0,m_EmptyCellsList.Count);
            Vector2Int coord = m_EmptyCellsList[randomIndex];
            m_EmptyCellsList.RemoveAt(randomIndex);
            WallObject newWall = Instantiate(WallPrefab);
            AddObject(newWall,coord);
        }
    }

    public void Clean(){
        if(m_BoardData == null){
            return;
        }
        for(int y = 0 ; y < Height ; y++){
            for(int x = 0 ; x < Width ; x++){
                var CellData = m_BoardData[x,y];
                if(CellData.containedObject != null){
                    Destroy(CellData.containedObject.gameObject);
                }
                SetCellTile(new Vector2Int(x,y),null);
            }
        }
    }
    public Tile GetCellTile(Vector2Int cellIndex){
        return m_Tilemap.GetTile<Tile>(new Vector3Int(cellIndex.x,cellIndex.y,0));
    }
    public void SetCellTile(Vector2Int CellIndex,Tile tile){
        m_Tilemap.SetTile(new Vector3Int(CellIndex.x,CellIndex.y,0),tile);
    }

    void GenerateFood(){
        int foodCount = Random.Range(1,FoodCountRange);
        
        for(int i = 0 ; i < foodCount ; i++){
            int randomIndex = Random.Range(0,m_EmptyCellsList.Count);
            Vector2Int coord = m_EmptyCellsList[randomIndex];
            m_EmptyCellsList.RemoveAt(randomIndex);
            int foodPrefabindex = Random.Range(0,FoodPrefabs.Length);
            FoodObject newFood = Instantiate(FoodPrefabs[foodPrefabindex]);
            AddObject(newFood,coord);
        }
    }

    public Vector3 CellToWorld(Vector2Int cellIndex){
        return m_Grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }

    public CellData GetCellData(Vector2Int cellIndex){
        if(cellIndex.x < 0 || cellIndex.y < 0 || cellIndex.x >= Width || cellIndex.y >= Height){
            return null;
        }
        return m_BoardData[cellIndex.x,cellIndex.y];
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Init()
    {
        m_Tilemap = GetComponentInChildren<Tilemap>();
        m_BoardData = new CellData[Width,Height];
        m_Grid = GetComponentInChildren<Grid>();
        m_EmptyCellsList = new List<Vector2Int>();

        for(int y = 0; y < Height ; ++y){
            for(int x = 0 ; x < Width ; ++x){
                Tile tile;
                m_BoardData[x,y] = new CellData();
                if(x==0 || y==0 || x==Width-1 || y==Height-1){
                    tile = WallTiles[Random.Range(0,WallTiles.Length)];
                    m_BoardData[x,y].passable = false;
                }
                else{
                    tile = GroundTiles[Random.Range(0,GroundTiles.Length)];
                    m_BoardData[x,y].passable = true;
                    m_EmptyCellsList.Add(new Vector2Int(x,y));
                }
                m_Tilemap.SetTile(new Vector3Int(x,y,0),tile);
            }
        }
        m_EmptyCellsList.Remove(new Vector2Int(1,1));

        Vector2Int endCoord = new Vector2Int(Width-2,Height-2);
        AddObject(Instantiate(ExitCellPrefab),endCoord);
        m_EmptyCellsList.Remove(endCoord);
        GenerateWall();
        GenerateFood();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
