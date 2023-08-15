using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += InteractPerformed;
    }

    private void InteractPerformed(CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementVectorNormalized()
    {
        return playerInputActions.Player.Move.ReadValue<Vector3>().normalized;
    }

    //public Vector3 GetMovementVectorNormalizedOld()
    //{
    //    Vector3 inputVector = new Vector3(0, 0, 0);
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        inputVector.z += 1;
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        inputVector.z -= 1;
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        inputVector.x -= 1;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        inputVector.x += 1;
    //    }
    //    return inputVector.normalized;
    //}
}
