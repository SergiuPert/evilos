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

    private void Start()
    {
        MenuUIManager.updateCostColors += UpdateCostColors;
        UpdateShopItems();
        UpdateSelectedSpells();
    }

    private void UpdateShopItems()
    {
        UpdateElectricShockInfo();
        UpdateCursedRingInfo();
        UpdateDragonRoarInfo();
    }

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

    private void UpdateSelectedSpells()
    {
        UpdateEquipedSpellSprite(firstSpellSlotSprites, GameManager.Instance.userSave.FirstSelectedSpell);
        UpdateEquipedSpellSprite(secondSpellSlotSprites, GameManager.Instance.userSave.SecondSelectedSpell);
        UpdateEquipedSpellSprite(thirdSpellSlotSprites, GameManager.Instance.userSave.ThirdSelectedSpell);
        UpdateEquipedSpellSprite(fourthSpellSlotSprites, GameManager.Instance.userSave.FourthSelectedSpell);
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

    private void UpdateScrolls(TextMeshProUGUI ammo, int gameManagerAmmo)
    {
        ammo.text = gameManagerAmmo.ToString();
    }

    private bool BuySpellUpgrade(int[] costs, int gameManagerUpgradeLevel, Slider weaponUpgradeLevel)
    {
        int price = costs[gameManagerUpgradeLevel];
        if (price > GameManager.Instance.userSave.Gold)
        {
            Debug.Log("not enought gold");
            return false;
        }
        GameManager.Instance.userSave.Gold -= price;
        weaponUpgradeLevel.value = gameManagerUpgradeLevel;
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

    private void UpdateEquipedSpellSprite(List<GameObject> weaponSlot, string spriteName)
    {
        if (spriteName != null)
        {
            weaponSlot.Where(sprite => sprite.gameObject.name == spriteName).FirstOrDefault().SetActive(true);
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
