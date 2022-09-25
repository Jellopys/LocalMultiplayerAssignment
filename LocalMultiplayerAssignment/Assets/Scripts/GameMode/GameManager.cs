using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _totalCharacters = 4;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private List<Transform> _spawnPoints;

    public float timeRemaining = 2;
    public bool timerIsRunning = false;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        for (int i = 0; i < _totalCharacters; i++)
        {
            GameObject character = Instantiate(_characterPrefab, GetRandomAvailableSpawnPoint(), Quaternion.identity);
            TurnManager.GetInstance().SetPlayerTeam(character);
            character.GetComponent<PlayerHealth>().InitializeUI(i + 1);
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
