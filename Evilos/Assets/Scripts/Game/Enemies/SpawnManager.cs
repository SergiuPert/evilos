using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyWavesForLevel;
    void Start()
    {
        Instantiate(enemyWavesForLevel[GameManager.Instance.levelIndex]);
    }
}
