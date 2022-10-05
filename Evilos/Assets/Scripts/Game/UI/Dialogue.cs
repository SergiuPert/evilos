using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private int dialogueIndex = 0;
    public void NextDialogue()
    {
        gameObject.transform.GetChild(dialogueIndex).gameObject.SetActive(false);
        dialogueIndex++;
        if (dialogueIndex < gameObject.transform.childCount)
        {
            gameObject.transform.GetChild(dialogueIndex).gameObject.SetActive(true);
        }
        else
        {
            GameUIManager.Instance.GameStart();
        }
    }
}
