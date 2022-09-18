using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerCustom : MonoBehaviour
{
    private PlayerTurn _playerTurn;
    private PlayerInputActions _inputsActions;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    

    [Header("Player Step Climb")]
    [SerializeField] private GameObject _stepRayUpper;
    [SerializeField] private GameObject _stepRayLower;
    [SerializeField] float _stepHeight = 0.3f;
    [SerializeField] float _stepSmooth = 0.1f;

    [Header("Camera")]
    [SerializeField] private Transform _camera;
    [SerializeField] private float _turnSmoothTime = 0.1f;

    [Header("Movement")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Transform _feetPos;
    [SerializeField] private float _checkGroundedDistance;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _jumpForce;
    private float _inputX;
    private float _inputY;
    private float _turnSmoothVelocity;
    private Rigidbody _rigidbody;
    private Vector2 _moveValue;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerTurn = GetComponent<PlayerTurn>();
        _inputsActions = new PlayerInputActions();
        
    }

    private void OnEnable()
    {
        _moveAction = _inputsActions.Player.Move;
        _moveAction.Enable();
        _jumpAction = _inputsActions.Player.Jump;
        _jumpAction.Enable();
        _jumpAction.performed += Jump;
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _jumpAction.Disable();
    }

    void Update()
    {
        MoveCharacter();

        if (_playerTurn.IsPlayerTurn())
        {
            _moveValue = _moveAction.ReadValue<Vector2>();
        }
    }

    void MoveCharacter()
    {
        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;
        _inputX = _moveValue.x;
        _inputY = _moveValue.y;

        Vector3 right = _inputX * cameraRight;
        Vector3 forward = _inputY * cameraForward;

        Vector3 cameraRelativeMovement = forward + right;

        Vector3 direction = new Vector3(_inputX, 0f, _inputY).normalized;
        transform.Translate(cameraRelativeMovement * _speed * Time.deltaTime, Space.World);        

        if (direction.magnitude >= 0.1f)
        {
            // Rotate player
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            StepClimb();
        }
    }

    void StepClimb()
    {
        // For climbing small steps and moving up ramps
        float raycastDistance = 0.2f;
        float raycastDistanceUpper = 0.35f;
        RaycastHit hitLower;

        if (Physics.Raycast(_stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, raycastDistance))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(_stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, raycastDistanceUpper))
            {
                Debug.DrawRay(_stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), Color.red, 100f);
                transform.Translate(0f, _stepSmooth, 0f);
            }
        }
    }


    public bool CheckIfGrounded()
    {
        if (Physics.Raycast(_feetPos.transform.position, -Vector3.up, _checkGroundedDistance, _groundLayer)) 
        {
            return true;
        }
        return false;
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (!_playerTurn.IsPlayerTurn()) { return; }

                if (Physics.Raycast(_feetPos.transform.position, -Vector3.up, _checkGroundedDistance, _groundLayer))
        {
            _rigidbody.velocity = Vector3.up * _jumpForce;
        }
    }
}
