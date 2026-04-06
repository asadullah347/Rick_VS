using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerController))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PunchHitbox punchHitbox;

    private PlayerAnimator animator;
    private PlayerInput input;
    private PlayerController controller;

    private bool isAttacking;

    private void Awake()
    {
        animator = GetComponent<PlayerAnimator>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<PlayerController>();

        if (punchHitbox == null)
            punchHitbox = GetComponentInChildren<PunchHitbox>();
    }

    private void Update()
    {
        if (input.AttackPressed && !isAttacking)
            Attack();
    }

    private void Attack()
    {
        isAttacking = true;
        controller.SetAttacking(true);
        animator.TriggerAttack();
    }

    public void AE_EnableHitbox()
    {
        punchHitbox.Activate();
    }

    public void AE_DisableHitbox()
    {
        punchHitbox.Deactivate();
    }

    public void AE_EndAttack()
    {
        isAttacking = false;
        controller.SetAttacking(false);
    }
}