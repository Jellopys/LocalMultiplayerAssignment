using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    private int teamIndex;
    private int positionIndex;

    public void SetPlayerTurn(int team, int position)
    {
        teamIndex = team;
        positionIndex = position;
    }
    
    public bool IsPlayerTurn()
    {
        return TurnManager.GetInstance().IsItPlayerTurn(teamIndex, positionIndex);
    }

    public int GetPlayerIndex()
    {
        return teamIndex;
    }
}
