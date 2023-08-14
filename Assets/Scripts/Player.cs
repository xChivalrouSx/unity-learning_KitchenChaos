using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;

    private bool isWailking;
    private Vector3 lastInteractDirectory;

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWailking;
    }

    private void HandleInteractions()
    {
        Vector3 movementDirectory = gameInput.GetMovementVectorNormalized();

        if (movementDirectory != Vector3.zero)
        {
            lastInteractDirectory = movementDirectory;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDirectory, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            //ClearCounter clearCounter = raycastHit.transform.GetComponent<ClearCounter>();
            //if (clearCounter != null) { clearCounter.Interact(); }

            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }

    private void HandleMovement()
    {
        Vector3 movementDirectory = gameInput.GetMovementVectorNormalized();

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirectory, moveDistance);
        if (!canMove)
        {
            Vector3 movementDirectoryX = new Vector3(movementDirectory.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirectoryX, moveDistance);
            if (canMove)
            {
                movementDirectory = movementDirectoryX;
            }
            else
            {
                Vector3 movementDirectoryZ = new Vector3(0, 0, movementDirectory.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirectoryZ, moveDistance);
                if (canMove)
                {
                    movementDirectory = movementDirectoryZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += movementDirectory * moveDistance;
        }

        isWailking = movementDirectory != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movementDirectory, Time.deltaTime * rotateSpeed);
    }

}