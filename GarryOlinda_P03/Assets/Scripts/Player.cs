using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float velocity = 0;
    [SerializeField] float verticalVelocity = 0;
    [SerializeField] float gravity = 14f;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float speed = 5f;
    [SerializeField] float desiredRotationSpeed = 0.1f;
    [SerializeField] Camera playerCamera = null;

    [SerializeField] float inputX;
    [SerializeField] float inputZ;

    CharacterController characterController;
    Animator characterAnimator;
    Vector3 moveVector;
    Vector3 lookDirection;
    Vector3 forward;
    Vector3 right;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    Vector3 moveDirection;

    int jumps = 0;

    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        ThrowHat();
        PlayerMovement();        
    }

    void PlayerMovement()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        speed = new Vector2(inputX, inputZ).sqrMagnitude;
        // move the player
        if (speed > 0.1)
        {
            characterAnimator.SetFloat("Blend", speed, StartAnimTime, Time.deltaTime);
            if (characterController.isGrounded)
            {
                verticalVelocity = -gravity * Time.deltaTime;
            }
            else
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }

            LookDirection();

            moveVector = lookDirection;
            moveVector.y = verticalVelocity;

            characterController.Move(moveVector * Time.deltaTime * velocity);
        }
        if (speed < 0.1)
        {
            characterAnimator.SetFloat("Blend", speed, StopAnimTime, Time.deltaTime);
        }

        if (characterController.isGrounded)
        {
            jumps = 0; 
            if (Input.GetKeyDown(KeyCode.Space) && jumps < 1)
            {
                Jump();
                characterAnimator.SetBool("Jumping", false);
                jumps++;
            }
            
        }
    }

    public void LookDirection()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        lookDirection = Vector3.zero;

        forward = playerCamera.transform.forward;
        right = playerCamera.transform.right;

        lookDirection = forward * inputZ + right * inputX; ;
        lookDirection.y = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(lookDirection), desiredRotationSpeed);
    }

    void ThrowHat()
    {
        if (Input.GetMouseButtonDown(0))
        {
            characterAnimator.SetTrigger("Swing");
            StartCoroutine(ThrowCoroutine());
        }
        characterAnimator.SetBool("Throwing", false);
    }

    public void Jump()
    {
        Debug.Log("Jump");
        characterAnimator.SetTrigger("Jump");
        StartCoroutine(JumpCoroutine());
        verticalVelocity = jumpForce;
        moveVector = forward * inputZ + right * inputX;

        moveVector.y = verticalVelocity * 5;

        LookDirection();

        characterController.Move(moveVector * Time.deltaTime * velocity);


    }

    IEnumerator ThrowCoroutine()
    {
        yield return new WaitForEndOfFrame();
        if (!characterAnimator.GetBool("Throwing"))
        {
            characterAnimator.SetBool("Throwing", true);
        }
            
    }

    IEnumerator JumpCoroutine()
    {
        yield return new WaitForEndOfFrame();
        if (!characterAnimator.GetBool("Jumping"))
        {
            characterAnimator.SetBool("Jumping", true);
        }
    }
}
