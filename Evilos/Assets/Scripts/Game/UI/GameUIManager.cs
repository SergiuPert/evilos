using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private Slider healthBar;

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
        GameManager.sceneChange += LoadScene;
        if (dialoguesForLevel[GameManager.Instance.levelIndex].transform.childCount <= 0)
        {
            //startGame += GameStart;
            Invoke("GameStart", 0.1f);
            //if (startGame != null)
            //{
            //    Debug.Log("got here");
            //    GameStart();
            //    return;
            //}
        }
        GameObject dialogues = Instantiate(dialoguesForLevel[GameManager.Instance.levelIndex]);
        dialogues.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
    }

    private void Update()
    {
        //CheckForWin(); //could call this from the enemies when they die
    }

    public void UpdateGold(int goldValue)
    {
        goldEarned += goldValue;
        GameManager.Instance.userSave.Gold += goldValue;
        goldText.text = goldEarned.ToString();
    }

    public void UpdateHealth(float health, float maxHealth)
    {
        healthBar.value = health/maxHealth;
    }

    public void Lose()
    {
        GameManager.Instance.gameRunning = false;
        GameStop();
        losePanel.SetActive(true);
    }

    public void CheckForWin()
    {
        if (GameManager.Instance.gameRunning)
        {
            GameObject lastEnemy = GameObject.FindGameObjectWithTag("Enemy");
            if (lastEnemy == null)
            {
                GameStop();
                winPanel.SetActive(true);
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

    public void LoadScene() // Maybe I need to create a coroutine to wait until the save is done
    {
        //GameManager.Instance.userSave.Gold += goldEarned; // Updated in the UpdateGold method instead
        SceneManager.LoadScene(0);
    }
    //to reset the events
    private void OnDestroy()
    {
        startGame = null;
        stopGame = null;
    }
}
