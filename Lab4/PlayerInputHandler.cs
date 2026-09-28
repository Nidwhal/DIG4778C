using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    //variables for relevant commands
    public Vector2 MoveInput { get; private set; }
    public bool IsFiring { get; set; }

    //method for moving
    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }

    //method for firing
    public void OnFire(InputValue value)
    {
        IsFiring = value.isPressed;
    }
}
