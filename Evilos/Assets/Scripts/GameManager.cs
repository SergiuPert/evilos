using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void SceneChange();
    public static event SceneChange sceneChange;

    public UserSave userSave = new UserSave();
    public bool gameRunning = false;
    public bool isSecondaryLoad;
    public int missileIndex = 0;
    public int levelIndex = 0;
    public int additionalHealth = 50; // to be changed
    //public int gold;

    private void Awake()
    {
        if (Instance != null)
        {
            sceneChange = null; // for clearing events
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void GetPlayerData()
    {

    }

    public void UpdatePlayerDataWin()
    {

    }

    public void UpdatePlayerDataLose()
    {

    }

    public void ChangeScene()
    {
        sceneChange();
    }

}
