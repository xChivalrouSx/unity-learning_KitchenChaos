using UnityEngine;

public class GameInput : MonoBehaviour
{
    PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector3 GetMovementVectorNormalized()
    {
        Vector3 inputVector = playerInputActions.Player.Move.ReadValue<Vector3>();
        return inputVector.normalized;
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
