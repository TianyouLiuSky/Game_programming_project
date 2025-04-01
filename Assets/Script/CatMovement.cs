using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float walkSpeed = 0.5f;
    public float chaseSpeed = 2f;
    public float rotationSpeed = 5f;
    public float randomWalkRadius = 3f;
    public float detectionRadius = 2f;
    public float sittingDistance = 0.1f;
    public float sittingDuration = 15f;
    public float jumpHeight = 0.2f;
    public float jumpDuration = 0.5f;


    private Animator animator;
    private string currentAnimState;
    private const string ANIM_IDLE = "idle";
    private const string ANIM_WALK = "walk";
    private const string ANIM_SIT = "sit";
    private const string ANIM_SITTING = "sitting";
    private const string ANIM_MIAU = "miau";

    // Cat states
    private enum CatState { Idle, RandomWalk, ChasePlayer, SitOnPlayer, WalkAway }
    private CatState currentState = CatState.Idle;
    
    // Movement
    private Vector3 targetPosition;
    private Transform playerTransform;
    private bool isSittingOnPlayer = false;
    private float stateTimer = 0f;
    private float idleTimer = 0f;
    private float idleDuration = 5f;
    private bool isJumping = false;
    private float animRefreshTimer = 0f;
    private Rigidbody rb;
    private RobotMovement playerMovement;
    private float obstructionTimer = 0f;
    private float obstructionDuration = 1f;
    private float chaseCooldownTimer = 0f;
    private float chaseCooldownDuration = 5f;
    private bool isOnChaseCooldown = false;

    void Start() {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = playerTransform.GetComponent<RobotMovement>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        // start with idle
        currentAnimState = ANIM_IDLE;
        SetRandomIdleDuration();
    }

    void Update() {
        if (isOnChaseCooldown) {
            chaseCooldownTimer += Time.deltaTime;
            if (chaseCooldownTimer >= chaseCooldownDuration) {
                isOnChaseCooldown = false;
            }
        }

        switch (currentState)
        {
            case CatState.Idle:
                UpdateIdleState();
                break;
            case CatState.RandomWalk:
                UpdateRandomWalkState();
                break;
            case CatState.ChasePlayer:
                UpdateChasePlayerState();
                break;
            case CatState.SitOnPlayer:
                UpdateSitOnPlayerState();
                break;
            case CatState.WalkAway:
                UpdateWalkAwayState();
                break;
        }

        if (!isOnChaseCooldown && currentState != CatState.SitOnPlayer && currentState != CatState.WalkAway)
        {
            DetectPlayer();
        }
    }

    void UpdateIdleState() {
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            SetRandomDestination();
            currentState = CatState.RandomWalk;
            ChangeAnimationState(ANIM_WALK);
            idleTimer = 0f;
        }
    }

    void UpdateRandomWalkState() {
        MoveTowardsTarget(walkSpeed);

        if (currentAnimState != ANIM_WALK) {
            ChangeAnimationState(ANIM_WALK);
        }
    
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // Go back to idle
            currentState = CatState.Idle;
            ChangeAnimationState(ANIM_IDLE);
            SetRandomIdleDuration();
        }
    }

void OnCollisionEnter(Collision collision) {
    if (currentState == CatState.ChasePlayer && collision.gameObject.CompareTag("Player")) {
        return;
    }
    
    if (currentState == CatState.RandomWalk) {
        SetRandomDestination();
    }
}  

void UpdateChasePlayerState(){
    // Check if player has moved out of detection range
    float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

    // Stop chasing if player move away
    if (distanceToPlayer > detectionRadius * 1.5f) {
        currentState = CatState.Idle;
        ChangeAnimationState(ANIM_IDLE);
        SetRandomIdleDuration();
        return;
    }

    // Update target position to current player position
    targetPosition = playerTransform.position;
    ChangeAnimationState(ANIM_WALK);
    MoveTowardsTarget(chaseSpeed);
    
    // If we're close enough to sit on the player
    if (Vector3.Distance(transform.position, playerTransform.position) < sittingDistance)
    {
        // Start jumping onto player
        currentState = CatState.SitOnPlayer;
        stateTimer = 0f;
        isSittingOnPlayer = false;
        isJumping = true;
        rb.isKinematic = true;
        StartCoroutine(JumpOntoPlayer());
        
        // Play miau sound animation randomly
        if (Random.Range(0, 3) == 0)
        {
            StartCoroutine(PlayMiauAnimation());
        }
    }
}

