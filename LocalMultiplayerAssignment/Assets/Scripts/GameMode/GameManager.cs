using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _numberOfTeams = 2;
    [SerializeField] private int _teamSize = 2;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private List<Transform> _spawnPoints;

    public float timeRemaining = 2;
    public bool timerIsRunning = false;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        Initialize();
    }
    void Update()
    {
        // if (timerIsRunning)
        // {
        //     if (timeRemaining > 0)
        //     {
        //         timeRemaining -= Time.deltaTime;
        //     }
        //     else
        //     {
        //         Debug.Log("hi");
        //         Initialize();
        //         timeRemaining = 0;
        //         timerIsRunning = false;
        //     }
        // }
    }

    void Awake()
    {
        
        // for (int i = 0; i < _numberOfTeams; i++)
        // {
        //     InstantiateCharacters();
        // }
    }

    public void Initialize()
    {
        for (int i = 0; i < _numberOfTeams; i++)
        {
            InitCharsInTeam(i);
        }
    }

    public void InitCharsInTeam(int team)
    {
        for (int i = 0; i < _teamSize; i++)
        {
            GameObject character = Instantiate(_characterPrefab, GetRandomAvailableSpawnPoint(), Quaternion.identity);
            PlayerTurn playerTurnRef = character.GetComponent<PlayerTurn>();
            TurnManager.GetInstance().SetPlayerTeam(playerTurnRef, team, i);
        }
    }

    public Vector3 GetRandomAvailableSpawnPoint()
    {
        Vector3 spawnPoint;
        int listPosition;

        listPosition = (Random.Range(0, _spawnPoints.Count - 1));
        spawnPoint = _spawnPoints[listPosition].position;
        _spawnPoints.RemoveAt(listPosition);
        return spawnPoint;
    }
}
