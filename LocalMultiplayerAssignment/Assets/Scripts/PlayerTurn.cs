using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    private int playerIndex;

    public void SetPlayerTurn(int index)
    {
        playerIndex = index;
    }
    
    public bool IsPlayerTurn()
    {
        return TurnManager.GetInstance().IsItPlayerTurn(playerIndex);
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }
}
