using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _totalCharacters = 4;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private List<Transform> _spawnPoints;
    // [SerializeField] private GameObject _characterProfileGUI;

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
            UIManager.GetInstance().InitPlayerProfiles(character, i);

            // // INIT Player Profile UI
            // RectTransform profileContainer = UIManager.GetInstance().GetProfileContainer();
            // UIManager.GetInstance().InitPlayerProfiles(character);
            // Vector3 spawnLocation = new Vector3(profileContainer.localPosition.x, profileContainer.localPosition.y, profileContainer.localPosition.z);
            // GameObject playerProfile = Instantiate(_characterProfileGUI, spawnLocation, Quaternion.identity);
            // playerProfile.GetComponent<RectTransform>().SetParent(profileContainer);
            // playerProfile.GetComponent<CharacterProfileGUI>().Initialize(i, character.GetComponent<PlayerHealth>());
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
