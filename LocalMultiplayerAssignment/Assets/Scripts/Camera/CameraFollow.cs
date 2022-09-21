using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    private CinemachineFreeLook _cinemachine;

    [SerializeField] private Transform _targetObject;
    private PlayerInputActions _inputsActions;
    private InputAction _lookAction;
    private InputAction _RMBAction;
    private bool _freeMovement = false;

    void Awake()
    {
        TurnManager.ChangeCameraTarget += SwitchTarget; // subscribe to TrigChangeTurn event and call EndTurn when the event is triggered.
        _inputsActions = new PlayerInputActions();
        _cinemachine = GetComponent<CinemachineFreeLook>();
    }

    private void OnEnable() // Initializes all inputActions
    {
        _lookAction = _inputsActions.Player.Look;
        _lookAction.Enable();
        _RMBAction = _inputsActions.Player.Fire;
        _RMBAction.Enable();
        _RMBAction.performed += RMB;
    }

    void OnDisable()
    {
        TurnManager.ChangeCameraTarget -= SwitchTarget; // unsubscribe
        _lookAction.Disable();
        _RMBAction.Disable();
    }

    public void SwitchTarget()
    {
        _targetObject = TurnManager.GetInstance().GetNewPlayerTransform();
        _cinemachine.m_LookAt = _targetObject;
        _cinemachine.m_Follow = _targetObject;
    }

    public void RMB(InputAction.CallbackContext context) // RightTrigger hotkey
    {
        Debug.Log("RMB action");
        _freeMovement = true;
    }
}
