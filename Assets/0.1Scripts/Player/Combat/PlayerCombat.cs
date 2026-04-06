using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PunchHitbox punchHitbox;
    [SerializeField] private CameraShake cameraShake;

    [Header("Audio")]
    [SerializeField] private AudioClip punchSwingClip;
    [SerializeField] private AudioClip punchHitClip;

    [Header("Hit Pause")]
    [SerializeField] private float hitPauseDuration = 0.05f;

    private PlayerAnimator animator;
    private PlayerInput input;
    private PlayerController controller;
    private AudioSource audioSource;

    private bool isAttacking;
    private bool isHitPauseRunning;

    private void Awake()
    {
        animator = GetComponent<PlayerAnimator>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();

        if (punchHitbox == null)
            punchHitbox = GetComponentInChildren<PunchHitbox>();

        if (cameraShake == null && Camera.main != null)
            cameraShake = Camera.main.GetComponentInChildren<CameraShake>();

        if (punchHitbox != null)
            punchHitbox.OnSuccessfulHit += HandleSuccessfulHit;
    }

    private void OnDestroy()
    {
        if (punchHitbox != null)
            punchHitbox.OnSuccessfulHit -= HandleSuccessfulHit;
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

        if (punchSwingClip != null)
            audioSource.PlayOneShot(punchSwingClip);

        animator.TriggerAttack();
    }

    private void HandleSuccessfulHit(Collider target)
    {
        if (punchHitClip != null)
            audioSource.PlayOneShot(punchHitClip);

        if (cameraShake != null)
            cameraShake.Shake(0.08f, 0.04f);

        if (!isHitPauseRunning)
            StartCoroutine(HitPause(hitPauseDuration));
    }

    private IEnumerator HitPause(float duration)
    {
        isHitPauseRunning = true;

        float oldTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = oldTimeScale;
        isHitPauseRunning = false;
    }

    // Animation Events
    public void AE_EnableHitbox()
    {
        if (punchHitbox != null)
            punchHitbox.Activate();
    }

    public void AE_DisableHitbox()
    {
        if (punchHitbox != null)
            punchHitbox.Deactivate();
    }

    public void AE_EndAttack()
    {
        isAttacking = false;
        controller.SetAttacking(false);
    }
}