IEnumerator JumpOntoPlayer() {
    Vector3 startPosition = transform.position;
    Vector3 targetPosition = playerTransform.position + Vector3.up * 0.1f;
    float elapsedTime = 0f;
    
    // calculate the arc
    while (elapsedTime < jumpDuration)
    {
        float progress = elapsedTime / jumpDuration;
        float heightOffset = Mathf.Sin(progress * Mathf.PI) * jumpHeight;
        Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, progress);
        currentPos.y += heightOffset;
        transform.position = currentPos;
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // currentAnimState = ANIM_SIT;
    // transform.position = targetPosition;
    // isSittingOnPlayer = true;
    // isJumping = false;
    
    // Check if player is still close enough after jump completes
    float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
    if (distanceToPlayer <= sittingDistance)
    {
        currentAnimState = ANIM_SIT;
        transform.position = targetPosition;
        isSittingOnPlayer = true;
        isJumping = false;
    }
    else {
        rb.isKinematic = false;
        isSittingOnPlayer = false;
        
        // Decide next state based on player distance
        if (distanceToPlayer <= detectionRadius)
        {
            currentState = CatState.ChasePlayer;
            ChangeAnimationState(ANIM_WALK);
        }
        else
        {
            currentState = CatState.RandomWalk;
            ChangeAnimationState(ANIM_WALK);
            SetRandomDestination();
        }
        isJumping = false;
    }
}

    void UpdateSitOnPlayerState()
    {
        if (!isJumping) {
            stateTimer += Time.deltaTime;
            // Stay on the player
            transform.position = playerTransform.position + Vector3.up * 0.05f;

            if (stateTimer <= 0.1f && playerMovement != null)
            {
                playerMovement.SlowDownFromCat();
            }

            if (stateTimer > 1f && currentAnimState == ANIM_SIT)
            {
                ChangeAnimationState(ANIM_SITTING);
            }
            
            // After sitting duration, get up and walk away
            if (stateTimer >= sittingDuration)
            {
                rb.isKinematic = false;
                isSittingOnPlayer = false;
                SetWalkAwayDestination();
                currentState = CatState.WalkAway;
                ChangeAnimationState(ANIM_WALK);
                isOnChaseCooldown = true;
                chaseCooldownTimer = 0f;
            }
        }
    }

    void UpdateWalkAwayState()
    {
        MoveTowardsTarget(walkSpeed);
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            // Go back to idle
            currentState = CatState.Idle;
            ChangeAnimationState(ANIM_IDLE);
            SetRandomIdleDuration();
        }
    }

    void DetectPlayer()
    {
        if (currentState != CatState.ChasePlayer)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            
            if (distanceToPlayer < detectionRadius)
            {
                currentState = CatState.ChasePlayer;
                ChangeAnimationState(ANIM_WALK);
            }
        }
    }

    void MoveTowardsTarget(float speed) {
        Vector3 direction = (targetPosition - transform.position).normalized;
        
        // Only move on the XZ plane
        direction.y = 0;
        
        if (direction != Vector3.zero)
        {
            // Rotate towards the target
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            rb.velocity = transform.forward * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void SetRandomDestination()
    {
        // Choose a random point within the radius
        Vector2 randomCircle = Random.insideUnitCircle * randomWalkRadius;
        targetPosition = transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
        targetPosition.y = transform.position.y;
    }

    void SetWalkAwayDestination()
    {
        Vector3 awayDirection = (transform.position - playerTransform.position).normalized;
        targetPosition = transform.position + awayDirection * randomWalkRadius * 2f;
        targetPosition.y = transform.position.y;
    }

    void SetRandomIdleDuration() {
        idleDuration = Random.Range(2f, 5f);
        idleTimer = 0f;
    }

    IEnumerator PlayMiauAnimation() {
        string previousAnim = currentAnimState;
        // Play miau animation
        ChangeAnimationState(ANIM_MIAU);
        yield return new WaitForSeconds(1f);
        ChangeAnimationState(previousAnim);
    }

    void ChangeAnimationState(string newState) {
        if (currentAnimState == newState) return;
        animator.CrossFade(newState, 0.2f);
        currentAnimState = newState;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, randomWalkRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sittingDistance);
    }
}