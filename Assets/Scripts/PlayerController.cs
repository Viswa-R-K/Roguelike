using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private BoardManager m_Board;
    private Vector2Int m_CellPosition;

    public void Spawn(BoardManager boardManager, Vector2Int cell){
        m_Board = boardManager;
        m_CellPosition = cell;

        transform.position = m_Board.CellToWorld(cell);
    }

    public void MoveTo(Vector2Int cell){
        m_CellPosition = cell;
        transform.position = m_Board.CellToWorld(cell);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int newCellTarget = m_CellPosition;
        bool hasMoved = false;
        if(Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame){
            newCellTarget.y += 1;
            hasMoved = true;
        }
        else if(Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame){
            newCellTarget.y -= 1;
            hasMoved = true;
        } 
        else if(Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame){
            newCellTarget.x += 1;
            hasMoved = true;
        }
        else if(Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame){
            newCellTarget.x -= 1;
            hasMoved = true;
        }
        if(hasMoved){
            BoardManager.CellData cellData = m_Board.GetCellData(newCellTarget);
            if(cellData != null && cellData.passable){
                GameManager.Instance.turnManager.Tick();
                MoveTo(newCellTarget);
            } 
        }
    }
}
