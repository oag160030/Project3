using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FPSInput : MonoBehaviour
{
    // Contains all functionality related to detecting input and sending it to the Player Controller for processing
    [SerializeField] bool _invertVertical = false;

    public event Action<Vector3> MoveInput = delegate { };
    public event Action<Vector3> RotateInput = delegate { };
    public event Action JumpInput = delegate { };
    public event Action ShootInput = delegate { };

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        DetectMoveInput();
        DetectRotateInput();
        DetectJumpInput();
        DetectShootInput();
    }

    void DetectMoveInput()
    {
        // process input as a 0 or 1 value, if we have it
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        // if we have either Horizontal or Vertical Input
        if(xInput != 0 || yInput != 0)
        {
            anim.SetInteger("Condition", 1);
            // convert to local directions, based on player orientation
            Vector3 _horizontalMovement = transform.right * xInput;
            Vector3 _forwardMovement = transform.forward * yInput;
            // combine movements into a single vector
            Vector3 movement = (_horizontalMovement + _forwardMovement).normalized;
            // notify that we have moved 
            MoveInput?.Invoke(movement);
        }
        else
        {
            anim.SetInteger("Condition", 0);
        }
    }

    void DetectRotateInput()
    {
        // get out inputs from input controller
        float xInput = Input.GetAxisRaw("Mouse X");
        float yInput = Input.GetAxisRaw("Mouse Y");

        if(xInput != 0 || yInput != 0)
        {
            // account for inverted camera movement, if specified 
            if(_invertVertical == true)
            {
                yInput = -yInput;
            }
            // move left/right should be y axis, up/down x axis
            Vector3 rotation = new Vector3(yInput, xInput, 0);
            // notify that we have rotated
            RotateInput?.Invoke(rotation);
        }
    }

    void DetectJumpInput()
    {
        // Spacebay press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetInteger("Condition", 2);
            JumpInput?.Invoke();
        }
    }

    void DetectShootInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // left mouse click
            //Debug.Log("Shoot!");
            ShootInput?.Invoke();
        }
    }
}
