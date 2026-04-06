using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cam;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float speedChangeRate = 5f;
    [SerializeField] private float turnSpeed = 8f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 1.2f;

    [Header("Attack Movement")]
    [SerializeField] private float attackMoveMultiplier = 0.15f; // 0 = no move, 0.15 = very small move
    [SerializeField] private bool lockRotationDuringAttack = true;

    private CharacterController controller;
    private PlayerAnimator animator;
    private PlayerInput input;

    private float verticalVelocity;
    private float speed;
    private bool isAttacking;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<PlayerAnimator>();
        input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (cam == null && Camera.main != null)
            cam = Camera.main.transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float targetSpeed = input.SprintHeld ? sprintSpeed : walkSpeed;

        if (isAttacking)
            targetSpeed *= attackMoveMultiplier;

        speed = Mathf.MoveTowards(speed, targetSpeed, speedChangeRate * Time.deltaTime);

        Vector3 move = new Vector3(input.Turn, 0f, input.Move);
        move = transform.TransformDirection(move);
        move *= speed;

        move.y = HandleGravity();
        controller.Move(move * Time.deltaTime);

        float animSpeed = speed * Mathf.Max(Mathf.Abs(input.Move), Mathf.Abs(input.Turn));
        animator.SetMoveSpeed(animSpeed);

        if (!isAttacking || !lockRotationDuringAttack)
            HandleRotation();
    }

    private void HandleRotation()
    {
        if (cam == null) return;

        if (Mathf.Abs(input.Move) < 0.1f && Mathf.Abs(input.Turn) < 0.1f)
            return;

        Vector3 lookDir = cam.forward;
        lookDir.y = 0;

        if (lookDir.sqrMagnitude < 0.01f) return;

        Quaternion targetRot = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
    }

    private float HandleGravity()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -1f;
            animator.SetGrounded(true);

            if (input.JumpPressed && !isAttacking)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * 2f * gravity);
                animator.TriggerJump();
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            animator.SetGrounded(false);
        }

        return verticalVelocity;
    }

    // Called from PlayerCombat
    public void SetAttacking(bool value)
    {
        isAttacking = value;
    }
}