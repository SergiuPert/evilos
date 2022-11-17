using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public delegate void CostColors();
    public static event CostColors updateCostColors;
    
    public static MenuUIManager Instance;

    [SerializeField]
    List<GameObject> menuPanels;
    [SerializeField]
    private TextMeshProUGUI shopGold;
    [SerializeField]
    private TextMeshProUGUI mapGold;
    [SerializeField]
    private GameObject intro;
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Slider loadingBar;
    [SerializeField]
    private TextMeshProUGUI loadingText;

    //private bool gameJustLaunched;

    //private int currentIndex = 0;

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
        if (!GameManager.Instance.isSecondaryLoad)
        {
            StartCoroutine(IntroScreen());
        }
    }

    IEnumerator IntroScreen()
    {
        intro.SetActive(true);
        yield return new WaitForSeconds(2);
        intro.SetActive(false);
    }

    private void Update() //Needs optimization
    {
        shopGold.text = GameManager.Instance.userSave.Gold.ToString();
        mapGold.text = GameManager.Instance.userSave.Gold.ToString();
    }

    public void UpdateCostColors()
    {
        updateCostColors();
    }
    public void LoadScene(int selectedLevel)
    {
        //gameJustLaunched = false;
        GameManager.Instance.isSecondaryLoad = true;
        GameManager.Instance.levelIndex = selectedLevel;
        //SceneManager.LoadScene(1);
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progressValue;
            loadingText.text = "Loading... " + (int)(progressValue * 100) + "%";
            yield return null;
        }
    }
    private void OnDestroy()
    {
        updateCostColors = null;
    }
}
