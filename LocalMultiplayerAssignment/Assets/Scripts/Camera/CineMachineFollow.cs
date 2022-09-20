using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineMachineFollow : MonoBehaviour
{
    private CinemachineFreeLook _camera;
    private Transform _transform;

    // Start is called before the first frame update
    void Awake()
    {
        Camera.main.gameObject.TryGetComponent<CinemachineFreeLook>(out var brain);
        if (brain == null) {brain = Camera.main.gameObject.AddComponent<CinemachineFreeLook>();}

        brain.Follow = _transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
