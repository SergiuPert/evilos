using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Slider mainWeaponUpgradeLevel;
    [SerializeField] private TextMeshProUGUI mainWeaponUpgradeCost;
    [SerializeField] private TextMeshProUGUI mainWeaponUpgradeText;
    [SerializeField] private List<GameObject> mainWeaponSprites;
    [SerializeField] private Button mainWeaponUpgradeButton;
    private int[] mainWeaponUpgradeCosts = { 
        500, 700, 1000, 1500, 2000, 2700,
        3500, 4500, 5700, 7000, 8500, 10000,
        12000, 15000, 18500, 22000, 26000, 30000,
        35000, 42000, 50000, 60000, 72000, 85000,
        100000, 120000, 150000, 200000, 300000, 0};
    private void Start()
    {
        mainWeaponSprites[GameManager.Instance.userSave.MainWeaponUpgrade / 6].gameObject.SetActive(true);
        UpdateShopItems();
    }

    private void UpdateShopItems()
    {
        UpdateMainWeaponInfo();
    }

    private void UpdateCostColors()
    {
        int gold = GameManager.Instance.userSave.Gold;
        if (mainWeaponUpgradeCosts[GameManager.Instance.userSave.MainWeaponUpgrade] > gold)
        {
            mainWeaponUpgradeCost.color = Color.red;
        }
    }


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
            //Button upgradebutton = mainWeaponUpgradeText.gameObject.GetComponentInParent<Button>();
            //upgradebutton.interactable = false;
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
        UpdateCostColors();
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

    public void SaveTransaction() // this wont be implemented, it will be called from GPGS manager
    {
        //save the game manager data into the save game
    }

    public void CancelTransaction() // this wont be implemented, it will be called from GPGS manager
    {
        // cancel transaction by loading the save game and reseting the game manager with the save file ammounts
    }
}
