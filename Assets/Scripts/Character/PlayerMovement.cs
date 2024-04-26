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
    private Vector3 direction;
    private float minX, maxX, minZ, maxZ;
    private float speedX, speedZ;
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

        //Looking direction
        direction = GetLookingPosition();
        //Get range for up-down speed
        float addX = 0.75f, addZ = 0.75f;
        if (Input.GetAxis("Vertical") < 0)
        {
            addX = -addX;
            minX = -1f;
            maxX = -0.5f;
        }
        else
        {
            minX = 0.5f;
            maxX = 1f;
        }
        //Get range for left-right speed
        if (Input.GetAxis("Horizontal") < 0)
        {
            addZ = -addZ;
            minZ = -1f;
            maxZ = -0.5f;
        }
        else
        {
            minZ = 0.5f;
            maxZ = 1f;
        }
        //Get speed
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        speedX = Mathf.Clamp(direction.x + addX, minX, maxX);
        speedZ = Mathf.Clamp(-direction.z + addZ, minZ, maxZ);
        float curSpeedX = (isRunning ? runSpeed : walkSpeed) * Mathf.Abs(Input.GetAxis("Vertical")) * speedX;
        float curSpeedZ = (isRunning ? runSpeed : walkSpeed) * Mathf.Abs(Input.GetAxis("Horizontal")) * speedZ;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedZ);

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

    private Vector3 GetLookingPosition()
    {
        //Looking direction
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, LayerMask.GetMask("Terrain")))
        {
            // The Raycast hit something, return with the position.
            var point = hitInfo.point;
            point -= transform.position;
            return Quaternion.Euler(new Vector3(0, 60, 0)) * point.normalized;
        }
        return new(0, 0, 0);
    }
}