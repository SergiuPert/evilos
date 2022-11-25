using System;
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
    
    public string FirstSelectedGun { get; set; }
    public string SecondSelectedGun { get; set; }

    public string FirstSelectedSpell { get; set; }
    public string SecondSelectedSpell { get; set; }
    public string ThirdSelectedSpell { get; set; }
    public string FourthSelectedSpell { get; set; }

    public int MainWeaponUpgrade { get; set; }
    public int FireblasterUpgrade { get; set; }
    public int FireblasterAmmo { get; set; }
    public int FrostShardUpgrade { get; set; }
    public int FrostShardAmmo { get; set; } // test if property order matters
    public int AcidBlobUpgrade { get; set; }
    public int AcidBlobAmmo { get; set; }

    public int ElectricShockUpgrade { get; set; }
    public int ElectricShockScrolls { get; set; }
    public int CursedRingUpgrade { get; set; }
    public int CursedRingScrolls { get; set; }
    public int ChainsOfTormentUpgrade { get; set; }
    public int ChainsOfTormentScrolls { get; set; }
    public int FrostNovaUpgrade { get; set; }
    public int FrostNovaScrolls { get; set; }
    public int DragonRoarUpgrade { get; set; }
    public int DragonRoarScrolls { get; set; }

}