using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputController : MonoBehaviour
{
    // References
    private PlayerMovement _playerMovement;
    private PlayerTurn _playerTurn;

    // InputActions
    private PlayerInputActions _inputsActions;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _fireAction;
    private Vector2 _moveValue;

    void Awake()
    {
        _playerTurn = GetComponent<PlayerTurn>();
        _playerMovement = GetComponent<PlayerMovement>();
        _inputsActions = new PlayerInputActions();
    }

    private void OnEnable() // Initializes all inputActions
    {
        _moveAction = _inputsActions.Player.Move;
        _moveAction.Enable();
        _jumpAction = _inputsActions.Player.Jump;
        _jumpAction.Enable();
        _jumpAction.performed += Jump;
        _fireAction = _inputsActions.Player.Fire;
        _fireAction.Enable();
        _fireAction.performed += Fire;
    }

    private void OnDisable() // Disables all inputActions
    {
        _moveAction.Disable();
        _jumpAction.Disable();
        _fireAction.Disable();
    }

    void Update()
    {
        if (_playerTurn.IsPlayerTurn())
        {
            _playerMovement.MoveCharacter(_moveAction.ReadValue<Vector2>());
        }
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (!_playerTurn.IsPlayerTurn()) { return; }

        _playerMovement.Jump();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (!_playerTurn.IsPlayerTurn()) { return; }

        TurnManager.GetInstance().TriggerChangeTurn();
        // _camera.depth--;
    }
}