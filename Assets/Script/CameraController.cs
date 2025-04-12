using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 80f;
    [SerializeField] private Transform targetToLookAt;
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineOrbitalTransposer orbitalTransposer;
    private float currentAngle = 0f;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0.5f, 1, -2);

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        // add an orbital transposer to the virtual camera
        orbitalTransposer = virtualCamera.AddCinemachineComponent<CinemachineOrbitalTransposer>();
        // set offset
        orbitalTransposer.m_FollowOffset = cameraOffset;
    }

    void Update()
    {
        // Rotate counterclockwise with Q
        if (Input.GetKey(KeyCode.Q))
        {
            currentAngle += rotationSpeed * Time.deltaTime;
        }
        
        // Rotate clockwise with E
        if (Input.GetKey(KeyCode.E))
        {
            currentAngle -= rotationSpeed * Time.deltaTime;
        }
        
        orbitalTransposer.m_XAxis.Value = currentAngle;
    }
}