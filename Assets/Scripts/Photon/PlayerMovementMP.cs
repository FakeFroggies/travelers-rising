using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementMP : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float gravity = 10f;
    public float jumpPower = 10f;
    public float defaultHeight = 2f;
    PhotonView view;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    private Vector3 direction;
    private float minX, maxX, minZ, maxZ;
    //Joystick
    [SerializeField] private InputActionReference moveActions;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine)
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
            direction.x *= -1;

            //Get range for up-down speed
            if (Input.GetAxis("Vertical") < 0)
            {
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
            float curSpeedX = (isRunning ? runSpeed : walkSpeed) * Math.Abs(Input.GetAxis("Vertical")) * Math.Clamp(Input.GetAxis("Vertical") - direction.x, minX, maxX);
            float curSpeedZ = (isRunning ? runSpeed : walkSpeed) * Math.Abs(Input.GetAxis("Horizontal")) * Math.Clamp(Input.GetAxis("Horizontal") - direction.z, minZ, maxZ);
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
    }

    private Vector3 GetLookingPosition()
    {
        //Looking direction
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity))
        {
            // The Raycast hit something, return with the position.
            var point = hitInfo.point;
            point -= transform.position;
            return Quaternion.Euler(new Vector3(0, 45, 0)) * point.normalized;
        }
        return new(0, 0, 0);
    }
}