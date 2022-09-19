using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _smoothness;
    [SerializeField] private Transform _targetObject;
    private Vector3 _initialOffset;
    private Vector3 _cameraPosition;

    void Start()
    {
        _initialOffset = transform.position - _targetObject.position;
    }

    void LateUpdate()
    {
        _cameraPosition = _targetObject.position + _initialOffset;
        transform.position = Vector3.Lerp(transform.position, _cameraPosition, _smoothness * Time.fixedDeltaTime);
    }

    void Awake()
    {
        TurnManager.TrigChangeTurn += EndTurn; // subscribe to TrigChangeTurn event and call EndTurn when the event is triggered.
    }

    void OnDisable()
    {
        TurnManager.TrigChangeTurn -= EndTurn; // unsubscribe
    }

    public void EndTurn()
    {
        _targetObject = TurnManager.GetInstance().GetCurrentPlayerTransform();
    }
}
