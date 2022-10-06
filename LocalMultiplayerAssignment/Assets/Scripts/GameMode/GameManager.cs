using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _totalCharacters = 4;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Material[] _bodyMaterials;
    [SerializeField] private Material[] _goggleMaterials;

    private void Start()
    {
        InitializePlayers();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void InitializePlayers()
    {
        for (int i = 0; i < _totalCharacters; i++)
        {
            GameObject character = Instantiate(_characterPrefab, GetRandomAvailableSpawnPoint(), Quaternion.identity);
            TurnManager.GetInstance().SetPlayerTeam(character);
            character.GetComponent<SetPlayerMaterial>().SetMaterial(_bodyMaterials[i], _goggleMaterials[i]);
            UIManager.GetInstance().InitPlayerProfiles(character, i);
        }
    }

    public Vector3 GetRandomAvailableSpawnPoint() // Spawning Players
    {
        Vector3 spawnPoint;
        int listPosition;
        listPosition = (Random.Range(0, _spawnPoints.Count - 1));
        spawnPoint = _spawnPoints[listPosition].position;
        _spawnPoints.RemoveAt(listPosition);
        return spawnPoint;
    }
}
