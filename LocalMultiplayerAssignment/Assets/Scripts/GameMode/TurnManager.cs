using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;
    [SerializeField] private List<PlayerTurn> _teamCaptains;
    [SerializeField] private List<int> _team;
    [SerializeField] private List<PlayerTurn> _positionInTeam;
    [SerializeField] private float timeBetweenTurns;
    private Dictionary<int, int> teamStructure = new Dictionary<int, int>();
    
    private int currentPositionIndex;
    private int currentTeamIndex;
    private bool waitingForSwitch;
    private float turnDelay;
    private int uglySwitchIntCHANGETHIS;

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
        currentTeamIndex = 0;
        currentPositionIndex = 0;
    }

    private void Update()
    {
        if (waitingForSwitch)
        {
            turnDelay += Time.deltaTime;
            if (turnDelay >= timeBetweenTurns)
            {
                turnDelay = 0;
                waitingForSwitch = false;
                if (uglySwitchIntCHANGETHIS == 0)
                {
                    SwitchCharacter();
                }
                else 
                    ChangeTurn();
            }
        }
    }

    public delegate void DelegateChangeTurn();
    public static event DelegateChangeTurn TrigChangeTurn;

    public bool IsItPlayerTurn(int teamIndex, int positionIndex)
    {
        if (waitingForSwitch)
        { 
            return false;
        }
        if (teamIndex == currentTeamIndex && positionIndex == currentPositionIndex)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static TurnManager GetInstance()
    {
        return instance;
    }

    public void TriggerChangeTurn()
    {
        waitingForSwitch = true;
        uglySwitchIntCHANGETHIS = 1;

        if (TrigChangeTurn != null)
            TrigChangeTurn();

    }

    private void ChangeTurn()
    {
        Debug.Log("current team index is " + currentTeamIndex + " and teamstructure dictionary count is " + teamStructure.Count);
        if (currentTeamIndex == teamStructure.Count - 1)
        {
            currentTeamIndex = 0;
        }
        else
            currentTeamIndex++;
    }

    public void TriggerSwitchCharacter()
    {
        waitingForSwitch = true;
        uglySwitchIntCHANGETHIS = 0;
    }

    private void SwitchCharacter()
    {
        if (currentPositionIndex == teamStructure[currentTeamIndex])
        {
            currentPositionIndex = 0;
        }
        else 
        {
            currentPositionIndex++;
        }
    }

    public Transform GetNewPlayerTransform()
    {
        return _teamCaptains[currentTeamIndex].gameObject.transform;
    }

    public void SetPlayerTeam(PlayerTurn playerCharacter, int team, int position)
    {
        if (!teamStructure.ContainsKey(team)) // If the team does not exist, create team
        {
            teamStructure.Add(team, position);
            _teamCaptains.Add(playerCharacter);
        }
        else
        {
            teamStructure[team] = position; // If the team exists, change that teams' character count
        }

        if (_team.Contains(team))
        {
            _team.Add(team);
        }
        playerCharacter.SetPlayerTurn(team, position);
    }
}
