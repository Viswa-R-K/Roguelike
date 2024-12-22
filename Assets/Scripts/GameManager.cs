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

    public void IncreaseFood(int Amount){
        m_FoodAmount += Amount;
        m_FoodLabel.text = "Energy : " + m_FoodAmount;
    }

    void OnTurnHappen(){
        IncreaseFood(-1);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_FoodLabel = uIDocument.rootVisualElement.Q<Label>("FoodLabel");
        m_FoodLabel.text = "Energy : " + m_FoodAmount;
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
