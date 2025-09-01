using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerAnimator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }
    private void Update()
    {
     if (Input.GetKeyDown(KeyCode.E))   // Shoot
         playerAnimator.animator.SetTrigger(playerAnimator.animShoot);
        
    }
}
