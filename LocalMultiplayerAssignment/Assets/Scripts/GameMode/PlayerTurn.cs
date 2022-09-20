using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    [SerializeField] private int teamIndex;
    [SerializeField] private int positionIndex;

    public void SetPlayerTurn(int team, int position)
    {
        teamIndex = team;
        positionIndex = position;
    }
    
    public bool IsPlayerTurn()
    {
        return TurnManager.GetInstance().IsItPlayerTurn(this.gameObject);
    }

    public int GetPlayerIndex()
    {
        return teamIndex;
    }
}
