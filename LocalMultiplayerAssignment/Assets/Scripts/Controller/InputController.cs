using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputController : MonoBehaviour
{

    [SerializeField] private Transform _projectileSpawnPoint;

    // References
    private PlayerMovement _playerMovement;
    private PlayerTurn _playerTurn;
    private WeaponManager _weaponManager;

    // InputActions
    private PlayerInputActions _inputsActions;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _fireAction;
    private InputAction _switchAction;
    private InputAction _lookAction;
    private InputAction _Weapon1Action;
    private InputAction _Weapon2Action;
    private InputAction _Weapon3Action;
    private InputAction _RMBAction;
    private Vector2 _moveValue;

    void Awake()
    {
        _playerTurn = GetComponent<PlayerTurn>();
        _playerMovement = GetComponent<PlayerMovement>();
        _inputsActions = new PlayerInputActions();
        _weaponManager = GetComponent<WeaponManager>();
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
        _fireAction.started += Fire;
        _fireAction.canceled += Fire;

        _switchAction = _inputsActions.Player.SwitchCharacter;
        _switchAction.Enable();
        _switchAction.performed += SwitchCharacter;

        _Weapon1Action = _inputsActions.Player.Weapon1;
        _Weapon1Action.Enable();
        _Weapon1Action.performed += SwitchToWeapon1;

        _Weapon2Action = _inputsActions.Player.Weapon2;
        _Weapon2Action.Enable();
        _Weapon2Action.performed += SwitchToWeapon2;

        _Weapon3Action = _inputsActions.Player.Weapon3;
        _Weapon3Action.Enable();
        _Weapon3Action.performed += SwitchToWeapon3;

        _RMBAction = _inputsActions.Player.RMB;
        _RMBAction.Enable();
        _RMBAction.performed += AimWeapon;
        _RMBAction.canceled += AimWeapon;
    }

    private void OnDisable() // Disables all inputActions
    {
        _moveAction.Disable();
        _jumpAction.Disable();
        _fireAction.Disable();
        _Weapon1Action.Disable();
        _Weapon2Action.Disable();
        _Weapon3Action.Disable();
        _RMBAction.Disable();
    }

    void Update()
    {
        if (_playerTurn.IsPlayerTurn())
        {
            _playerMovement.MoveCharacter(_moveAction.ReadValue<Vector2>());
        }
    }

    public void Jump(InputAction.CallbackContext context) // Space hotkey
    {
        if (!_playerTurn.IsPlayerTurn()) { return; }

        _playerMovement.Jump();
    }

    public void Fire(InputAction.CallbackContext context) // LMB hotkey
    {
        if (!_playerTurn.IsPlayerTurn()) { return; }

        if (context.started)
        {
            _weaponManager.Shoot(true);
        }
        else if (context.canceled)
        {
            _weaponManager.Shoot(false);
        }
    }

    public void SwitchCharacter(InputAction.CallbackContext context) // V hotkey
    {
        if (!_playerTurn.IsPlayerTurn()) { return; }

        TurnManager.GetInstance().TriggerChangeTurn();
    }

    public void SwitchToWeapon1(InputAction.CallbackContext context) // 1 Keyboard hotkey
    {
        if (!_playerTurn.IsPlayerTurn()) { return; }

        _weaponManager.SwitchWeapon(0);
    }

    public void SwitchToWeapon2(InputAction.CallbackContext context) // 2 Keyboard hotkey
    {
        if (!_playerTurn.IsPlayerTurn()) { return; }

        _weaponManager.SwitchWeapon(1);
    }

    public void SwitchToWeapon3(InputAction.CallbackContext context) // 3 Keyboard hotkey
    {
        if (!_playerTurn.IsPlayerTurn()) { return; }

        _weaponManager.SwitchWeapon(2);
    }

    public void AimWeapon(InputAction.CallbackContext context)
    {
        if (!_playerTurn.IsPlayerTurn()) { return; }
    }


    public void RMB(InputAction.CallbackContext context)
    {
        if (!_playerTurn.IsPlayerTurn()) { return; }
    }
}
