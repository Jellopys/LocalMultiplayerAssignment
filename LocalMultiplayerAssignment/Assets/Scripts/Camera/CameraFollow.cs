using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    private CinemachineFreeLook _cinemachine;


    private GameObject _targetObject;
    // private PlayerInputActions _inputsActions;
    // private InputAction _lookAction;
    // private InputAction _RMBAction;
    private bool _freeMovement = false;

    void Awake()
    {
        TurnManager.ChangeCameraTarget += SwitchTarget; // subscribe to TrigChangeTurn event and call EndTurn when the event is triggered.
        _cinemachine = GetComponent<CinemachineFreeLook>();
        // _inputsActions = new PlayerInputActions();
    }

    void Update()
    {
        
    }

    private void OnEnable() // Initializes all inputActions
    {
        // _lookAction = _inputsActions.Player.Look;
        // _lookAction.Enable();
        // _RMBAction = _inputsActions.Player.RMB;
        // _RMBAction.Enable();
        // _RMBAction.performed += RMB;
        // _RMBAction.canceled += RMB;
    }

    void OnDisable()
    {
        TurnManager.ChangeCameraTarget -= SwitchTarget; // unsubscribe
        // _lookAction.Disable();
        // _RMBAction.Disable();
    }

    public void SwitchTarget()
    {
        _targetObject = TurnManager.GetInstance().GetPlayerObject();
        _cinemachine.m_LookAt = _targetObject.transform;
        _cinemachine.m_Follow = _targetObject.transform;
    }

    // public void RMB(InputAction.CallbackContext context) // RightTrigger hotkey
    // {
    //     _cinemachine.m_LookAt = _targetObject.GetComponent<PlayerMovement>()._aimCamPosition.transform;
    //     _cinemachine.m_Follow = _targetObject.GetComponent<PlayerMovement>()._aimCamPosition.transform;
        
    // }
}
