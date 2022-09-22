using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameRunning = false;
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject losePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning)
        {
            CheckForWin();
        }
    }

    void CheckForWin()
    {
        GameObject lastEnemy = GameObject.FindGameObjectWithTag("Enemy");
        if (lastEnemy == null)
        {
            gameRunning = false;
            winPanel.SetActive(true);
        }
    }

    public void Lose()
    {
        gameRunning = false;
        losePanel.SetActive(true);
    }


}
