using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    
    private Rigidbody rb;
    private bool isGrounded;

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
    }

    private void Update()
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal"); // A & D
        float vertical = Input.GetAxis("Vertical");     // W & S

        // Calculate movement vector
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed;
        
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
}