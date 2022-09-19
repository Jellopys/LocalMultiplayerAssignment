using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private static CameraFollow instance;
    [SerializeField] private float _smoothness = 0.7f;
    [SerializeField] private Transform _targetObject;
    private Vector3 _initialOffset = new Vector3 (0, 8, -7);
    private Vector3 _cameraPosition;
    private TurnManager _turnManager;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else 
        {
            instance = this;
        }

        TurnManager.TrigChangeTurn += EndTurn; // subscribe to TrigChangeTurn event and call EndTurn when the event is triggered.
    }

    void Start()
    {
        // _initialOffset = transform.position - _targetObject.position;
        // Debug.Log(_initialOffset);
    }

    void LateUpdate()
    {
        if (_targetObject != null)
        {
            _cameraPosition = _targetObject.position + _initialOffset;
            transform.position = Vector3.Lerp(transform.position, _cameraPosition, _smoothness * Time.fixedDeltaTime);
        }
    }

    void OnDisable()
    {
        TurnManager.TrigChangeTurn -= EndTurn; // unsubscribe
    }

    public void EndTurn()
    {
        _targetObject = TurnManager.GetInstance().GetNewPlayerTransform();
    }
}
