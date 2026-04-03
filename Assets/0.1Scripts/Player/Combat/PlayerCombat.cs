using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PunchHitbox punchHitbox;

    private PlayerAnimator animator;
    private PlayerInput input;

    private bool isAttacking;

    private void Awake()
    {
        animator = GetComponent<PlayerAnimator>();
        input = GetComponent<PlayerInput>();

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
        animator.TriggerAttack();
    }

    // Animation Events
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
    }
}