using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    public delegate void StartGame();
    public static event StartGame startGame;

    public delegate void StopGame();
    public static event StopGame stopGame;

    [SerializeField]
    private GameObject[] maps; // or tilemaps
    [SerializeField]
    private GameObject[] dialoguesForLevel;
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject losePanel;
    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private TextMeshProUGUI healthText;

    private int goldEarned = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("destroyed");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (dialoguesForLevel[GameManager.Instance.levelIndex].transform.childCount <= 0)
        {
            startGame += GameStart;
            if (startGame != null)
            {
                GameStart();
                return;
            }
        }
        GameObject dialogues = Instantiate(dialoguesForLevel[GameManager.Instance.levelIndex]);
        dialogues.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
    }

    private void Update()
    {
        CheckForWin();
    }

    public void UpdateGold(int goldValue)
    {
        goldEarned += goldValue;
        goldText.text = "Gold: " + goldEarned;
    }

    public void UpdateHealth(int health)
    {
        healthText.text = "Health " + health;
    }

    public void Lose()
    {
        GameManager.Instance.gameRunning = false;
        GameStop();
        losePanel.SetActive(true);
    }

    private void CheckForWin()
    {
        if (GameManager.Instance.gameRunning)
        {
            GameObject lastEnemy = GameObject.FindGameObjectWithTag("Enemy");
            if (lastEnemy == null)
            {
                GameStop();
                winPanel.SetActive(true);
                GameManager.Instance.gold += goldEarned;
            }
        }
    }

    public void GameStart()
    {
        GameManager.Instance.gameRunning = true;
        startGame();
    }

    public void GameStop()
    {
        GameManager.Instance.gameRunning = false;
        stopGame();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
    //to reset the events
    private void OnDestroy()
    {
        startGame = null;
        stopGame = null;
    }
}
