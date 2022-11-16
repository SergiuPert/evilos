using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedUserSave : UserSave
{
    public Dictionary<string, int> Ammos = new Dictionary<string, int>();
    private UserSave userSave;

    public ExtendedUserSave()
    {
        userSave = GameManager.Instance.userSave;
        Debug.Log(userSave);
        //Ammos = new Dictionary<string, int>()
        //{
        //    { AmmoType.FrostShardAmmo.ToString(), initialGameData.FrostShardAmmo },
        //    { AmmoType.FireblasterAmmo.ToString(), initialGameData.FireblasterAmmo },
        //};
        foreach (string i in Enum.GetNames(typeof(AmmoType)))
        {
            int value = (int)userSave.GetType().GetProperty(i).GetValue(userSave);
            Debug.Log(i);
            Debug.Log(value);
            Ammos.Add(i, value);
        }
        Debug.Log(Ammos);

    }

    public void SaveData()
    {
        foreach (string ammoType in Ammos.Keys)
        {
            userSave.GetType().GetProperty(ammoType).SetValue(userSave, Ammos[ammoType], null);
        }
        //userSave.Name = this.Name;  // se face automat printr-un object mapper
    }
}

public enum AmmoType
{
    FrostShardAmmo,
    FireblasterAmmo
}