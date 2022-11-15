using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserSave
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public int Gold { get; set; }
    public int HighestLevelCompleted { get; set; }
    public int Reanimations { get; set; }
    public int ManaBoost { get; set; }
    public int MainWeaponUpgrade { get; set; }
    public int FireblasterUpgrade { get; set; }
    public int FireblasterAmmo { get; set; }
    public int FrostShardUpgrade { get; set; }
    public int FrostShardAmmo { get; set; }
    public string FirstSelectedGun { get; set; }
    public string SecondSelectedGun { get; set; }
}
