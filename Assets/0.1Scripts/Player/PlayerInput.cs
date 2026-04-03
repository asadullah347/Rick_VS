using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Movement
    public float Move { get; private set; }
    public float Turn { get; private set; }

    // Actions
    public bool AttackPressed { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool SprintHeld { get; private set; }

    private void Update()
    {
        // Movement input
        Move = Input.GetAxis("Vertical");
        Turn = Input.GetAxis("Horizontal");

        // Action input
        AttackPressed = Input.GetMouseButtonDown(0); // LEFT CLICK
        JumpPressed = Input.GetButtonDown("Jump");   // SPACE
        SprintHeld = Input.GetKey(KeyCode.LeftShift);
    }
}