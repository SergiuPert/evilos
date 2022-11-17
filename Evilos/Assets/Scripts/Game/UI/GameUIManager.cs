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

    [SerializeField] private TextMeshProUGUI firstWeaponAmmo;
    [SerializeField] private TextMeshProUGUI secondWeaponAmmo;
    [SerializeField] private List<GameObject> mainWeaponSprites;
    [SerializeField] private List<GameObject> firstWeaponSprites;
    [SerializeField] private List<GameObject> secondWeaponSprites;
    
    [SerializeField] private TextMeshProUGUI firstSpellScrolls;
    [SerializeField] private TextMeshProUGUI secondSpellScrolls;
    [SerializeField] private TextMeshProUGUI thirdSpellScrolls;
    [SerializeField] private TextMeshProUGUI fourthSpellScrolls;
    [SerializeField] private List<GameObject> firstSpellSprites;
    [SerializeField] private List<GameObject> secondSpellSprites;
    [SerializeField] private List<GameObject> thirdSpellSprites;
    [SerializeField] private List<GameObject> fourthSpellSprites;

    [SerializeField] private List<GameObject> spells;

    [SerializeField] private GameObject firstWeaponSprite;
    [SerializeField] private GameObject secondWeaponSprite;
    [SerializeField] private GameObject firstSpellSprite;
    [SerializeField] private GameObject secondSpellSprite;
    [SerializeField] private GameObject thirdSpellSprite;
    [SerializeField] private GameObject fourthSpellSprite;


    private int weaponSelected = 0;
    public int spellSelected = 0;
    public GameObject spell;
    private int goldEarned = 0;
    public float mana = 100;

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
        GameManager.sceneChange += LoadScene;
        if (dialoguesForLevel[GameManager.Instance.levelIndex].transform.childCount <= 0)
        {
            Invoke("GameStart", 0.1f);
        }
        InitiateWeaponSprites();
        InitiateSpellSprites();
        GameObject dialogues = Instantiate(dialoguesForLevel[GameManager.Instance.levelIndex]);
        dialogues.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
        InitiateAmmo(GameManager.Instance.userSave.FirstSelectedGun, firstWeaponAmmo);
        InitiateAmmo(GameManager.Instance.userSave.SecondSelectedGun, secondWeaponAmmo);
        InitiateScrolls(GameManager.Instance.userSave.FirstSelectedSpell, firstSpellScrolls);
        InitiateScrolls(GameManager.Instance.userSave.SecondSelectedSpell, secondSpellScrolls);
        InitiateScrolls(GameManager.Instance.userSave.ThirdSelectedSpell, thirdSpellScrolls);
        InitiateScrolls(GameManager.Instance.userSave.FourthSelectedSpell, fourthSpellScrolls);
    }

    private void InitiateWeaponSprites()
    {
        mainWeaponSprites[GameManager.Instance.userSave.MainWeaponUpgrade/6].gameObject.SetActive(true);
        GameObject firstWeapon = firstWeaponSprites.Where(sprite => sprite.gameObject.name == GameManager.Instance.userSave.FirstSelectedGun).FirstOrDefault();
        GameObject secondWeapon = secondWeaponSprites.Where(sprite => sprite.gameObject.name == GameManager.Instance.userSave.SecondSelectedGun).FirstOrDefault();
        SetSprite(firstWeapon, firstWeaponSprite);
        SetSprite(secondWeapon, secondWeaponSprite);
    }
    private void InitiateSpellSprites()
    {
        GameObject firstSpell = firstSpellSprites.Where(sprite => sprite.gameObject.name == GameManager.Instance.userSave.FirstSelectedSpell).FirstOrDefault();
        GameObject secondSpell = secondSpellSprites.Where(sprite => sprite.gameObject.name == GameManager.Instance.userSave.SecondSelectedSpell).FirstOrDefault();
        GameObject thirdSpell = thirdSpellSprites.Where(sprite => sprite.gameObject.name == GameManager.Instance.userSave.ThirdSelectedSpell).FirstOrDefault();
        GameObject fourthSpell = fourthSpellSprites.Where(sprite => sprite.gameObject.name == GameManager.Instance.userSave.FourthSelectedSpell).FirstOrDefault();
        SetSprite(firstSpell, firstSpellSprite);
        SetSprite(secondSpell, secondSpellSprite);
        SetSprite(thirdSpell, thirdSpellSprite);
        SetSprite(fourthSpell, fourthSpellSprite);
    }
    private void SetSprite(GameObject sprite, GameObject spriteContainer)
    {
        if (sprite != null)
        {
            sprite.SetActive(true);
        }
        else
        {
            spriteContainer.SetActive(false);
        }
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

    public void UpdateSpellSelected(int spellIndex)
    {
        spellSelected = spellIndex;
        if (spellIndex == 1)
        {
            spell = spells.Where(s => s.name == GameManager.Instance.userSave.FirstSelectedSpell).FirstOrDefault();
        }
        else if (spellIndex == 2)
        {
            spell = spells.Where(s => s.name == GameManager.Instance.userSave.SecondSelectedSpell).FirstOrDefault();
        }
        else if (spellIndex == 3)
        {
            spell = spells.Where(s => s.name == GameManager.Instance.userSave.ThirdSelectedSpell).FirstOrDefault();
        }
        else if (spellIndex == 4)
        {
            spell = spells.Where(s => s.name == GameManager.Instance.userSave.FourthSelectedSpell).FirstOrDefault();
        }
    }

    private void InitiateAmmo(string ammoType, TextMeshProUGUI text)
    {
        if (ammoType != null)
        {
            text.text = GameManager.Instance.extendedUserSave.Ammos[ammoType].ToString();
        }
    }
    private void InitiateScrolls(string scrollType, TextMeshProUGUI text)
    {
        if (scrollType != null)
        {
            text.text = GameManager.Instance.extendedUserSave.Scrolls[scrollType].ToString();
        }
    }

    public void UpdateAmmo()
    {
        if(weaponSelected == 1)
        {
            firstWeaponAmmo.text = (int.Parse(firstWeaponAmmo.text) - 1).ToString();
        }
        else if(weaponSelected == 2)
        {
            secondWeaponAmmo.text = (int.Parse(secondWeaponAmmo.text) - 1).ToString();
        }
    }

    public void UpdateScrolls()
    {
        if (spellSelected == 1)
        {
            firstSpellScrolls.text = (int.Parse(firstSpellScrolls.text) - 1).ToString();
        }
        else if (spellSelected == 2)
        {
            secondSpellScrolls.text = (int.Parse(secondSpellScrolls.text) - 1).ToString();
        }
        else if (spellSelected == 3)
        {
            thirdSpellScrolls.text = (int.Parse(thirdSpellScrolls.text) - 1).ToString();
        }
        else if (spellSelected == 4)
        {
            fourthSpellScrolls.text = (int.Parse(fourthSpellScrolls.text) - 1).ToString();
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
