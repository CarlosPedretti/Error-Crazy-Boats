using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    //input fields
    //private PlayerInputActions playerActionsAsset;
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;

    //movement fields
    private Rigidbody rb;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera;
    private Animator animator;


    float verticalInput;
    float movementFactor;
    float horizontalInput;
    float steerFactor;
    float clampedSpeed;
    float clampedSteerSpeed;

    public float speed = 1.0f;
    public float steerSpeed = 1.0f;
    public float movementThresold = 10.0f;
    public float minSpeedLimit = 1f;
    public float maxSpeedLimit = 3f;
    public float minSpeedSteerLimit = 2f;
    public float maxSpeedSteerLimit = 3f;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();

        //playerActionsAsset = new PlayerInputActions();
        inputAsset = this.GetComponentInChildren<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
    }

    private void OnEnable()
    {
        //playerActionsAsset.Player.Jump.started += DoJump;
        //playerActionsAsset.Player.Attack.started += DoAttack;
        //move = playerActionsAsset.Player.Move;
        move = player.FindAction("Move");

    }

    private void OnDisable()
    {
        //playerActionsAsset.Player.Jump.started -= DoJump;
        //playerActionsAsset.Player.Attack.started -= DoAttack;
        player.Disable();

    }

    private void FixedUpdate()
    {
        verticalInput = move.ReadValue<float>();
        horizontalInput += move.ReadValue<float>();


        verticalInput = Input.GetAxis("Vertical");
        clampedSpeed = Mathf.Clamp(speed, minSpeedLimit, maxSpeedLimit);
        movementFactor = Mathf.Lerp(movementFactor, verticalInput, Time.deltaTime / movementThresold);
        transform.Translate(0.0f, 0.0f, movementFactor * clampedSpeed);

        horizontalInput = Input.GetAxis("Horizontal");
        clampedSteerSpeed = Mathf.Clamp(steerSpeed, minSpeedSteerLimit, maxSpeedSteerLimit);
        steerFactor = Mathf.Lerp(steerFactor, horizontalInput * verticalInput, Time.deltaTime / movementThresold);
        transform.Rotate(0.0f, steerFactor * clampedSteerSpeed, 0.0f);


    }




}