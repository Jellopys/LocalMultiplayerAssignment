using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;
    [SerializeField] private PlayerTurn playerOne;
    [SerializeField] private PlayerTurn playerTwo;
    [SerializeField] private float timeBetweenTurns;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform playerOneTransform;
    [SerializeField] private Transform playerTwoTransform;
    
    private int currentPlayerIndex;
    private bool waitingForNextTurn;
    private float turnDelay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            currentPlayerIndex = 1;
            playerOne.SetPlayerTurn(1);
            playerTwo.SetPlayerTurn(2);
        }
    }

    private void Update()
    {
        if (waitingForNextTurn)
        {
            turnDelay += Time.deltaTime;
            if (turnDelay >= timeBetweenTurns)
            {
                turnDelay = 0;
                waitingForNextTurn = false;
                ChangeTurn();
            }
        }
    }

    public delegate void DelegateChangeTurn();
    public static event DelegateChangeTurn TrigChangeTurn;

    public bool IsItPlayerTurn(int index)
    {
        if (waitingForNextTurn)
        { 
            return false;
        }

        return index == currentPlayerIndex;
    }

    public static TurnManager GetInstance()
    {
        return instance;
    }

    public void TriggerChangeTurn()
    {
        waitingForNextTurn = true;

        if (TrigChangeTurn != null)
            TrigChangeTurn();

    }

    private void ChangeTurn()
    {
        if (currentPlayerIndex == 1)
        {
            currentPlayerIndex = 2;
        }
        else if (currentPlayerIndex == 2)
        {
            currentPlayerIndex = 1;
        }
    }

    public Transform GetCurrentPlayerTransform()
    {
        if (currentPlayerIndex == 1)
        {
            return playerOneTransform;
        }
        else if (currentPlayerIndex == 2)
        {
            return playerTwoTransform;
        }
        return playerOneTransform;
    }
}
