using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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
    private Vector3 _direction;
    // private Vector2 _moveValue;
    private bool _canJump = true;

    void Awake()
    {
        _stepRayUpper.transform.position = new Vector3(_stepRayUpper.transform.position.x, _stepHeight, _stepRayUpper.transform.position.z);
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveCharacter();

        if (!_canJump && _rigidbody.velocity.y == 0)
        {
            _canJump = true;
        }
    }

    public void ReadInputValue(Vector2 moveValue)
    {
        _inputX = moveValue.x;
        _inputY = moveValue.y;
    }

    public void MoveCharacter()
    {
        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;
        // _inputX = moveValue.x;
        // _inputY = moveValue.y;

        Vector3 right = _inputY * cameraRight;
        Vector3 forward = _inputY * cameraForward;
        Vector3 cameraRelativeMovement = forward + right;
        transform.Translate(cameraRelativeMovement * _speed * Time.deltaTime, Space.World);        

        Vector3 direction = new Vector3(_inputY, 0f, _inputY).normalized;

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

    public void Jump()
    {
        if (_canJump)
        {
            _rigidbody.velocity = Vector3.up * _jumpForce;
            _canJump = false;
        }
    }
}
