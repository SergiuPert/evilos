using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    //might be a good singleton
    [SerializeField]
    List<GameObject> menuPanels;
    [SerializeField]
    private TextMeshProUGUI shopGold;
    [SerializeField]
    private TextMeshProUGUI mapGold;

    private int currentIndex = 0;

    private void Update() //Needs optimization
    {
        shopGold.text = GameManager.Instance.userSave.Gold.ToString();
        mapGold.text = GameManager.Instance.userSave.Gold.ToString();
    }

    public void SwitchPanel(int index)
    {
        menuPanels[currentIndex].SetActive(false);
        currentIndex = index;
        menuPanels[index].SetActive(true);
    }

    public void LoadScene(int selectedLevel)
    {
        GameManager.Instance.levelIndex = selectedLevel;
        SceneManager.LoadScene(1);
    }
}
