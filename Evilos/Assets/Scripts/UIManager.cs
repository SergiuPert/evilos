using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Dialogues;
    int dialogueIndex = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextDialogue()
    {
        Dialogues[dialogueIndex].gameObject.SetActive(false);
        dialogueIndex++;
        if (dialogueIndex < Dialogues.Length)
        {
            Dialogues[dialogueIndex].gameObject.SetActive(true);
        }
        else
        {
            GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.gameRunning = true;
            }
            else
            {
                Debug.Log("Game manager missing");
            }
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
