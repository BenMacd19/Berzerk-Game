using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] Camera mainCamera;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] GameObject followPointer;

    [SerializeField] GameObject player;
    
    Vector3 mousePosition = new Vector3();
    Vector3 followPointerPosition = new Vector3();

    // Update is called once per frame
    void Update()
    {
        // Calculate position for mouse tracker
        mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0f;

        // Calulate position for follow pointer
        followPointerPosition = (mousePosition + player.transform.position) / 2;
        followPointerPosition = (followPointerPosition + player.transform.position) / 2;
        followPointer.transform.position = followPointerPosition;
    }
}
