using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float xForce = 10.0f;
    public float zForce = 10.0f;
    public float yForce = 500.0f;
    public float sprintMultiplier = 4f;
    public Transform cameraTransform;
    public CinemachineFreeLook cinemachine;
    private bool _isSprinting = false;
    private Rigidbody _rigidbody;
    private bool _canJump = true;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _initialFov = cinemachine.m_Lens.FieldOfView;
        _targetFov = cinemachine.m_Lens.FieldOfView;
    }

    public float maxFov = 90f;
    public float minFov = 10f;
    public float zoomSensitivity = 10f;

    [SerializeField]
    private float _initialFov;
    [SerializeField]
    private float _targetFov;
    void Update()
    {
        var axis = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(axis) > 0.05f)
        {
            
            _initialFov = cinemachine.m_Lens.FieldOfView;
            _targetFov = cinemachine.m_Lens.FieldOfView - (zoomSensitivity * axis);
            _targetFov = Mathf.Clamp(_targetFov, minFov, maxFov);
        }

        var delta = Time.deltaTime * (_targetFov - cinemachine.m_Lens.FieldOfView);
        cinemachine.m_Lens.FieldOfView += delta;
        var forward = cameraTransform.forward;
        var right = cameraTransform.right;
        float x = 0.0f;
        if (Input.GetKey(KeyCode.A) && !_isSprinting)
        {
            x = x - xForce;
        }

        if (Input.GetKey(KeyCode.D) && !_isSprinting)
        {
            x = x + xForce;
        }

        float z = 0.0f;
        if (Input.GetKey(KeyCode.S) && !_isSprinting)
        {
            z = z - zForce;
        }

        if (Input.GetKey(KeyCode.W))
        {
            z = z + zForce;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isSprinting = false;
        }

        float y = 0.0f;
        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            y = yForce;
            _canJump = false;
            StartCoroutine(RechargeJump());
        }

        var desiredMoveDirection = forward * z + right * x;
        if (_isSprinting)
        {
            desiredMoveDirection *= sprintMultiplier;
        }
        _rigidbody.AddForce(desiredMoveDirection + new Vector3(0, y, 0));
    }


    private IEnumerator RechargeJump()
    {
        yield return new WaitForSeconds(1);
        _canJump = true;
    }
}