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
    [SerializeField] private GameObject machete;
    [SerializeField] private Animator macheteAnimator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private GameObject headlight;

    private SpawnAndRespawn spawnManager;
    private MeshRenderer playerMesh;
    Camera cam;
    float mouseSensitivity = 2f;
    float vertRotationStore;
    float gravityMod = 5f;
    float activeMoveSpeed;
    [SerializeField] private float walkSpeed = 16f;
    [SerializeField] private float runSpeed = 25f;

    bool isGrounded;

    Vector3 moveDirection, movement;
    Vector2 mouseInput;
    private float macheteRange = 3.2f;

    // Start is called before the first frame update
    void Awake()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnAndRespawn>();
        spawnManager.spawnText.gameObject.SetActive(false);
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        machete.transform.localPosition = eyes.transform.position - new Vector3(0f, 0f, -2f);
        playerMesh = GetComponent<MeshRenderer>();
        playerMesh.enabled = false;
        eyes.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessPlayerMovement();
        ProcessPlayerView();
        ProcessMacheteSwing();
        ManageMouseCursor();
        ManageHeadlight();
    }

    private void ManageHeadlight()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (headlight.activeInHierarchy)
            {
                headlight.SetActive(false);
            }
            else
            {
                headlight.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Makes mouse cursor disappear when user is playing game.
    /// Press Escape to reactive mouse cursor.
    /// </summary>
    private static void ManageMouseCursor()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Cursor.lockState == CursorLockMode.None)
        {
            if (Input.GetMouseButton(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    /// <summary>
    /// Animates machete as well as checks to see if machete has hit something and what it has hit.
    /// </summary>
    private void ProcessMacheteSwing()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            macheteAnimator.SetTrigger("swinging");
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            ray.origin = cam.transform.position;
            bool hitObject = Physics.Raycast(ray, macheteRange);
            if (hitObject && Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"You hit a {hit.collider.name}");
            }
        }

    }
    /// <summary>
    /// Updates camera
    /// </summary>
    private void LateUpdate()
    {
        cam.transform.position = eyes.position;
        cam.transform.rotation = eyes.rotation;
    }
    /// <summary>
    /// Handles player rotation
    /// </summary>
    private void ProcessPlayerView()
    {
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        vertRotationStore += mouseInput.y;
        vertRotationStore = Mathf.Clamp(vertRotationStore, -60f, 60f);

        eyes.rotation = Quaternion.Euler(-vertRotationStore, eyes.rotation.eulerAngles.y, eyes.rotation.eulerAngles.z);
        
    }
    /// <summary>
    /// Handles player movement
    /// </summary>
    private void ProcessPlayerMovement()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            if (audioSource.clip == audioClips[0] || !isGrounded)
            {
                audioSource.enabled = false;
            }
            if (isGrounded)
            {
                activeMoveSpeed = runSpeed;
                audioSource.clip = audioClips[1];
                audioSource.enabled = true;
            }
            
    } else
        {
            if (audioSource.clip == audioClips[1] || !isGrounded)
            {
                audioSource.enabled = false;
            }
            if (isGrounded)
            {
                activeMoveSpeed = walkSpeed;
                audioSource.clip = audioClips[0];
                audioSource.enabled = true;
            }
        }
        if (moveDirection == Vector3.zero)
        {
            audioSource.enabled = false;
        }
        float yVelocity = movement.y;
        movement = ((transform.right * moveDirection.x) + (transform.forward * moveDirection.z)).normalized * activeMoveSpeed;
        movement.y = yVelocity;

        if (charController.isGrounded)
        {
            movement.y = 0f;
        }

        //Jump
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.5f, groundLayers);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            movement.y = jumpImpulse;
        }
        movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;
        charController.Move(movement * Time.deltaTime);
    }
}
