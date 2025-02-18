using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    [Header("Currency")] public int scoreData;

    [Header("PlayerMovement")] public bool canDashData;
    
    
 
    
    public GameData()
    {
        //Currency
        this.scoreData = 0;
        this.canDashData = false;
        
    }
    
}
