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

}
