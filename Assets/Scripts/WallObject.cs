using UnityEngine;
using UnityEngine.Tilemaps;

public class WallObject : CellObject
{
    public Tile ObstacleTile;

    public override bool PlayerWantsToEnter()
    {
        return false;
    }

    public override void Init(Vector2Int cell)
    {
        base.Init(cell);
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
