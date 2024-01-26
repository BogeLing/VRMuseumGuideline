using UnityEngine;
using UnityEngine.InputSystem;

public class ActionInputScript : MonoBehaviour
{
    public InputAction actionOne; // Assign in Inspector or via script
    public InputAction actionTwo; // Assign in Inspector or via script

    private void OnEnable()
    {
        // Enable the actions
        actionOne.Enable();
        actionTwo.Enable();

        // Subscribe to the action events
        actionOne.performed += OnActionOnePerformed;
        actionTwo.performed += OnActionTwoPerformed;
    }

    private void OnDisable()
    {
        // Disable the actions
        actionOne.Disable();
        actionTwo.Disable();

        // Unsubscribe from the action events
        actionOne.performed -= OnActionOnePerformed;
        actionTwo.performed -= OnActionTwoPerformed;
    }

    private void OnActionOnePerformed(InputAction.CallbackContext context)
    {
        // This will be called when actionOne is performed
        Debug.Log("Action One Performed");
    }

    private void OnActionTwoPerformed(InputAction.CallbackContext context)
    {
        // This will be called when actionTwo is performed
        Debug.Log("Action Two Performed");
    }
}
