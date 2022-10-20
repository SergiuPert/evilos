using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public void BuyItem(int itemIndex, int price)
    {
        //update game manager
    }

    public void SaveTransaction() // this wont be implemented, it will be called from GPGS manager
    {
        //save the game manager data into the save game
    }

    public void CancelTransaction() // this wont be implemented, it will be called from GPGS manager
    {
        // cancel transaction by loading the save game and reseting the game manager with the save file ammounts
    }
}
