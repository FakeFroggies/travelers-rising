using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float gravity = 10f;
    public float jumpPower = 10f;
    public float defaultHeight = 2f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    private LookDirection direction = new LookDirection();
    //Joystick
    [SerializeField] private InputActionReference moveActions;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Joystick
        Vector2 move = moveActions.action.ReadValue<Vector2>();
        Vector3 correctMove = new(move.x, 0, move.y);
        correctMove = correctMove.x * Camera.main.transform.right + correctMove.z * Camera.main.transform.forward;
        correctMove.y = 0;
        characterController.Move(runSpeed * Time.deltaTime * correctMove);

        //Move direction
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        direction = GetLookingPosition();
        Debug.Log(direction.right + ":" + direction.forward);
        //Debug.Log(Input.GetAxis("Vertical") + "|" + Input.GetAxis("Horizontal"));

        //Get speed
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical");
        float curSpeedY = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //Jumping
        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        //Gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        //Move
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private LookDirection GetLookingPosition()
    {
        //Looking direction
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity))
        {
            // The Raycast hit something, return with the position.
            var point = hitInfo.point;
            point.x -= transform.position.x;
            point.z -= transform.position.z;
            //Debug.Log(point);
            direction.right = point.x - point.z;
            if (point.x < 0 && point.z < 0)
                direction.forward = point.x / Math.Abs(point.z);
            else
                direction.forward = point.x / point.z;
            //if (point.x > 0 && point.z > 0)
            //{
            //    //Debug.Log("Up");
            //    return 2;
            //}
            //else if (point.x < 0 && point.z < 0)
            //{
            //    //Debug.Log("Down");
            //    return -2;
            //}
            //else
            //    switch (point.x - point.z)
            //    {
            //        case > 0:
            //            //Debug.Log("Right");
            //            return 1;
            //        case < 0:
            //            //Debug.Log("Left");
            //            return -1;

            //    }
        }
        return direction;
    }
    class LookDirection
    {
        public float right, forward;
    }
}