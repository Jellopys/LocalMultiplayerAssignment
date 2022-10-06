using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    private int _teamIndex;
    private int _positionIndex;
    
    public bool IsPlayerTurn()
    {
        if (this == null) {return false;}
        return TurnManager.GetInstance().IsItPlayerTurn(this.gameObject);
    }

    public int GetPlayerIndex()
    {
        return _teamIndex;
    }
}
