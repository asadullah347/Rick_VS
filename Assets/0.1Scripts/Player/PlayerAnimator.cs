using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Animations")]
    [HideInInspector]
    public int animMoveSpeed;
    public int animJump;
    public int animGrounded;
    public int animAttack;
    public int animShoot;

    public Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetUpAnimator();
    }


    void Update()
    {
      
    }

    private void SetUpAnimator()
    {
        animMoveSpeed = UnityEngine.Animator.StringToHash("MoveSpeed");
        animJump = UnityEngine.Animator.StringToHash("Jump");
        animGrounded = UnityEngine.Animator.StringToHash("Grounded");
        animAttack = UnityEngine.Animator.StringToHash("Attack");
        animShoot = UnityEngine.Animator.StringToHash("Shoot");
    }
}
