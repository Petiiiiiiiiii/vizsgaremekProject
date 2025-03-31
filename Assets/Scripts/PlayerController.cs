using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed;
    public float runningSpeed;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed;
    public const float baseLookSpeed = 2f;
    public float lookXLimit = 90.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    Animator animator;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        walkingSpeed = 5f;
        runningSpeed = 8f;

        lookSpeed = 2f;

        if (PlayerPrefs.GetString("movement_boost") == "unlocked")
        {
            walkingSpeed *= 1.1f;
            runningSpeed *= 1.1f;
        }

        if (PlayerPrefs.HasKey("sensitivity")) lookSpeed = baseLookSpeed * PlayerPrefs.GetFloat("sensitivity");
    }

    public void SetSensitivity() 
    {
        if (PlayerPrefs.HasKey("sensitivity")) lookSpeed = baseLookSpeed * PlayerPrefs.GetFloat("sensitivity");
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && verticalInput > 0;

        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * verticalInput : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * horizontalInput : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        float speed = new Vector3(curSpeedX, 0, curSpeedY).magnitude;
        animator.SetFloat("Speed", speed);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
            animator.SetTrigger("JumpOnce");
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            //rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            //rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            //playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            //transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

            // Kamera elõre mozgatása, ha eléri a 20°-os dõlésszöget
            if (rotationX >= 0)
            {
                float t = Mathf.InverseLerp(0, 90, rotationX); // Interpolációs érték 0 és 1 között
                float newZ = Mathf.Lerp(0, 0.54f, t);

                playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, 1.8f, newZ);
            }
            else
            {
                playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, playerCamera.transform.localPosition.y, 1.544953e-06f);
            }
        }
    }
}
