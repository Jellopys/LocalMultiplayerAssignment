using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    private CinemachineFreeLook _cinemachine;
    private GameObject _targetObject;

    void Awake()
    {
        TurnManager.ChangeCameraTarget += SwitchTarget; // subscribe to TrigChangeTurn event and call EndTurn when the event is triggered.
        _cinemachine = GetComponent<CinemachineFreeLook>();
    }

    void OnDisable()
    {
        TurnManager.ChangeCameraTarget -= SwitchTarget; // unsubscribe
    }

    public void SwitchTarget()
    {
        _targetObject = TurnManager.GetInstance().GetPlayerObject();
        _cinemachine.m_LookAt = _targetObject.transform;
        _cinemachine.m_Follow = _targetObject.transform;
    }
}
