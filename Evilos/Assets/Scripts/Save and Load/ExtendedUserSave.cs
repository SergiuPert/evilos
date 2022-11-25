using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedUserSave : UserSave
{
    public Dictionary<string, int> Ammos = new Dictionary<string, int>();
    public Dictionary<string, int> Scrolls = new Dictionary<string, int>();
    private UserSave userSave;

    public ExtendedUserSave()
    {
        userSave = GameManager.Instance.userSave;
        //Ammos = new Dictionary<string, int>()
        //{
        //    { AmmoType.FrostShardAmmo.ToString(), initialGameData.FrostShardAmmo },
        //    { AmmoType.FireblasterAmmo.ToString(), initialGameData.FireblasterAmmo },
        //};
        foreach (string ammo in Enum.GetNames(typeof(AmmoType)))
        {
            int value = (int)userSave.GetType().GetProperty(ammo).GetValue(userSave);
            Ammos.Add(ammo, value);
        }
        foreach (string scroll in Enum.GetNames(typeof(ScrollType)))
        {
            int value = (int)userSave.GetType().GetProperty(scroll).GetValue(userSave);
            Scrolls.Add(scroll, value);
        }
    }

    public void SaveAmmoData() // could be optimized to update speciffic ammo
    {
        foreach (string ammoType in Ammos.Keys)
        {
            userSave.GetType().GetProperty(ammoType).SetValue(userSave, Ammos[ammoType], null);
        }
        //userSave.Name = this.Name;  // se face automat printr-un object mapper
    }
    public void SaveScrollsData() // could be optimized to update speciffic scrolls
    {
        foreach (string scrollType in Scrolls.Keys)
        {
            userSave.GetType().GetProperty(scrollType).SetValue(userSave, Scrolls[scrollType], null);
        }
        //userSave.Name = this.Name;  // se face automat printr-un object mapper
    }
}

public enum AmmoType
{
    FrostShardAmmo,
    FireblasterAmmo,
    AcidBlobAmmo
}

public enum ScrollType
{
    ElectricShockScrolls,
    ChainsOfTormentScrolls,
    FrostNovaScrolls,
    DragonRoarScrolls,
    CursedRingScrolls
}