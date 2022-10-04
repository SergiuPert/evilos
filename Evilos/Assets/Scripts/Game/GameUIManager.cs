using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    //public delegate void StartGame();
    //public static event StartGame startGame;

    [SerializeField]
    private GameObject[] maps;
    [SerializeField]
    private GameObject[] dialoguesForLevel;
    private GameObject dialogues;
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject losePanel;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (dialoguesForLevel[GameManager.Instance.levelIndex].transform.childCount <= 0)
        {
            //startGame += GameStart;
            //if (startGame != null)
            //{
                GameStart();
                return;
            //}
        }
        dialogues = Instantiate(dialoguesForLevel[GameManager.Instance.levelIndex]);
        dialogues.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
    }

    private void Update()
    {
        CheckForWin();
    }

    public void Lose()
    {
        GameManager.Instance.gameRunning = false;
        losePanel.SetActive(true);
    }

    private void CheckForWin()
    {
        if (GameManager.Instance.gameRunning)
        {
            GameObject lastEnemy = GameObject.FindGameObjectWithTag("Enemy");
            if (lastEnemy == null)
            {
                GameManager.Instance.gameRunning = false;
                winPanel.SetActive(true);
            }
        }
    }

    public void GameStart()
    {
        GameManager.Instance.gameRunning = true;
        //startGame();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
