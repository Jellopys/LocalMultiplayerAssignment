using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Step Climb")]
    [SerializeField] private GameObject _stepRayUpper;
    [SerializeField] private GameObject _stepRayLower;
    [SerializeField] float _stepSmooth = 0.1f;

    [Header("Camera")]
    [SerializeField] private float _turnSmoothTime = 0.1f;
    private Transform _camera;
    [SerializeField] public GameObject _aimCamPosition;

    [Header("Movement")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Transform _feetPos;
    [SerializeField] private float _checkGroundedDistance = 0.1f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private LayerMask _groundLayer;
    private float _turnSmoothVelocity;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main.transform;
    }

    public void MoveCharacter(Vector2 inputValue) // Gets inputValue from InputController
    {
        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 right = inputValue.x * cameraRight;
        Vector3 forward = inputValue.y * cameraForward;

        Vector3 cameraRelativeMovement = forward + right;

        Vector3 direction = new Vector3(inputValue.x, 0f, inputValue.y).normalized;
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

    void StepClimb() // For climbing small steps and moving up ramps
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
        if (Physics.Raycast(_feetPos.transform.position, -Vector3.up, _checkGroundedDistance, _groundLayer))
        {
            _rigidbody.velocity = Vector3.up * _jumpForce;
        }
    }

    public void Fire()
    {
        // DO I EVEN NEED THIS?
    }
}
