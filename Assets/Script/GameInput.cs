using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}

    public event EventHandler onMenuButtonPressed;
    private InputActions inputActions;
    private void Awake()
    {
        Instance =this;
        inputActions = new InputActions();
        inputActions.Enable();

        inputActions.Player.Menu.performed += Menu_performed;

    }

    private void Menu_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        onMenuButtonPressed?.Invoke(this, EventArgs.Empty);
    }
    private void OnDestroy()
    {
        inputActions.Disable();
    }
    public bool IsUpActionPressed(){
    return inputActions.Player.LanderUp.IsPressed();
    }

    public bool IsLeftActionPressed()
    {
        return inputActions.Player.LanderLeft.IsPressed();
    }

    public bool IsRightActionPressed()
    {
        return inputActions.Player.LanderRight.IsPressed();
    }
    
        
    
}
