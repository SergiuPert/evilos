using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeapons : MonoBehaviour
{
    [SerializeField] private List<GameObject> firstWeaponSlotSprites = new List<GameObject>();
    [SerializeField] private List<GameObject> secondWeaponSlotSprites = new List<GameObject>();

    #region Main Weapon

    [Header("Main Weapon")]
    
    [SerializeField] private Slider mainWeaponUpgradeLevel;
    [SerializeField] private TextMeshProUGUI mainWeaponUpgradeCost;
    [SerializeField] private TextMeshProUGUI mainWeaponUpgradeText;
    [SerializeField] private Button mainWeaponUpgradeButton;
    [SerializeField] private List<GameObject> mainWeaponSprites;
    
    private int[] mainWeaponUpgradeCosts = { 
        500, 700, 1000, 1500, 2000, 2700,
        3500, 4500, 5700, 7000, 8500, 10000,
        12000, 15000, 18500, 22000, 26000, 30000,
        35000, 42000, 50000, 60000, 72000, 85000,
        100000, 120000, 150000, 200000, 300000, 0};
    private void UpdateMainWeaponInfo()
    {
        mainWeaponUpgradeLevel.value = GameManager.Instance.userSave.MainWeaponUpgrade % 6;
        mainWeaponUpgradeCost.text = mainWeaponUpgradeCosts[GameManager.Instance.userSave.MainWeaponUpgrade].ToString();
        if (mainWeaponUpgradeText.text == "Evolve")
        {
            foreach (GameObject sprite in mainWeaponSprites)
            {
                sprite.gameObject.SetActive(false);
            }
            mainWeaponSprites[GameManager.Instance.userSave.MainWeaponUpgrade / 6].gameObject.SetActive(true);
        }
        if (GameManager.Instance.userSave.MainWeaponUpgrade == 29)
        {
            mainWeaponUpgradeText.text = "Maxed";
            mainWeaponUpgradeCost.gameObject.SetActive(false);
            mainWeaponUpgradeButton.interactable = false;
        }
        else if (GameManager.Instance.userSave.MainWeaponUpgrade % 6 == 5)
        {
            mainWeaponUpgradeText.text = "Evolve";
        }
        else
        {
            mainWeaponUpgradeText.text = "Upgrade";
        }
        MenuUIManager.Instance.UpdateCostColors();
    }
    public void BuyMainWeaponUpgrade()
    {
        int price = mainWeaponUpgradeCosts[GameManager.Instance.userSave.MainWeaponUpgrade];
        if (price > GameManager.Instance.userSave.Gold)
        {
            Debug.Log("not enought gold");
            return;
        }
        GameManager.Instance.userSave.MainWeaponUpgrade++;
        GameManager.Instance.userSave.Gold -= price;
        UpdateMainWeaponInfo();
    }
    #endregion

    #region Fireblaster

    [Header("Fireblaster")]

    [SerializeField] private Slider fireblasterUpgradeLevel;
    [SerializeField] private TextMeshProUGUI fireblasterUpgradeCost;
    [SerializeField] private TextMeshProUGUI fireblasterUpgradeText;
    [SerializeField] private TextMeshProUGUI fireblasterAmmoCost;
    [SerializeField] private Button fireblasterUpgradeButton;
    [SerializeField] private Button fireblasterBuyAmmoButton;
    [SerializeField] private Button fireblasterEquipButton;
    [SerializeField] private TextMeshProUGUI fireblasterAmmo;

    private int[] fireblasterUpgradeCosts = {1000, 2000, 5000, 10000, 20000, 35000, 0 };
    private int fireblasterAmmoCostVariable = 300;
    private int fireblasterAmmoPackQuantity = 30;
    private void UpdateFireblasterInfo()
    {
        UpdateWeaponInfo(GameManager.Instance.userSave.FireblasterUpgrade, fireblasterUpgradeCost,
            fireblasterUpgradeCosts, fireblasterUpgradeText, fireblasterUpgradeButton);
        UpdateAmmo(fireblasterAmmo, GameManager.Instance.userSave.FireblasterAmmo);
        fireblasterUpgradeLevel.value = GameManager.Instance.userSave.FireblasterUpgrade - 1;
        fireblasterBuyAmmoButton.interactable = GameManager.Instance.userSave.FireblasterUpgrade != 0;
        fireblasterEquipButton.interactable = GameManager.Instance.userSave.FireblasterUpgrade != 0;
    }
    public void BuyFireblasterUpgrade()
    {
        bool hasPurchased = BuyWeaponUpgrade(fireblasterUpgradeCosts, GameManager.Instance.userSave.FireblasterUpgrade, fireblasterUpgradeLevel);
        if (hasPurchased)
        {
            GameManager.Instance.userSave.FireblasterUpgrade++;
            UpdateWeaponInfo(GameManager.Instance.userSave.FireblasterUpgrade, fireblasterUpgradeCost,
            fireblasterUpgradeCosts, fireblasterUpgradeText, fireblasterUpgradeButton);
            if (!fireblasterEquipButton.interactable)
            {
                fireblasterEquipButton.interactable = true;
                fireblasterBuyAmmoButton.interactable = true;
            }
        }
    }

    public void BuyFireblasterAmmo()
    {
        bool hasPurchased = BuyAmmo(fireblasterAmmoCostVariable);
        if (hasPurchased)
        {
            GameManager.Instance.extendedUserSave.Ammos["FireblasterAmmo"] += fireblasterAmmoPackQuantity;
            GameManager.Instance.extendedUserSave.SaveAmmoData();
            fireblasterAmmo.text = GameManager.Instance.userSave.FireblasterAmmo.ToString();
        }
    }
    #endregion
    
    #region Acid Blob

    [Header("Acid Blob")]

    [SerializeField] private Slider acidBlobUpgradeLevel;
    [SerializeField] private TextMeshProUGUI acidBlobUpgradeCost;
    [SerializeField] private TextMeshProUGUI acidBlobUpgradeText;
    [SerializeField] private TextMeshProUGUI acidBlobAmmoCost;
    [SerializeField] private Button acidBlobUpgradeButton;
    [SerializeField] private Button acidBlobBuyAmmoButton;
    [SerializeField] private Button acidBlobEquipButton;
    [SerializeField] private TextMeshProUGUI acidBlobAmmo;

    private int[] acidBlobUpgradeCosts = {2000, 3000, 6000, 11000, 21000, 36000, 0 };
    private int acidBlobAmmoCostVariable = 500;
    private int acidBlobAmmoPackQuantity = 20;
    private void UpdateAcidBlobInfo()
    {
        UpdateWeaponInfo(GameManager.Instance.userSave.AcidBlobUpgrade, acidBlobUpgradeCost,
            acidBlobUpgradeCosts, acidBlobUpgradeText, acidBlobUpgradeButton);
        UpdateAmmo(acidBlobAmmo, GameManager.Instance.userSave.AcidBlobAmmo);
        acidBlobUpgradeLevel.value = GameManager.Instance.userSave.AcidBlobUpgrade - 1;
        acidBlobBuyAmmoButton.interactable = GameManager.Instance.userSave.AcidBlobUpgrade != 0;
        acidBlobEquipButton.interactable = GameManager.Instance.userSave.AcidBlobUpgrade != 0;
    }
    public void BuyAcidBlobUpgrade()
    {
        bool hasPurchased = BuyWeaponUpgrade(acidBlobUpgradeCosts, GameManager.Instance.userSave.AcidBlobUpgrade, acidBlobUpgradeLevel);
        if (hasPurchased)
        {
            GameManager.Instance.userSave.AcidBlobUpgrade++;
            UpdateWeaponInfo(GameManager.Instance.userSave.AcidBlobUpgrade, acidBlobUpgradeCost,
            acidBlobUpgradeCosts, acidBlobUpgradeText, acidBlobUpgradeButton);
            if (!acidBlobEquipButton.interactable)
            {
                acidBlobEquipButton.interactable = true;
                acidBlobBuyAmmoButton.interactable = true;
            }
        }
    }

    public void BuyAcidBlobAmmo()
    {
        bool hasPurchased = BuyAmmo(acidBlobAmmoCostVariable);
        if (hasPurchased)
        {
            GameManager.Instance.extendedUserSave.Ammos["AcidBlobAmmo"] += acidBlobAmmoPackQuantity;
            GameManager.Instance.extendedUserSave.SaveAmmoData();
            acidBlobAmmo.text = GameManager.Instance.userSave.AcidBlobAmmo.ToString();
        }
    }
    #endregion

    #region Frost Shard

    [Header("Frost Shard")]

    [SerializeField] private Slider frostShardUpgradeLevel;
    [SerializeField] private TextMeshProUGUI frostShardUpgradeCost;
    [SerializeField] private TextMeshProUGUI frostShardUpgradeText;
    [SerializeField] private TextMeshProUGUI frostShardAmmoCost;
    [SerializeField] private Button frostShardUpgradeButton;
    [SerializeField] private Button frostShardBuyAmmoButton;
    [SerializeField] private Button frostShardEquipButton;
    [SerializeField] private TextMeshProUGUI frostShardAmmo;

    private int[] frostShardUpgradeCosts = { 3000, 5000, 8000, 15000, 30000, 55000, 0 };
    private int frostShardAmmoCostVariable = 500;
    private int frostShardAmmoPackQuantity = 10;
    private void UpdateFrostShardInfo()
    {
        UpdateWeaponInfo(GameManager.Instance.userSave.FrostShardUpgrade, frostShardUpgradeCost,
            frostShardUpgradeCosts, frostShardUpgradeText, frostShardUpgradeButton);
        UpdateAmmo(frostShardAmmo, GameManager.Instance.userSave.FrostShardAmmo);
        frostShardUpgradeLevel.value = GameManager.Instance.userSave.FrostShardUpgrade - 1;
        frostShardBuyAmmoButton.interactable = GameManager.Instance.userSave.FrostShardUpgrade != 0;
        frostShardEquipButton.interactable = GameManager.Instance.userSave.FrostShardUpgrade != 0;
    }
    public void BuyFrostShardUpgrade()
    {
        bool hasPurchased = BuyWeaponUpgrade(frostShardUpgradeCosts, GameManager.Instance.userSave.FrostShardUpgrade, frostShardUpgradeLevel);
        if (hasPurchased)
        {
            GameManager.Instance.userSave.FrostShardUpgrade++;
            UpdateWeaponInfo(GameManager.Instance.userSave.FrostShardUpgrade, frostShardUpgradeCost,
            frostShardUpgradeCosts, frostShardUpgradeText, frostShardUpgradeButton);
            if (!frostShardEquipButton.interactable)
            {
                frostShardEquipButton.interactable = true;
                frostShardBuyAmmoButton.interactable = true;
            }
        }
    }

    public void BuyFrostShardAmmo()
    {
        bool hasPurchased = BuyAmmo(frostShardAmmoCostVariable);
        if (hasPurchased)
        {
            GameManager.Instance.extendedUserSave.Ammos["FrostShardAmmo"] += frostShardAmmoPackQuantity;
            GameManager.Instance.extendedUserSave.SaveAmmoData();
            frostShardAmmo.text = GameManager.Instance.userSave.FrostShardAmmo.ToString();
        }
    }
    #endregion

    private void Start()
    {
        MenuUIManager.updateCostColors += UpdateCostColors;
        mainWeaponSprites[GameManager.Instance.userSave.MainWeaponUpgrade / 6].gameObject.SetActive(true);
        UpdateShopItems();
        UpdateSelectedWeapons();
    }

    private void UpdateShopItems()
    {
        UpdateMainWeaponInfo();
        UpdateFireblasterInfo();
        UpdateFrostShardInfo();
        UpdateAcidBlobInfo();
    }

    private void UpdateCostColors()
    {
        int gold = GameManager.Instance.userSave.Gold;
        if (mainWeaponUpgradeCosts[GameManager.Instance.userSave.MainWeaponUpgrade] > gold)
        {
            mainWeaponUpgradeCost.color = Color.red;
        }
        if (fireblasterUpgradeCosts[GameManager.Instance.userSave.FireblasterUpgrade] > gold)
        {
            fireblasterUpgradeCost.color = Color.red;
        }
        if (fireblasterAmmoCostVariable > gold)
        {
            fireblasterAmmoCost.color = Color.red;
        }
        if (frostShardUpgradeCosts[GameManager.Instance.userSave.FrostShardUpgrade] > gold)
        {
            frostShardUpgradeCost.color = Color.red;
        }
        if (frostShardAmmoCostVariable > gold)
        {
            frostShardAmmoCost.color = Color.red;
        }
        if (acidBlobUpgradeCosts[GameManager.Instance.userSave.AcidBlobUpgrade] > gold)
        {
            acidBlobUpgradeCost.color = Color.red;
        }
        if (acidBlobAmmoCostVariable > gold)
        {
            acidBlobAmmoCost.color = Color.red;
        }
    }
    private void UpdateSelectedWeapons()
    {
        UpdateEquipedWeaponSprite(firstWeaponSlotSprites, GameManager.Instance.userSave.FirstSelectedGun);
        UpdateEquipedWeaponSprite(secondWeaponSlotSprites, GameManager.Instance.userSave.SecondSelectedGun);
    }

    private void UpdateWeaponInfo(int gameManagerUpgradeLevel, TextMeshProUGUI upgradeCost, int[] upgradeCosts, TextMeshProUGUI upgradeText, Button upgradeButton)
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

    private void UpdateAmmo(TextMeshProUGUI ammo, int gameManagerAmmo)
    {
        ammo.text = gameManagerAmmo.ToString();
    }

    private bool BuyWeaponUpgrade(int[] costs, int gameManagerUpgradeLevel, Slider weaponUpgradeLevel)
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

    private bool BuyAmmo(int cost)
    {
        if (GameManager.Instance.userSave.Gold >= cost)
        {
            GameManager.Instance.userSave.Gold -= cost;
            MenuUIManager.Instance.UpdateCostColors();
            return true;
        }
        return false;
    }

    public void EquipWeapon(string weaponName)
    {
        if (GameManager.Instance.userSave.FirstSelectedGun == null && GameManager.Instance.userSave.SecondSelectedGun != weaponName)
        {
            GameManager.Instance.userSave.FirstSelectedGun = weaponName;
            UpdateEquipedWeaponSprite(firstWeaponSlotSprites, weaponName);
        }
        else if (GameManager.Instance.userSave.SecondSelectedGun == null && GameManager.Instance.userSave.FirstSelectedGun != weaponName)
        {
            GameManager.Instance.userSave.SecondSelectedGun = weaponName;
            UpdateEquipedWeaponSprite(secondWeaponSlotSprites, weaponName);
        }
    }

    private void UpdateEquipedWeaponSprite(List<GameObject> weaponSlot, string spriteName)
    {
        weaponSlot.Where(sprite => sprite.gameObject.name == spriteName).FirstOrDefault().SetActive(true);
    }

    public void UnequipFirstWeapon()
    {
        GameManager.Instance.userSave.FirstSelectedGun = null;
        firstWeaponSlotSprites.Where(sprite => sprite.gameObject.activeInHierarchy).FirstOrDefault().SetActive(false);
    }
    public void UnequipSecondWeapon()
    {
        GameManager.Instance.userSave.SecondSelectedGun = null;
        secondWeaponSlotSprites.Where(sprite => sprite.gameObject.activeInHierarchy).FirstOrDefault().SetActive(false);
    }

    public void SaveTransaction() // this wont be implemented, it will be called from GPGS manager
    {
        //save the game manager data into the save game
    }

    public void CancelTransaction() // this wont be implemented, it will be called from GPGS manager
    {
        // cancel transaction by loading the save game and reseting the game manager with the save file ammounts
    }
}
