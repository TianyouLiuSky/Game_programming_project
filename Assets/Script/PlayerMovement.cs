// PlayerMovement.cs Tianyou Liu, Nian Gao, Alina Pan
using UnityEngine;
using System.Collections;

public class RobotMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float slowedMoveSpeed = 2f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float slowdownDuration = 3f;

    private Rigidbody rb;
    private bool isGrounded;
    private float currentMoveSpeed;
    private bool isSlowed = false;

    private void Start()
    {
        // Get or add Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        // Configure Rigidbody for a robot vacuum
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Prevent tipping over
        rb.mass = 1f;
        rb.drag = 1f;

        //initialize movespeed
        currentMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal"); // A & D
        float vertical = Input.GetAxis("Vertical");     // W & S

        // Calculate movement vector
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * currentMoveSpeed;
        
        // Apply movement to rigidbody
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
}