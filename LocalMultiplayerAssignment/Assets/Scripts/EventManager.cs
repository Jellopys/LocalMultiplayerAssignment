using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private enum ETeam {Team1, Team2};
    private int _currentTeam;

    private Dictionary <ETeam, int> _Teams = new Dictionary<ETeam, int>()
    {
        {ETeam.Team1, 3},
        {ETeam.Team2, 3}
    };

    public void EndTurn()
    {
        if (_currentTeam == 1)
        {
            _currentTeam = 2;
        }
        else if (_currentTeam == 2)
        {
            _currentTeam = 1;
        }
    }

    public int GetCurrentTeam()
    {
        return _currentTeam;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }

    public static EventManager GetInstance()
    {
        return Instance;
    }
}
