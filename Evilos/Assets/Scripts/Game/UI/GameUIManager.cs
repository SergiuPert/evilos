using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] private GameObject[] maps; // or tilemaps
    [SerializeField] private GameObject[] dialoguesForLevel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider manaBar;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private TextMeshProUGUI FirstWeaponAmmo;
    [SerializeField] private TextMeshProUGUI SecondWeaponAmmo;
    [SerializeField] private List<GameObject> mainWeaponSprites;
    [SerializeField] private List<GameObject> firstWeaponSprites;
    [SerializeField] private List<GameObject> secondWeaponSprites;

    private int weaponSelected = 0;
    private int goldEarned = 0;
    public float mana = 100;

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
            Invoke("GameStart", 0.1f);
        }
        InitiateWeaponSprites();
        GameObject dialogues = Instantiate(dialoguesForLevel[GameManager.Instance.levelIndex]);
        dialogues.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
        InitiateAmmo(GameManager.Instance.userSave.FirstSelectedGun, FirstWeaponAmmo);
        InitiateAmmo(GameManager.Instance.userSave.SecondSelectedGun, SecondWeaponAmmo);
    }

    private void InitiateWeaponSprites()
    {
        mainWeaponSprites[GameManager.Instance.userSave.MainWeaponUpgrade/6].gameObject.SetActive(true);
        firstWeaponSprites.Where(sprite => sprite.gameObject.name == GameManager.Instance.userSave.FirstSelectedGun).FirstOrDefault().SetActive(true);
        secondWeaponSprites.Where(sprite => sprite.gameObject.name == GameManager.Instance.userSave.SecondSelectedGun).FirstOrDefault().SetActive(true);

    }


    private void Update()
    {
        if (mana < 100)
        {
            mana += 10 * Time.deltaTime;
            if (mana > 100)
            {
                mana = 100;
            }
            manaBar.value = mana;
        }
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

    public void UpdateWeaponSelected(int weaponIndex)
    {
        weaponSelected = weaponIndex;
    }

    private void InitiateAmmo(string ammoType, TextMeshProUGUI text)
    {
        switch (ammoType)
        {
            case "Fireblaster":
                text.text = GameManager.Instance.userSave.FireblasterAmmo.ToString();
                break;
            case "Frost Shard":
                text.text = GameManager.Instance.userSave.FrostShardAmmo.ToString();
                break;
            default:
                return;
        }
    }

    public void UpdateAmmo()
    {
        if(weaponSelected == 1)
        {
            FirstWeaponAmmo.text = (int.Parse(FirstWeaponAmmo.text) - 1).ToString();
        }
        else if(weaponSelected == 2)
        {
            SecondWeaponAmmo.text = (int.Parse(SecondWeaponAmmo.text) - 1).ToString();
        }
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

    public void LoadScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        //loadingScreen.SetActive(true); //// might not need
        AsyncOperation operation = SceneManager.LoadSceneAsync(0);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progressValue;
            loadingText.text = "Loading... " + (int)(progressValue * 100) + "%";
            yield return null;
        }
    }
    //to reset the events
    private void OnDestroy()
    {
        startGame = null;
        stopGame = null;
    }
}
