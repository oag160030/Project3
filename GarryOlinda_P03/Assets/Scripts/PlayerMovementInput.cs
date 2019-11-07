using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementInput : MonoBehaviour
{
    [SerializeField] float speed = 4;
    [SerializeField] float rotateSpeed = 80;
    [SerializeField] float rotation = 0;
    [SerializeField] float gravity = 8;

    Vector3 moveDirection = Vector3.zero;

    CharacterController controller;
    Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection = new Vector3(0, 0, 1);
                moveDirection *= speed;
                moveDirection = transform.TransformDirection(moveDirection);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                moveDirection = new Vector3(0, 0, 0);
            }
        }
        rotation += Input.GetAxis("horizontal") * rotateSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotation, 0);

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
