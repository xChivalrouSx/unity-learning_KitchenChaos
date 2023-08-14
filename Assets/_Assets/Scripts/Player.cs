using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameInput gameInput;

    private bool isWailking;

    // Update is called once per frame
    private void Update()
    {
        Vector3 inputVector = gameInput.GetMovementVectorNormalized();

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, inputVector, moveDistance);
        if (canMove)
        {
            transform.position += inputVector * moveDistance;
        }

        isWailking = inputVector != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, inputVector, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWailking;
    }
}
