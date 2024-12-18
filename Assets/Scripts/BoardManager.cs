using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{

    public class CellData{
        public bool passable;
    }

    private CellData[,] m_BoardData;
    private Tilemap m_Tilemap;

    private Grid m_Grid;

    public PlayerController Player;

    public int Height;
    public int Width;
    public Tile[] GroundTiles; 

    public Tile[] WallTiles;

    public Vector3 CellToWorld(Vector2Int cellIndex){
        return m_Grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Tilemap = GetComponentInChildren<Tilemap>();
        m_BoardData = new CellData[Width,Height];
        m_Grid = GetComponentInChildren<Grid>();

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
                }
                m_Tilemap.SetTile(new Vector3Int(x,y,0),tile);
            }
        }
        Player.Spawn(this,new Vector2Int(1,1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
