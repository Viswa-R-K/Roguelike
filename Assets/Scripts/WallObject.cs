using UnityEngine;
using UnityEngine.Tilemaps;

public class WallObject : CellObject
{
    public Tile ObstacleTile;
    public Tile SemiDamagedTile;
    public Tile DamagedTile;
    public int HitPoints = 3;

    private int m_CurrentHitPoint;
    private Tile m_OriginalTile;



    public override bool PlayerWantsToEnter()
    {
        m_CurrentHitPoint -= 1;
        GameManager.Instance.IncreaseFood(-2);
        if(m_CurrentHitPoint > 0){
            GameManager.Instance.playerController.PlayerAttack();
            if(m_CurrentHitPoint == 2){
                GameManager.Instance.boardManager.SetCellTile(m_Cell,SemiDamagedTile);
            }
            else if(m_CurrentHitPoint == 1){
                GameManager.Instance.boardManager.SetCellTile(m_Cell,DamagedTile);
            }
            return false;
        }
        GameManager.Instance.boardManager.SetCellTile(m_Cell,m_OriginalTile);
        Destroy(gameObject);
        return true;
    }

    public override void Init(Vector2Int cell)
    {
        base.Init(cell);
        m_CurrentHitPoint = HitPoints;
        m_OriginalTile = GameManager.Instance.boardManager.GetCellTile(cell);
        GameManager.Instance.boardManager.SetCellTile(cell,ObstacleTile);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
