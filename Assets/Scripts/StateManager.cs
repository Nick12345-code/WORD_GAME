using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public enum State { START, PLAYERTURN, ENEMYTURN, WIN, LOSE }

// controls the game state
public class StateManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] Enemy enemy;
    [SerializeField] WordGenerator wordGenerator;
    [SerializeField] WordUI wordUI;
    [SerializeField] ButtonGenerator buttonGenerator;
    [SerializeField] Player player;
    [Header("")]
    public State currentState;
    [SerializeField] GameObject resultsPanel;
    [SerializeField] TextMeshProUGUI resultsText;
    [SerializeField] TextMeshProUGUI pickALetter;
    [SerializeField] float delay;
    bool hasWon = false;
    public bool HasWon
    {
        get { return hasWon; }
        set
        {
            if (hasWon != value)
            {
                hasWon = value;
                Result(State.WIN, "You Won");
            }
        }
    }
    bool hasLost = false;
    public bool HasLost
    {
        get { return hasLost; }
        set
        {
            if (hasLost != value)
            {
                hasLost = value;
                Result(State.LOSE, "You Lost");
            }
        }
    }

    [SerializeField] UnityEvent<bool> onWin;
    [SerializeField] UnityEvent<bool> onLose;

    void Start() => Setup();

    void Setup()
    {
        currentState = State.START;
        wordGenerator.GenerateRandomWord();
        wordUI.GeneratePlayerLetters();
        wordUI.GenerateEnemyLetters();
        buttonGenerator.GenerateButtons();
        currentState = State.PLAYERTURN;
    }

    void Update()
    {
        if (currentState == State.ENEMYTURN) EnemyTurn();
    }

    void EnemyTurn()
    {
        enemy.ChooseRandomLetter();
        currentState = State.PLAYERTURN;
    }

    void Result(State state, string result)
    {
        currentState = state;
        resultsPanel.SetActive(true);
        resultsText.text = result;
    }
}
