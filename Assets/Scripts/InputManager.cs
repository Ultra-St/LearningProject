using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static InputManager Instance { get; private set; }
    private InputSystem_Actions inputSystemActions;
    private void Awake()
    { 
        if(Instance != null)
        {
            Debug.LogError("There is more than one InputManager Instance");
        }
        Instance = this;

        inputSystemActions = new InputSystem_Actions();
        inputSystemActions.Player.Enable();
    }

    public Vector2 GetMoveInput()
    {
        Vector2 inputVector = inputSystemActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public bool JumpWasPressed()
    {
        return inputSystemActions.Player.Jump.WasPressedThisFrame();
    }

    // Método adicional para detectar cuando se suelta el botón de salto
    public bool JumpWasReleased()
    {
        return inputSystemActions.Player.Jump.WasReleasedThisFrame();
    }

    // Método para verificar si el botón de salto está siendo presionado
    public bool IsJumpHeld()
    {
        return inputSystemActions.Player.Jump.IsPressed();
    }

    private void OnDestroy()
    {
        if (inputSystemActions != null)
        {
            inputSystemActions.Player.Disable();
            inputSystemActions.Dispose();
        }
    }
}
