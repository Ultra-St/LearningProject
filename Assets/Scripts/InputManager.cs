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

    // M�todo adicional para detectar cuando se suelta el bot�n de salto
    public bool JumpWasReleased()
    {
        return inputSystemActions.Player.Jump.WasReleasedThisFrame();
    }

    // M�todo para verificar si el bot�n de salto est� siendo presionado
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
