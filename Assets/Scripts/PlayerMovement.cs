using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController charController;
    [SerializeField] private Transform eyes;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] float jumpImpulse = 5f;

    Camera cam;
    float mouseSensitivity = 5f;
    float vertRotationStore;
    float gravityMod = 2f;
    float activeMoveSpeed;
    private float walkSpeed = 5f;
    private float runSpeed = 8f;

    bool isGrounded;

    Vector3 moveDirection, movement;
    Vector2 mouseInput;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessPlayerMovement();
        ProcessPlayerView();
    }

    private void LateUpdate()
    {
        cam.transform.position = eyes.position;
        cam.transform.rotation = eyes.rotation;
    }

    private void ProcessPlayerView()
    {
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        vertRotationStore += mouseInput.y;
        vertRotationStore = Mathf.Clamp(vertRotationStore, -60f, 60f);

        eyes.rotation = Quaternion.Euler(-vertRotationStore, eyes.rotation.eulerAngles.y, eyes.rotation.eulerAngles.z);
    }

    private void ProcessPlayerMovement()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeMoveSpeed = runSpeed;
        } else
        {
            activeMoveSpeed = walkSpeed;
        }
        float yVelocity = movement.y;
        movement = ((transform.right * moveDirection.x) + (transform.forward * moveDirection.z)).normalized * activeMoveSpeed;
        movement.y = yVelocity;

        if (charController.isGrounded)
        {
            movement.y = 0f;
        }

        //Jump
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.2f, groundLayers);
        Debug.Log(isGrounded);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            movement.y = jumpImpulse;
        }
        movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;
        charController.Move(movement * Time.deltaTime);
    }
}
