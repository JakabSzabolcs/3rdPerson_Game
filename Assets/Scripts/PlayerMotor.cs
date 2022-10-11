using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour

{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletTransform;
    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private float WeaponAccuracy = 25f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private PlayerInput playerinput;
    private bool groundedPlayer;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction lookStatic;
    private Transform cameraTransform;

    private InputAction shootAction;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerinput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        moveAction = playerinput.actions["Move"];
        jumpAction = playerinput.actions["Jump"];
        lookStatic = playerinput.actions["LookStatic"];
        shootAction = playerinput.actions["Shoot"];

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        shootAction.performed += _ => Shootgun();
    }

    private void OnDisable()
    {
        shootAction.performed -= _ => Shootgun();
    }

    private void Shootgun()
    {
        
        RaycastHit hit;
        GameObject Bullet = GameObject.Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity, bulletParent);
        BulletController bulletController = Bullet.GetComponent<BulletController>();
        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {
            SoundManagerScript.PlaySound("shoot");
            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            SoundManagerScript.PlaySound("shoot");
            bulletController.target = cameraTransform.position + cameraTransform.forward * WeaponAccuracy;
            bulletController.hit = false;
        }
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);


        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


        // rotate towards camera dir

            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime*10);

    }
}