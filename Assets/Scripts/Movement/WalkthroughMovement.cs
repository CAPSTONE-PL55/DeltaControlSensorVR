using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalkthroughMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    public AudioSource footstep1;
    public AudioSource footstep2;
    public AudioSource footstep3;
    public AudioSource footstep4;
    public AudioSource jump;
    public AudioSource land;

    float horizontalInput;
    float verticalInput;
    float footstepInterval = 0.5f;
    private int footstep;

    Vector3 moveDirection;

    Rigidbody rb;

    [HideInInspector] public TextMeshProUGUI text_speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        footstep = 1;

        readyToJump = true;
    }

    private void Update()
    {
        // ground check
        bool wasGrounded = grounded;
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // Play audio on landing
        if (grounded && !wasGrounded) {
            footstepInterval = Time.time + 0.5f;
            land.Play();
        }

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded) {
            if (Time.time >= footstepInterval && (moveDirection != new Vector3(0,0,0)))
            {
                PlayWalkAudio();

                // Reset the time for the next footstep sound
                footstepInterval = Time.time + 0.5f;
            }
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
            

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        //text_speed.SetText("Speed: " + flatVel.magnitude);
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        footstepInterval = Time.time + 0.5f;
        jump.Play();
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void PlayWalkAudio()
    {
        switch (footstep)
        {
            case 1: 
                footstep1.Play();
                footstep ++;
                break;
            case 2: 
                footstep2.Play();
                footstep ++;
                break;
            case 3: 
                footstep3.Play();
                footstep ++;
                break;  
            default:
                footstep4.Play();
                footstep = 1;
                break;
        }
    }
}