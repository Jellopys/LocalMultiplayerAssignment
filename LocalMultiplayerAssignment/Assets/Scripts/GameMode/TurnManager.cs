using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour // SINGLETON
{
    private List<GameObject> _livingCharacters = new List<GameObject>();
    private GameObject _currentCharacter;
    private static TurnManager _instance;
    private int _currentCharacterIndex = 0;
    private float timeBetweenTurns = 1f;
    private int _currentPositionIndex;
    private int _currentTeamIndex;
    private bool _waitingForSwitch;
    private float _turnDelay;

    // ROUND TIMER
    [SerializeField] private TextMeshProUGUI _timerText;
    private float _roundTime = 10f;
    private float _currentRoundTime;
    private int _roundTimeInt;
    private bool _timerIsRunning;

    public delegate void DelegateChangeTurn();
    public static event DelegateChangeTurn ChangeCameraTarget;

    public static TurnManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else 
        {
            _instance = this;
        }
    }

    private void Update()
    {
        if (_currentRoundTime <= 0f) {ChangeTurn();}

        _currentRoundTime -= 1f * Time.deltaTime;
        _roundTimeInt = (int)_currentRoundTime;
        _timerText.text = _roundTimeInt.ToString();
    }

    public bool IsItPlayerTurn(GameObject character)
    {
        if (character == null) { return false; }

        if (!_waitingForSwitch)
            return character == _currentCharacter;
        else
            return false;
    }

    // IEnumerator StartRoundTimer()
    // {
    //     if (_timerIsRunning) {yield break;}
    //     Debug.Log("Start Timer");
    //     _currentRoundTime = _roundTime;
    //     _timerIsRunning = true;
    //     yield return new WaitForSeconds(_roundTime);
    //     ChangeTurn();
    //     _timerIsRunning = false;
    // }

    // public void TriggerChangeTurn()
    // {
    //     _waitingForSwitch = true;
    // }

    public void ChangeTurn()
    {
        _currentRoundTime = _roundTime;
        _currentCharacterIndex++;

        if (_currentCharacterIndex >= _livingCharacters.Count)
        {
            _currentCharacter = _livingCharacters[0];
            _currentCharacterIndex = 0;
        }
        else
        {
            _currentCharacter = _livingCharacters[_currentCharacterIndex];
        }

        _currentCharacter.GetComponent<WeaponManager>().Reload();

        if (ChangeCameraTarget != null)
            ChangeCameraTarget();
    }

    public GameObject GetPlayerObject()
    {
        return _currentCharacter;
    }

    public void SetPlayerTeam(GameObject character)
    {
        _livingCharacters.Add(character);
        _currentCharacter = _livingCharacters[0];
        _currentRoundTime = _roundTime;

        if (ChangeCameraTarget != null)
            ChangeCameraTarget();
    }

    public void CharacterDied(GameObject character)
    {
        if (character == _currentCharacter)
        {
            ChangeTurn();
        }

        _livingCharacters.Remove(character);
        Destroy(character);

        if (_livingCharacters.Count <= 1)
        {
            Debug.Log("VICTORY");
            // TODO: PROPER VICTORY UI
        }
    }
}
