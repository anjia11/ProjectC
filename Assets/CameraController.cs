using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    
    void Start()
    {
        _cinemachineVirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();

        _cinemachineVirtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
