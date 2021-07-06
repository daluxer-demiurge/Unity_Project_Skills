using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Controller : MonoBehaviour
{
    private float _xRotation;
    private float _yRotation;
    private float _xRotationCurrent;
    private float _yRotationCurrent;
    [SerializeField] private Camera _player;
    public GameObject playerGameObject;
    [SerializeField] private float _camSensitivity = 5f;
    private float _smoothTime = 0.1f;
    private float _currentVelosityX;
    private float _currentVelosityY;
    [SerializeField] private float _mainSpeed;
    [SerializeField] private float _shiftSpeed;
    public float _currentSpeed;
    public float _x_Move;
    public float _z_Move;
    CharacterController player;
    Vector3 moveDirection;

    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    void Update()
    {
        MouseMove();
        Move();
    }

    void MouseMove()
    {
        _xRotation += Input.GetAxis("Mouse X") * _camSensitivity;
        _yRotation += Input.GetAxis("Mouse Y") * _camSensitivity;
        _yRotation = Mathf.Clamp(_yRotation, -90, 90);

        _xRotationCurrent = Mathf.SmoothDamp(_xRotationCurrent, _xRotation, ref _currentVelosityX, _smoothTime);
        _yRotationCurrent = Mathf.SmoothDamp(_yRotationCurrent, _yRotation, ref _currentVelosityY, _smoothTime);
        _player.transform.rotation = Quaternion.Euler(-_yRotationCurrent, _xRotationCurrent, 0f);
        playerGameObject.transform.rotation = Quaternion.Euler(0f, _xRotationCurrent, 0f);
    }

    void Move()
    {
        _x_Move = Input.GetAxis("Horizontal");
        _z_Move = Input.GetAxis("Vertical");
        if (player.isGrounded)
        {
            moveDirection = new Vector3(_x_Move, 0f, _z_Move);
            moveDirection = transform.TransformDirection(moveDirection);
        }
        moveDirection.y -= 1;
        if (Input.GetKey(KeyCode.LeftShift)) _currentSpeed = _shiftSpeed;
        else _currentSpeed = _mainSpeed;
        player.Move(moveDirection * _currentSpeed);
    }
}

