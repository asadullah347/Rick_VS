using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerAnimator playerAnimator;     
    [SerializeField] private Enemy enemy;     
    
    [Header("References")]
    [SerializeField] private Transform cam;
    private CharacterController controller;


    [Header("Movement Setting")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float sprintTransitSpeed = 5f; //Declare how fast speed will change from walk to sprint
    [SerializeField] float turingSpeed = 2f;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float jumpheight = 1.1f;

    private float verticalVelocity;
    private float speed;

    [Header("inputs")]
    private float moveInput;
    private float turnInput;

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        
    }

    private void Update()
    {
        InputManager();
        Movement();
        HandleCombatInput();
    }
    private void Movement()
    {
        GroundMovement();
        Turn();
    }
    private void InputManager()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    private void GroundMovement()
    {
        Vector3 move = new Vector3 (turnInput,0, moveInput);
        move = transform.TransformDirection(move);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitSpeed * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTransitSpeed * Time.deltaTime);
        }

        move *= speed;
        move.y = VerticalForceCalculation();
        controller.Move(move * Time.deltaTime);

        //Animatons
        playerAnimator.animator.SetFloat(playerAnimator.animMoveSpeed, speed * Mathf.Max(Mathf.Abs(moveInput), Mathf.Abs(turnInput)));
    }

    private void Turn()
    {
        if(Mathf.Abs(turnInput) > 0 || Mathf.Abs(moveInput) > 0)
        {
            Vector3 currLookDir = cam.forward;
            currLookDir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(currLookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turingSpeed * Time.deltaTime);
        }
    }

    private float VerticalForceCalculation()
    {
        if(controller.isGrounded)
        {
            verticalVelocity = -1;

            playerAnimator.animator.SetBool(playerAnimator.animGrounded, true);

            if (Input.GetButton("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpheight * gravity * 2);
                playerAnimator.animator.SetTrigger(playerAnimator.animJump);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            playerAnimator.animator.SetBool(playerAnimator.animGrounded, false);
        }
        return verticalVelocity;
    }

    private void HandleCombatInput()
    {
        if (Input.GetMouseButtonDown(0))  // Punch
        {
            playerAnimator.animator.SetTrigger(playerAnimator.animAttack);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Enemy>()?.TakeDamage(1);
    }

}