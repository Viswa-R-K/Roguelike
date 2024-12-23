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

    private int m_FoodAmount = 50; 
    private int m_CurrentLevel = 1;

    private VisualElement m_GameOverPanel;
    private Label m_GameOverMessage;

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

        if(m_FoodAmount <= 0){
            playerController.GameOver();
            m_GameOverPanel.style.visibility = Visibility.Visible;
            m_GameOverMessage.text = "Game Over \n\n You have completed "+ (m_CurrentLevel-1) + " Levels";
        }
    }

    void OnTurnHappen(){
        IncreaseFood(-1);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_GameOverPanel = uIDocument.rootVisualElement.Q<VisualElement>("GameOverPanel");
        m_GameOverMessage = m_GameOverPanel.Q<Label>("GameOverLabel");

        m_GameOverPanel.style.visibility = Visibility.Hidden;
        m_FoodLabel = uIDocument.rootVisualElement.Q<Label>("FoodLabel");
        m_FoodLabel.text = "Energy : " + m_FoodAmount;
        turnManager = new TurnManager();
        turnManager.OnTick += OnTurnHappen;
        boardManager.Init();
        playerController.Spawn(boardManager,new Vector2Int(1,1));
    }
    public void NewLevel(){
        boardManager.Clean();
        boardManager.Init();
        playerController.Spawn(boardManager,new Vector2Int(1,1));

        m_CurrentLevel += 1;
    }

    public void StartNewGame(){
        m_GameOverPanel.style.visibility = Visibility.Hidden;

        m_CurrentLevel = 1;
        m_FoodAmount = 50;
        m_FoodLabel.text = "Energy : " + m_FoodAmount;

        boardManager.Clean();
        boardManager.Init();

        playerController.Init();
        playerController.Spawn(boardManager,new Vector2Int(1,1));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
