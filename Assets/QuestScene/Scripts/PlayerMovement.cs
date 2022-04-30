using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //PLEASE COMMENT THIS CODE LMAO


    [Header("Buttons")]
    public KeyCode runButton;
    public KeyCode jumpButton;
    public KeyCode crouchButton;

    [Header("Player inputs")]
    public float movementSpeed;
    public float runSpeed;
    public float jumpHeight;
    float currentJumpHeight;
    public float crouchSpeed;
    public bool canDoubleJump;
    public float longJumpHeight;
    public float longJumpTime;
    public bool canLongJump;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isGrounded;

    [Header("Top Check")]
    public Transform topCheck;
    public LayerMask topLayer;
    public bool isTop;
    public bool headColliding;
    public bool crouchButtonIsHeld;
    public bool isCrouched;

    public CharacterController controller;
    public Vector3 movementDir;

    public bool isMoving;

    float longJumpTimer;
    float jumpYPos;

    [Header("Math")]
    Vector3 velocity;
    public float gravityConst = -9.81f;
    public float gravityMultiplayer;

    private Transform mainCam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentJumpHeight = jumpHeight;
        isMoving = false;
        mainCam = Camera.main.transform;
    }


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundLayer);
        isTop = Physics.CheckSphere(topCheck.position, 0.4f, topLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
            if (!canDoubleJump) canDoubleJump = true;
            // if (longJumpTimer > 0) longJumpTimer = 0;
        }
        else 
        {
            velocity.y += gravityConst * gravityMultiplayer * Time.deltaTime;
        }

        controller.Move(velocity*Time.deltaTime);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //movementDir = transform.right * x + transform.forward * z;
        Vector3 forward = mainCam.forward;
        Vector3 right = mainCam.right;
        forward.Normalize();
        right.Normalize();
        movementDir = forward * z + right * x;

        if (Input.GetKey(crouchButton)||isCrouched)
        {
            CrouchMove();
        }
        else if (Input.GetKey(runButton) && !isCrouched)
        {
            Run();
        }
        else 
        {
            Move();
        }


        if(Input.GetKeyDown(jumpButton) && isGrounded && !isTop)
        {
            Jump();
        }

        if(Input.GetKeyDown(jumpButton) && !isGrounded && canDoubleJump && !isTop)
        {
            Jump();
            canDoubleJump = false;
        }

        if (Input.GetKey(jumpButton) && canLongJump)
        {
            longJumpTimer += Time.deltaTime;
            if (longJumpTimer >= longJumpTime)
            {
                LongJump();
            }
        }


        if (Input.GetKeyDown(crouchButton) && !isCrouched)
        {
            crouchButtonIsHeld = true;
            Crouch();
        }

        if(headColliding && Input.GetKeyDown(crouchButton))
        {
            crouchButtonIsHeld = true;
        }

        if (Input.GetKeyUp(crouchButton))
        {
            crouchButtonIsHeld = false;
        }

        if (Input.GetKeyUp(crouchButton) && !headColliding)
        {
            UnCrouch();
        }

        if (isTop)
        {
            headColliding = true;
        }
        else
        {
            headColliding = false;
        }

        if (!crouchButtonIsHeld && !headColliding)
        {
            DefaultHeightAndCamera();
        }

    }

    void DefaultHeightAndCamera()
    {
        //controller.height = 3f;
    }

    void Jump()
    {
        jumpYPos = transform.position.y;
        longJumpTimer = 0;
        canLongJump = true;
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityConst *gravityMultiplayer);
    }

    void LongJump()
    {
        canLongJump = false;
        float heightDiff = transform.position.y - jumpYPos;
        velocity.y = Mathf.Sqrt((longJumpHeight - heightDiff) * -2 * gravityConst * gravityMultiplayer);
    }

    void CrouchMove()
    {
        Vector3 motion = movementDir * crouchSpeed * Time.deltaTime;
        controller.Move(motion);
    }

    void Crouch()
    {
        isCrouched = true;
        controller.height *= 0.5f;
        Camera.main.transform.localPosition -= new Vector3(0, 0.5f * Camera.main.transform.localPosition.y, 0);
        groundCheck.localPosition *= 0.5f;
    }

    void UnCrouch()
    {
        isCrouched = false;
        controller.height *= 2;
        Camera.main.transform.localPosition += new Vector3(0, Camera.main.transform.localPosition.y, 0);
        groundCheck.localPosition *= 2;
    }

    void Run()
    {
        Vector3 motion = movementDir * runSpeed * Time.deltaTime;
        controller.Move(motion);
    }

    void Move()
    {
        Vector3 motion = movementDir * movementSpeed * Time.deltaTime;
        isMoving = (motion != Vector3.zero);
        controller.Move(motion);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "JumpPad" && isGrounded)
        {
            jumpHeight = 10f;
            Jump();
        }
        else if (hit.gameObject.layer == 3)
        {
            jumpHeight = currentJumpHeight;
        }
    }

}
