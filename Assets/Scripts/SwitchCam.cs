using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchCam : MonoBehaviour
{

    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private int boost = 10;
    private InputAction aimAction;
    private CinemachineVirtualCamera camera;
    [SerializeField]
    private Canvas thirdPersonCamera;
    [SerializeField]
    private Canvas AimCamera;

    // Start is called before the first frame update
    private void Awake()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
    }

    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }

    private void StartAim()
    {
        camera.Priority += boost;
        AimCamera.enabled = true;
        thirdPersonCamera.enabled = false;
    }   

    private void CancelAim()
    {
        camera.Priority -= boost;
        AimCamera.enabled = false;
        thirdPersonCamera.enabled = true;
    }
    // Update is called once per frame

}
