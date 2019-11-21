using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovementInput : MonoBehaviour
{

    [SerializeField] float velocity = 0;
    [SerializeField] float verticalVelocity = 0;
    [SerializeField] float gravity = 14f;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float speed = 5f;

    CharacterController characterController;
    Animator characterAnimator;

    [SerializeField] float inputX;
    [SerializeField] float inputZ;
    [SerializeField] Vector3 faceDesiredMoveDirection;
    [SerializeField] Vector3 goToDesiredMoveDirection;

    [SerializeField] float desiredRotationSpeed = 0.1f;

    [SerializeField] float allowPlayerRotation = 0.1f;
    [SerializeField] Camera playerCamera = null;

    /*
    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    [SerializeField] float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    [SerializeField] float VerticalAnimTime = 0.2f;
    [Range(0, 1f)]
    [SerializeField] float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    [SerializeField] float StopAnimTime = 0.15f;
    */
    

    private Vector3 moveVector;
    private Vector3 jumpVector;

    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        CheckGrounded();
        PlayerMove();
        /*
        if (characterController.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 moveVector = Vector3.zero * speed;
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y = verticalVelocity;
        moveVector.z = Input.GetAxis("Vertical") * speed;
        characterController.Move(moveVector * Time.deltaTime);
        */
    }

    void PlayerMove()
    {
        TakeInMovementInput();
        Jump();
    }

    void CheckGrounded()
    {
        if (characterController.isGrounded)
        {
            gravity -= 0;
        }
        else
        {
            gravity -= 2;
        }
        moveVector = new Vector3(0, gravity, 0);
        characterController.Move(moveVector);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (characterController.isGrounded)
            {
                Debug.Log("Jump");
                goToDesiredMoveDirection.y = jumpForce;
                //characterController.Move(goToDesiredMoveDirection * Time.deltaTime * velocity);
            }

        }
    }

    void PlayerMovementAndRotation()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        faceDesiredMoveDirection = forward * inputZ + right * inputX;
        goToDesiredMoveDirection = forward * inputZ + right * inputX;
        faceDesiredMoveDirection.y = 0;
        
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(faceDesiredMoveDirection), desiredRotationSpeed);
        characterController.Move(goToDesiredMoveDirection * Time.deltaTime * velocity);
    }

    void TakeInMovementInput()
    {
        //Calculate Input Vectors
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        //anim.SetFloat ("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
        //anim.SetFloat ("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);

        //Calculate the Input Magnitude
        speed = new Vector2(inputX, inputZ).sqrMagnitude;

        // move the player
        if (speed > allowPlayerRotation)
        {
            //characterAnimator.SetFloat("Blend", Speed, StartAnimTime, Time.deltaTime);
            characterAnimator.SetInteger("Condition", 1);
            PlayerMovementAndRotation();
        }
        else if (speed < allowPlayerRotation)
        {
            characterAnimator.SetInteger("Condition", 0);
            //characterAnimator.SetFloat("Blend", Speed, StopAnimTime, Time.deltaTime);
        }
    }
}
