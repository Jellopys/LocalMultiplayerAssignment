using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;
    [SerializeField] private List<PlayerTurn> _teamOneCharacters;
    [SerializeField] private List<PlayerTurn> _charactersInTeam;
    [SerializeField] private float timeBetweenTurns;
    private Dictionary<int, int> teamStructure = new Dictionary<int, int>();
    
    private int currentPositionIndex;
    private int currentTeamIndex;
    private bool waitingForNextTurn;
    private float turnDelay;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else 
        {
            instance = this;
        }
        currentTeamIndex = 1;
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

    public bool IsItPlayerTurn(int teamIndex, int positionIndex)
    {
        if (waitingForNextTurn)
        { 
            return false;
        }

        return teamIndex == currentTeamIndex && positionIndex == currentPositionIndex;
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
        if (currentTeamIndex == 1)
        {
            currentTeamIndex = 2;
        }
        else if (currentTeamIndex == 2)
        {
            currentTeamIndex = 1;
        }
    }

    public Transform GetNewPlayerTransform()
    {
        if (currentTeamIndex == 1)
        {
            return _teamOneCharacters[0].gameObject.transform;
        }
        else if (currentTeamIndex == 2)
        {
            return _teamOneCharacters[1].gameObject.transform;
        }
        return _teamOneCharacters[0].gameObject.transform;

        // return _teamOneCharacters[currentTeamIndex].transform;
    }

    public void SetPlayerTeam(PlayerTurn playerCharacter, int team, int position)
    {
        teamStructure.Add(team, position);
        // _teamOneCharacters.Add(playerCharacter);
        playerCharacter.SetPlayerTurn(team, position);
    }
}
