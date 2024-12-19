using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get ; private set;}
    public BoardManager boardManager;
    public PlayerController playerController;

    public TurnManager turnManager {get ; private set;}

    public UIDocument uIDocument;
    private Label m_FoodLabel;

    private int m_FoodAmount = 100; 

    public void Awake(){
        if(Instance != null){
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void OnTurnHappen(){
        m_FoodAmount -= 1;
        m_FoodLabel.text = "Food : " + m_FoodAmount;
        Debug.Log("Current food amount " + m_FoodAmount);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_FoodLabel = uIDocument.rootVisualElement.Q<Label>("FoodLabel");
        m_FoodLabel.text = "Food : " + m_FoodAmount;
        turnManager = new TurnManager();
        turnManager.OnTick += OnTurnHappen;
        boardManager.Init();
        playerController.Spawn(boardManager,new Vector2Int(1,1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
