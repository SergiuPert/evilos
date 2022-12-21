using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSpells : MonoBehaviour
{
    [SerializeField] private List<GameObject> firstSpellSlotSprites = new List<GameObject>();
    [SerializeField] private List<GameObject> secondSpellSlotSprites = new List<GameObject>();
    [SerializeField] private List<GameObject> thirdSpellSlotSprites = new List<GameObject>();
    [SerializeField] private List<GameObject> fourthSpellSlotSprites = new List<GameObject>();


    // TODO: Try sending the GameManager as reference (Use "ref")

    #region Electric Shock

    [Header("Electric Shock")]

    [SerializeField] private Slider electricShockUpgradeLevel;
    [SerializeField] private TextMeshProUGUI electricShockUpgradeCost;
    [SerializeField] private TextMeshProUGUI electricShockUpgradeText;
    [SerializeField] private TextMeshProUGUI electricShockScrollsCost;
    [SerializeField] private Button electricShockUpgradeButton;
    [SerializeField] private Button electricShockBuyScrollsButton;
    [SerializeField] private Button electricShockEquipButton;
    [SerializeField] private TextMeshProUGUI electricShockScrolls;

    private int[] electricShockUpgradeCosts = { 1000, 2000, 5000, 10000, 20000, 35000, 0 };
    private int electricShockScrollsCostVariable = 300;
    private void UpdateElectricShockInfo()
    {
        UpdateSpellInfo(GameManager.Instance.userSave.ElectricShockUpgrade, electricShockUpgradeCost,
            electricShockUpgradeCosts, electricShockUpgradeText, electricShockUpgradeButton);
        UpdateScrolls(electricShockScrolls, GameManager.Instance.userSave.ElectricShockScrolls);
        electricShockScrollsCost.text = electricShockScrollsCostVariable.ToString();
        electricShockUpgradeLevel.value = GameManager.Instance.userSave.ElectricShockUpgrade - 1;
        electricShockBuyScrollsButton.interactable = GameManager.Instance.userSave.ElectricShockUpgrade != 0;
        electricShockEquipButton.interactable = GameManager.Instance.userSave.ElectricShockUpgrade != 0;
    }
    public void BuyElectricShockUpgrade()
    {
        bool hasPurchased = BuySpellUpgrade(electricShockUpgradeCosts, GameManager.Instance.userSave.ElectricShockUpgrade, electricShockUpgradeLevel);
        if (hasPurchased)
        {
            GameManager.Instance.userSave.ElectricShockUpgrade++;
            UpdateSpellInfo(GameManager.Instance.userSave.ElectricShockUpgrade, electricShockUpgradeCost,
            electricShockUpgradeCosts, electricShockUpgradeText, electricShockUpgradeButton);
            if (!electricShockEquipButton.interactable)
            {
                electricShockEquipButton.interactable = true;
                electricShockBuyScrollsButton.interactable = true;
            }
        }
    }
    public void BuyElectricShockScroll()
    {
        bool hasPurchased = BuyScroll(electricShockScrollsCostVariable);
        if (hasPurchased)
        {
            GameManager.Instance.extendedUserSave.Scrolls["ElectricShockScrolls"]++;
            GameManager.Instance.extendedUserSave.SaveScrollsData();
            electricShockScrolls.text = GameManager.Instance.userSave.ElectricShockScrolls.ToString();
        }
    }
    #endregion    
    
    #region Frost Nova
    
    [Header("Frost Nova")]

    [SerializeField] private Slider frostNovaUpgradeLevel;
    [SerializeField] private TextMeshProUGUI frostNovaUpgradeCost;
    [SerializeField] private TextMeshProUGUI frostNovaUpgradeText;
    [SerializeField] private TextMeshProUGUI frostNovaScrollsCost;
    [SerializeField] private Button frostNovaUpgradeButton;
    [SerializeField] private Button frostNovaBuyScrollsButton;
    [SerializeField] private Button frostNovaEquipButton;
    [SerializeField] private TextMeshProUGUI frostNovaScrolls;

    private int[] frostNovaUpgradeCosts = { 1500, 3000, 8000, 12000, 25000, 40000, 0 };
    private int frostNovaScrollsCostVariable = 400;


    private void UpdateFrostNovaInfo()
    {
        UpdateSpellInfo(GameManager.Instance.userSave.FrostNovaUpgrade, frostNovaUpgradeCost,
            frostNovaUpgradeCosts, frostNovaUpgradeText, frostNovaUpgradeButton);
        UpdateScrolls(frostNovaScrolls, GameManager.Instance.userSave.FrostNovaScrolls);
        frostNovaScrollsCost.text = frostNovaScrollsCostVariable.ToString();
        frostNovaUpgradeLevel.value = GameManager.Instance.userSave.FrostNovaUpgrade - 1;
        frostNovaBuyScrollsButton.interactable = GameManager.Instance.userSave.FrostNovaUpgrade != 0;
        frostNovaEquipButton.interactable = GameManager.Instance.userSave.FrostNovaUpgrade != 0;
    }
    public void BuyFrostNovaUpgrade()
    {
        bool hasPurchased = BuySpellUpgrade(frostNovaUpgradeCosts, GameManager.Instance.userSave.FrostNovaUpgrade, frostNovaUpgradeLevel);
        if (hasPurchased)
        {
            GameManager.Instance.userSave.FrostNovaUpgrade++;
            UpdateSpellInfo(GameManager.Instance.userSave.FrostNovaUpgrade, frostNovaUpgradeCost,
            frostNovaUpgradeCosts, frostNovaUpgradeText, frostNovaUpgradeButton);
            if (!frostNovaEquipButton.interactable)
            {
                frostNovaEquipButton.interactable = true;
                frostNovaBuyScrollsButton.interactable = true;
            }
        }
    }
    public void BuyFrostNovaScroll()
    {
        bool hasPurchased = BuyScroll(frostNovaScrollsCostVariable);
        if (hasPurchased)
        {
            GameManager.Instance.extendedUserSave.Scrolls["FrostNovaScrolls"]++;
            GameManager.Instance.extendedUserSave.SaveScrollsData();
            frostNovaScrolls.text = GameManager.Instance.userSave.FrostNovaScrolls.ToString();
        }
    }
    #endregion

    #region Chains Of Torment

    [Header("Chains Of Torment")]

    [SerializeField] private Slider chainsOfTormentUpgradeLevel;
    [SerializeField] private TextMeshProUGUI chainsOfTormentUpgradeCost;
    [SerializeField] private TextMeshProUGUI chainsOfTormentUpgradeText;
    [SerializeField] private TextMeshProUGUI chainsOfTormentScrollsCost;
    [SerializeField] private Button chainsOfTormentUpgradeButton;
    [SerializeField] private Button chainsOfTormentBuyScrollsButton;
    [SerializeField] private Button chainsOfTormentEquipButton;
    [SerializeField] private TextMeshProUGUI chainsOfTormentScrolls;

    private int[] chainsOfTormentUpgradeCosts = { 1500, 3000, 7000, 11000, 25000, 40000, 0 };
    private int chainsOfTormentScrollsCostVariable = 500;
    private void UpdateChainsOfTormentInfo()
    {
        UpdateSpellInfo(GameManager.Instance.userSave.ChainsOfTormentUpgrade, chainsOfTormentUpgradeCost,
            chainsOfTormentUpgradeCosts, chainsOfTormentUpgradeText, chainsOfTormentUpgradeButton);
        UpdateScrolls(chainsOfTormentScrolls, GameManager.Instance.userSave.ChainsOfTormentScrolls);
        chainsOfTormentScrollsCost.text = chainsOfTormentScrollsCostVariable.ToString();
        chainsOfTormentUpgradeLevel.value = GameManager.Instance.userSave.ChainsOfTormentUpgrade - 1;
        chainsOfTormentBuyScrollsButton.interactable = GameManager.Instance.userSave.ChainsOfTormentUpgrade != 0;
        chainsOfTormentEquipButton.interactable = GameManager.Instance.userSave.ChainsOfTormentUpgrade != 0;
    }
    public void BuyChainsOfTormentUpgrade()
    {
        bool hasPurchased = BuySpellUpgrade(chainsOfTormentUpgradeCosts, GameManager.Instance.userSave.ChainsOfTormentUpgrade, chainsOfTormentUpgradeLevel);
        if (hasPurchased)
        {
            GameManager.Instance.userSave.ChainsOfTormentUpgrade++;
            UpdateSpellInfo(GameManager.Instance.userSave.ChainsOfTormentUpgrade, chainsOfTormentUpgradeCost,
            chainsOfTormentUpgradeCosts, chainsOfTormentUpgradeText, chainsOfTormentUpgradeButton);
            if (!chainsOfTormentEquipButton.interactable)
            {
                chainsOfTormentEquipButton.interactable = true;
                chainsOfTormentBuyScrollsButton.interactable = true;
            }
        }
    }
    public void BuyChainsOfTormentScroll()
    {
        bool hasPurchased = BuyScroll(chainsOfTormentScrollsCostVariable);
        if (hasPurchased)
        {
            GameManager.Instance.extendedUserSave.Scrolls["ChainsOfTormentScrolls"]++;
            GameManager.Instance.extendedUserSave.SaveScrollsData();
            chainsOfTormentScrolls.text = GameManager.Instance.userSave.ChainsOfTormentScrolls.ToString();
        }
    }
    #endregion

    #region Cursed Ring

    [Header("Cursed Ring")]

    [SerializeField] private Slider cursedRingUpgradeLevel;
    [SerializeField] private TextMeshProUGUI cursedRingUpgradeCost;
    [SerializeField] private TextMeshProUGUI cursedRingUpgradeText;
    [SerializeField] private TextMeshProUGUI cursedRingScrollsCost;
    [SerializeField] private Button cursedRingUpgradeButton;
    [SerializeField] private Button cursedRingBuyScrollsButton;
    [SerializeField] private Button cursedRingEquipButton;
    [SerializeField] private TextMeshProUGUI cursedRingScrolls;

    private int[] cursedRingUpgradeCosts = { 5000, 10000, 18000, 30000, 50000, 75000, 0 };
    private int cursedRingScrollsCostVariable = 600;
    private void UpdateCursedRingInfo()
    {
        UpdateSpellInfo(GameManager.Instance.userSave.CursedRingUpgrade, cursedRingUpgradeCost,
            cursedRingUpgradeCosts, cursedRingUpgradeText, cursedRingUpgradeButton);
        UpdateScrolls(cursedRingScrolls, GameManager.Instance.userSave.CursedRingScrolls);
        cursedRingScrollsCost.text = cursedRingScrollsCostVariable.ToString();
        cursedRingUpgradeLevel.value = GameManager.Instance.userSave.CursedRingUpgrade - 1;
        cursedRingBuyScrollsButton.interactable = GameManager.Instance.userSave.CursedRingUpgrade != 0;
        cursedRingEquipButton.interactable = GameManager.Instance.userSave.CursedRingUpgrade != 0;
    }
    public void BuyCursedRingUpgrade()
    {
        bool hasPurchased = BuySpellUpgrade(cursedRingUpgradeCosts, GameManager.Instance.userSave.CursedRingUpgrade, cursedRingUpgradeLevel);
        if (hasPurchased)
        {
            GameManager.Instance.userSave.CursedRingUpgrade++;
            UpdateSpellInfo(GameManager.Instance.userSave.CursedRingUpgrade, cursedRingUpgradeCost,
            cursedRingUpgradeCosts, cursedRingUpgradeText, cursedRingUpgradeButton);
            if (!cursedRingEquipButton.interactable)
            {
                cursedRingEquipButton.interactable = true;
                cursedRingBuyScrollsButton.interactable = true;
            }
        }
    }
    public void BuyCursedRingScroll()
    {
        bool hasPurchased = BuyScroll(cursedRingScrollsCostVariable);
        if (hasPurchased)
        {
            GameManager.Instance.extendedUserSave.Scrolls["CursedRingScrolls"]++;
            GameManager.Instance.extendedUserSave.SaveScrollsData();
            cursedRingScrolls.text = GameManager.Instance.userSave.CursedRingScrolls.ToString();
        }
    }
    #endregion

    #region Dragon Roar

    [Header("Dragon Roar")]

    [SerializeField] private Slider dragonRoarUpgradeLevel;
    [SerializeField] private TextMeshProUGUI dragonRoarUpgradeCost;
    [SerializeField] private TextMeshProUGUI dragonRoarUpgradeText;
    [SerializeField] private TextMeshProUGUI dragonRoarScrollsCost;
    [SerializeField] private Button dragonRoarUpgradeButton;
    [SerializeField] private Button dragonRoarBuyScrollsButton;
    [SerializeField] private Button dragonRoarEquipButton;
    [SerializeField] private TextMeshProUGUI dragonRoarScrolls;

    private int[] dragonRoarUpgradeCosts = { 30000, 50000, 80000, 100000, 150000, 300000, 0 };
    private int dragonRoarScrollsCostVariable = 1000;
    private void UpdateDragonRoarInfo()
    {
        UpdateSpellInfo(GameManager.Instance.userSave.DragonRoarUpgrade, dragonRoarUpgradeCost,
            dragonRoarUpgradeCosts, dragonRoarUpgradeText, dragonRoarUpgradeButton);
        UpdateScrolls(dragonRoarScrolls, GameManager.Instance.userSave.DragonRoarScrolls);
        dragonRoarScrollsCost.text = dragonRoarScrollsCostVariable.ToString();
        dragonRoarUpgradeLevel.value = GameManager.Instance.userSave.DragonRoarUpgrade - 1;
        dragonRoarBuyScrollsButton.interactable = GameManager.Instance.userSave.DragonRoarUpgrade != 0;
        dragonRoarEquipButton.interactable = GameManager.Instance.userSave.DragonRoarUpgrade != 0;
    }
    public void BuyDragonRoarUpgrade()
    {
        bool hasPurchased = BuySpellUpgrade(dragonRoarUpgradeCosts, GameManager.Instance.userSave.DragonRoarUpgrade, dragonRoarUpgradeLevel);
        if (hasPurchased)
        {
            GameManager.Instance.userSave.DragonRoarUpgrade++;
            UpdateSpellInfo(GameManager.Instance.userSave.DragonRoarUpgrade, dragonRoarUpgradeCost,
            dragonRoarUpgradeCosts, dragonRoarUpgradeText, dragonRoarUpgradeButton);
            if (!dragonRoarEquipButton.interactable)
            {
                dragonRoarEquipButton.interactable = true;
                dragonRoarBuyScrollsButton.interactable = true;
            }
        }
    }
    public void BuyDragonRoarScroll()
    {
        bool hasPurchased = BuyScroll(dragonRoarScrollsCostVariable);
        if (hasPurchased)
        {
            GameManager.Instance.extendedUserSave.Scrolls["DragonRoarScrolls"]++;
            GameManager.Instance.extendedUserSave.SaveScrollsData();
            dragonRoarScrolls.text = GameManager.Instance.userSave.DragonRoarScrolls.ToString();
        }
    }
    #endregion

    private void Start()
    {
        MenuUIManager.updateCostColors += UpdateCostColors;
        UpdateShopItems();
        UpdateSelectedSpells();
    }

    private void UpdateShopItems()
    {
        UpdateElectricShockInfo();
        UpdateChainsOfTormentInfo();
        UpdateCursedRingInfo();
        UpdateFrostNovaInfo();
        UpdateDragonRoarInfo();
    }

    private void UpdateCostColors()
    {
        int gold = GameManager.Instance.userSave.Gold;
        if (electricShockUpgradeCosts[GameManager.Instance.userSave.ElectricShockUpgrade] > gold)
        {
            electricShockUpgradeCost.color = Color.red;
        }
        if (electricShockScrollsCostVariable > gold)
        {
            electricShockScrollsCost.color = Color.red;
        }
        if (frostNovaUpgradeCosts[GameManager.Instance.userSave.FrostNovaUpgrade] > gold)
        {
            frostNovaUpgradeCost.color = Color.red;
        }
        if (frostNovaScrollsCostVariable > gold)
        {
            frostNovaScrollsCost.color = Color.red;
        }
        if (chainsOfTormentUpgradeCosts[GameManager.Instance.userSave.ChainsOfTormentUpgrade] > gold)
        {
            chainsOfTormentUpgradeCost.color = Color.red;
        }
        if (chainsOfTormentScrollsCostVariable > gold)
        {
            chainsOfTormentScrollsCost.color = Color.red;
        }
        if (cursedRingUpgradeCosts[GameManager.Instance.userSave.CursedRingUpgrade] > gold)
        {
            cursedRingUpgradeCost.color = Color.red;
        }
        if (cursedRingScrollsCostVariable > gold)
        {
            cursedRingScrollsCost.color = Color.red;
        }
        if (dragonRoarUpgradeCosts[GameManager.Instance.userSave.DragonRoarUpgrade] > gold)
        {
            dragonRoarUpgradeCost.color = Color.red;
        }
        if (dragonRoarScrollsCostVariable > gold)
        {
            dragonRoarScrollsCost.color = Color.red;
        }
    }

    private void UpdateSelectedSpells()
    {
        UpdateEquipedSpellSprite(firstSpellSlotSprites, GameManager.Instance.userSave.FirstSelectedSpell);
        UpdateEquipedSpellSprite(secondSpellSlotSprites, GameManager.Instance.userSave.SecondSelectedSpell);
        UpdateEquipedSpellSprite(thirdSpellSlotSprites, GameManager.Instance.userSave.ThirdSelectedSpell);
        UpdateEquipedSpellSprite(fourthSpellSlotSprites, GameManager.Instance.userSave.FourthSelectedSpell);
    }

    private void UpdateSpellInfo(int gameManagerUpgradeLevel, TextMeshProUGUI upgradeCost, int[] upgradeCosts, TextMeshProUGUI upgradeText, Button upgradeButton)
    {
        upgradeCost.text = upgradeCosts[gameManagerUpgradeLevel].ToString();
        if (gameManagerUpgradeLevel == 0)
        {
            upgradeText.text = "Buy";
        }
        else if (gameManagerUpgradeLevel == 6)
        {
            upgradeText.text = "Maxed";
            upgradeCost.gameObject.SetActive(false);
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeText.text = "Upgrade";
        }
        MenuUIManager.Instance.UpdateCostColors();
    }

    private void UpdateScrolls(TextMeshProUGUI scrolls, int gameManagerScrolls)
    {
        scrolls.text = gameManagerScrolls.ToString();
    }

    private bool BuySpellUpgrade(int[] costs, int gameManagerUpgradeLevel, Slider spellUpgradeLevel)
    {
        int price = costs[gameManagerUpgradeLevel];
        if (price > GameManager.Instance.userSave.Gold)
        {
            Debug.Log("not enought gold");
            return false;
        }
        GameManager.Instance.userSave.Gold -= price;
        spellUpgradeLevel.value = gameManagerUpgradeLevel;
        MenuUIManager.Instance.UpdateCostColors();
        return true;
    }

    private bool BuyScroll(int cost)
    {
        if (GameManager.Instance.userSave.Gold >= cost)
        {
            GameManager.Instance.userSave.Gold -= cost;
            MenuUIManager.Instance.UpdateCostColors();
            return true;
        }
        return false;
    }

    public void EquipSpell(string spellName)
    {
        if (GameManager.Instance.userSave.FirstSelectedSpell == null && !IsSpellEquipped(spellName))
        {
            GameManager.Instance.userSave.FirstSelectedSpell = spellName;
            UpdateEquipedSpellSprite(firstSpellSlotSprites, spellName);
        }
        else if (GameManager.Instance.userSave.SecondSelectedSpell == null && !IsSpellEquipped(spellName))
        {
            GameManager.Instance.userSave.SecondSelectedSpell = spellName;
            UpdateEquipedSpellSprite(secondSpellSlotSprites, spellName);
        }
        else if (GameManager.Instance.userSave.ThirdSelectedSpell == null && !IsSpellEquipped(spellName))
        {
            GameManager.Instance.userSave.ThirdSelectedSpell = spellName;
            UpdateEquipedSpellSprite(thirdSpellSlotSprites, spellName);
        }
        else if (GameManager.Instance.userSave.FourthSelectedSpell == null && !IsSpellEquipped(spellName))
        {
            GameManager.Instance.userSave.FourthSelectedSpell = spellName;
            UpdateEquipedSpellSprite(fourthSpellSlotSprites, spellName);
        }
    }

    private bool IsSpellEquipped(string spellName)
    {
        return GameManager.Instance.userSave.FirstSelectedSpell == spellName || GameManager.Instance.userSave.SecondSelectedSpell == spellName ||
            GameManager.Instance.userSave.ThirdSelectedSpell == spellName || GameManager.Instance.userSave.FourthSelectedSpell == spellName;
    }

    private void UpdateEquipedSpellSprite(List<GameObject> spellSlot, string spriteName)
    {
        if (spriteName != null)
        {
            spellSlot.Where(sprite => sprite.gameObject.name == spriteName).FirstOrDefault().SetActive(true);
        }
    }

    public void UnequipFirstSpell()
    {
        GameManager.Instance.userSave.FirstSelectedSpell = null;
        firstSpellSlotSprites.Where(sprite => sprite.gameObject.activeInHierarchy).FirstOrDefault().SetActive(false);
    }

    public void UnequipSecondSpell()
    {
        GameManager.Instance.userSave.SecondSelectedSpell = null;
        secondSpellSlotSprites.Where(sprite => sprite.gameObject.activeInHierarchy).FirstOrDefault().SetActive(false);
    }

    public void UnequipThirdSpell()
    {
        GameManager.Instance.userSave.ThirdSelectedSpell = null;
        thirdSpellSlotSprites.Where(sprite => sprite.gameObject.activeInHierarchy).FirstOrDefault().SetActive(false);
    }

    public void UnequipFourthSpell()
    {
        GameManager.Instance.userSave.FourthSelectedSpell = null;
        fourthSpellSlotSprites.Where(sprite => sprite.gameObject.activeInHierarchy).FirstOrDefault().SetActive(false);
    }
}
