// PlayerMovement.cs Tianyou Liu, Nian Gao, Alina Pan
using UnityEngine;
using System.Collections;

public class RobotMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float slowedMoveSpeed = 2f;
    [SerializeField] private float slowdownDuration = 3f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private AudioClip jumpSound;

    [Header("Camera Reference")]
    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;
    private bool isGrounded;
    private float currentMoveSpeed;
    private bool isSlowed = false;
    private AudioSource audioSource;

    private void Start()
    {
        // Get or add Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Get AudioSource component
        audioSource = GetComponent<AudioSource>();
        
        // Configure Rigidbody for a robot vacuum
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Prevent tipping over
        rb.mass = 1f;
        rb.drag = 1f;

        // Initialize movespeed
        currentMoveSpeed = moveSpeed;

        // Find camera if not assigned
        if (cameraTransform == null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                cameraTransform = mainCamera.transform;
            }
        }
    }

    private void Update()
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal"); // A & D
        float vertical = Input.GetAxis("Vertical");     // W & S

        // Get camera forward and right vectors
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Project vectors onto the horizontal plane (y = 0)
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Calculate movement direction relative to camera orientation
        Vector3 movement = (right * horizontal + forward * vertical) * currentMoveSpeed;
        
        // Apply movement to rigidbody
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Make the player object face the movement direction
        if (movement.magnitude > 0.1f)
        {
            Quaternion movementRotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z).normalized);
            movementRotation *= Quaternion.Euler(-90, 0, 0);
            transform.rotation = movementRotation;
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }

    // Ground check
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.name.Contains("obstacle"))
        {
            // Apply slowdown effect
            if (!isSlowed)
            {
                StartCoroutine(SlowDownPlayer());
            }
        }
    }

    private IEnumerator SlowDownPlayer()
    {
        isSlowed = true;
        currentMoveSpeed = slowedMoveSpeed;
        yield return new WaitForSeconds(slowdownDuration);
        currentMoveSpeed = moveSpeed;
        isSlowed = false;
    }

    private IEnumerator CatSlowDownPlayer()
    {
        isSlowed = true;
        currentMoveSpeed = slowedMoveSpeed;
        yield return new WaitForSeconds(15f);
        currentMoveSpeed = moveSpeed;
        isSlowed = false;
    }

    public void SlowDownFromCat()
    {
        if (!isSlowed)
        {
            StartCoroutine(CatSlowDownPlayer());
        }
    }
}