using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera _aimCamera;
    [SerializeField] private CinemachineVirtualCamera _nextCamera;

    public static CameraManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else 
        {
            instance = this;
        }
    }
}
