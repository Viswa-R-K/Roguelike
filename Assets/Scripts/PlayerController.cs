using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private BoardManager m_Board;
    private Vector2Int m_CellPosition;
    private bool m_IsGameOver;

    public void Spawn(BoardManager boardManager, Vector2Int cell){
        m_Board = boardManager;
        m_CellPosition = cell;

        transform.position = m_Board.CellToWorld(cell);
    }
    public void Init(){
        m_IsGameOver = false;
    }

    public void GameOver(){
        m_IsGameOver = true;
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
        if(m_IsGameOver){
            if(Keyboard.current.enterKey.wasPressedThisFrame){
                GameManager.Instance.StartNewGame();
            }
            return;
        }
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
                if(cellData.containedObject == null){
                    MoveTo(newCellTarget);
                }
                else if(cellData.containedObject.PlayerWantsToEnter()){
                    MoveTo(newCellTarget);
                    cellData.containedObject.PlayerEntered();
                }
            } 
        }
    }
}
