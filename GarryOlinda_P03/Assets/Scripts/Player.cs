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

    [SerializeField] HatController hat = null;
    [SerializeField] float throwPower = 500;
    Rigidbody hatRB;


    Vector3 moveDirection;

    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        hatRB = hat.GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerMovement();
        //ThrowHat();
        
    }

    void PlayerMovement()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        speed = new Vector2(inputX, inputZ).sqrMagnitude;
        // move the player
        if (speed > 0.1)
        {
            characterAnimator.SetInteger("Condition", 1);
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
            characterAnimator.SetInteger("Condition", 0);
        }

        Jump();
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
        if (Input.GetKey(KeyCode.E))
        {
            //characterAnimator.SetInteger("Condition", 3);
            hatRB.isKinematic = false;
            hatRB.transform.parent = null;
            //hatRB.AddForce(transform.forward * throwPower, ForceMode.Impulse);
        }
    }

    void Jump()
    {
        if (characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Jump");
                characterAnimator.SetInteger("Condition", 2);
                verticalVelocity = jumpForce;
                moveVector = forward * inputZ + right * inputX;

                moveVector.y = verticalVelocity;

                LookDirection();

                characterController.Move(moveVector * Time.deltaTime * velocity);
            }
        }
        
    }
}
