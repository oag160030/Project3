using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FPSInput))]
[RequireComponent(typeof(FPSMotor))]
public class PlayerController : MonoBehaviour
{
    //The “Controller” script responsible for processing and passing information between the other scripts.
    FPSInput _input = null;
    FPSMotor _motor = null;

    [SerializeField] ParticleSystem _shootParticle = null;
    [SerializeField] float _moveSpeed = 0.1f;
    [SerializeField] float _turnSpeed = 6f;
    [SerializeField] float _jumpStrength = 10f;
    [SerializeField] float _sprintSpeed = 2f;
    //[SerializeField] float _fallSpeed = 0f;
    //[SerializeField] float _safeFallHeight = 0f;

    [SerializeField] float rayDistance = 10f;
    [SerializeField] float debugRayDuration = 1f;

    private void Awake()
    {
        _input = GetComponent<FPSInput>();
        _motor = GetComponent<FPSMotor>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //TODO unlock your cursor for menu navigation (pause), 
        //and then lock it again when you go back into play mode (unpause) 
        //if that’s something you’d like.
    }

    private void OnEnable()
    {
        _input.MoveInput += OnMove;
        //_input.SprintInput += OnSprint;
        _input.RotateInput += OnRotate;
        _input.JumpInput += OnJump;
        _input.ShootInput += OnShoot;
    }

    private void OnDisable()
    {
        _input.MoveInput -= OnMove;
        //_input.SprintInput -= OnSprint;
        _input.RotateInput -= OnRotate;
        _input.JumpInput -= OnJump;
        _input.ShootInput -= OnShoot;
    }

    void OnMove(Vector3 movement)
    {
        // incorporate out move speed
        //Debug.Log("Move: " + movement);
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _motor.Move(movement * _sprintSpeed);
            //Debug.Log("Sprint");
        }
        else
        {
            _motor.Move(movement * _moveSpeed);
        }
    }

    /*
    void OnSprint(Vector3 sprint)
    {
        _motor.Move(sprint * _sprintSpeed);
    }
    */

    void OnRotate(Vector3 rotation)
    {
        // camera looks vertical, body rotates left/right
        //Debug.Log("Rorate: " + rotation);
        _motor.Turn(rotation.y * _turnSpeed);
        _motor.Look(rotation.x * _turnSpeed);
    }

    void OnJump()
    {
        // apply out jump force to out moto
        //Debug.Log("Jump!");
        _motor.Jump(_jumpStrength);
    }

    void OnShoot()
    {
        Debug.Log("Shoot!");
        //_shootParticle.Emit(1);
        //DebugRay();
        //ShootRay();
    }

    void DebugRay()
    {
        Vector3 endPoint = transform.forward * rayDistance;
        Debug.DrawRay(transform.position, endPoint, Color.cyan, debugRayDuration);
    }

    void ShootRay()
    {
        if (Physics.Raycast(transform.position, transform.forward, rayDistance))
        {
            Debug.Log("Hit Something");
        }
        else
        {
            Debug.Log("Miss");
        }
    }
}
