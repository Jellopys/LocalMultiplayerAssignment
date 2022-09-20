using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _smoothness = 0.7f;
    [SerializeField] private Transform _targetObject;
    private Vector3 _initialOffset = new Vector3 (0, 16, -15);
    private Vector3 _cameraPosition;
    private PlayerInputActions _inputsActions;
    private InputAction _lookAction;
    private InputAction _RMBAction;
    private bool _freeMovement = false;

    void Awake()
    {
        TurnManager.ChangeCameraTarget += SwitchTarget; // subscribe to TrigChangeTurn event and call EndTurn when the event is triggered.
        _inputsActions = new PlayerInputActions();
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

    void LateUpdate()
    {
        if (_targetObject != null)
        {
            _cameraPosition = _targetObject.position + _initialOffset;
            transform.position = Vector3.Lerp(transform.position, _cameraPosition, _smoothness * Time.fixedDeltaTime);
        }
    }


    public void SwitchTarget()
    {
        _targetObject = TurnManager.GetInstance().GetNewPlayerTransform();
    }

    public void RMB(InputAction.CallbackContext context) // RightTrigger hotkey
    {
        Debug.Log("RMB action");
        _freeMovement = true;
    }
}
