using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> menuPanels;

    private int currentIndex = 0;

    public void SwitchPanel(int index)
    {
        menuPanels[currentIndex].SetActive(false);
        currentIndex = index;
        menuPanels[index].SetActive(true);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
