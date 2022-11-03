using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabSelector : MonoBehaviour
{
    public List<GameObject> playerPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        playerPrefabs[GameManager.Instance.userSave.MainWeaponUpgrade / 6].SetActive(true);
    }
}
