using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputActions inputActions;
    private void Awake()
    { 
        inputActions = new InputActions();
        inputActions.Player.Enable();
    }

    public Vector2 GetMoveInput()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public bool JumpWasPressed()
    {
        return inputActions.Player.Jump.WasPressedThisFrame();
    }

    public bool JumpWasReleased()
    {
        return inputActions.Player.Jump.WasReleasedThisFrame();
    }
}
