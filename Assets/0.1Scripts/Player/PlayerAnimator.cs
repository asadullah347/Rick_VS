using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    public int animMoveSpeed;
    public int animJump;
    public int animGrounded;
    public int animAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        animMoveSpeed = Animator.StringToHash("MoveSpeed");
        animJump = Animator.StringToHash("Jump");
        animGrounded = Animator.StringToHash("Grounded");
        animAttack = Animator.StringToHash("Attack");
    }

    public void SetMoveSpeed(float value)
    {
        animator.SetFloat(animMoveSpeed, value);
    }

    public void SetGrounded(bool value)
    {
        animator.SetBool(animGrounded, value);
    }

    public void TriggerJump()
    {
        animator.SetTrigger(animJump);
    }

    public void TriggerAttack()
    {
        animator.SetTrigger(animAttack);
    }
}