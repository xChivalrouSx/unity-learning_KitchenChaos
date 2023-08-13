using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private bool isWailking;

    // Update is called once per frame
    private void Update()
    {
        Vector3 inputVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.z -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }

        inputVector = inputVector.normalized;
        transform.position += inputVector * moveSpeed * Time.deltaTime;

        isWailking = inputVector != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, inputVector, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWailking;
    }
}
