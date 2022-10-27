using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabSelector : MonoBehaviour
{
    public List<GameObject> playerPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        playerPrefabs[GameManager.Instance.mainWeaponUpgrade / 6].SetActive(true);
    }
}
