using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPrefabSelector : MonoBehaviour
{
    public List<GameObject> playerPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        ActivateMainWeapon();
    }

    public void ActivateMainWeapon()
    {
        playerPrefabs[GameManager.Instance.userSave.MainWeaponUpgrade / 6].SetActive(true);
        DisableSecondaryWeapons();
    }

    public void EquipFirstWeapon()
    {
        GameObject playerWithFirstWeapon = playerPrefabs.Where(prefab => prefab.gameObject.name == GameManager.Instance.userSave.FirstSelectedGun).FirstOrDefault();
        if (playerWithFirstWeapon != null)
        {
            playerWithFirstWeapon.SetActive(true);
            playerPrefabs[GameManager.Instance.userSave.MainWeaponUpgrade / 6].SetActive(false);

            GameObject playerWithSecondWeapon = playerPrefabs.Where(prefab => prefab.gameObject.name == GameManager.Instance.userSave.SecondSelectedGun).FirstOrDefault();
            playerWithSecondWeapon?.SetActive(false);
        }
        else
        {
            Debug.Log("No weapon found!");
        }
    }
    
    public void EquipSecondWeapon()
    {
        GameObject playerWithSecondWeapon = playerPrefabs.Where(prefab => prefab.gameObject.name == GameManager.Instance.userSave.SecondSelectedGun).FirstOrDefault();
        if (playerWithSecondWeapon != null)
        {
            playerWithSecondWeapon.SetActive(true);
            playerPrefabs[GameManager.Instance.userSave.MainWeaponUpgrade / 6].SetActive(false);

            GameObject playerWithFirstWeapon = playerPrefabs.Where(prefab => prefab.gameObject.name == GameManager.Instance.userSave.FirstSelectedGun).FirstOrDefault();
            playerWithFirstWeapon?.SetActive(false);
        }
        else
        {
            Debug.Log("No weapon found!");
        }
    }

    private void DisableSecondaryWeapons()
    {
        GameObject playerWithFirstWeapon = playerPrefabs.Where(prefab => prefab.gameObject.name == GameManager.Instance.userSave.FirstSelectedGun).FirstOrDefault();
        GameObject playerWithSecondWeapon = playerPrefabs.Where(prefab => prefab.gameObject.name == GameManager.Instance.userSave.SecondSelectedGun).FirstOrDefault();
        playerWithFirstWeapon?.SetActive(false);
        playerWithSecondWeapon?.SetActive(false);
    }
}
