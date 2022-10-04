using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private static TurnManager instance;
    [SerializeField] private List<GameObject> _livingCharacters;
    [SerializeField] private GameObject _currentCharacter;
    [SerializeField] private int _currentCharacterIndex = 0;
    [SerializeField] private float timeBetweenTurns;
    
    private int currentPositionIndex;
    private int currentTeamIndex;
    private bool waitingForSwitch;
    private float turnDelay;

    public delegate void DelegateChangeTurn();
    public static event DelegateChangeTurn ChangeCameraTarget;

    
    public static TurnManager GetInstance()
    {
        return instance;
    }

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
                ChangeTurn();
            }
        }
    }


    public bool IsItPlayerTurn(GameObject character)
    {
        if (character == null) { return false; }

        if (!waitingForSwitch)
            return character == _currentCharacter;
        else
            return false;
    }

    public void TriggerChangeTurn()
    {
        waitingForSwitch = true;
    }

    private void ChangeTurn()
    {
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
