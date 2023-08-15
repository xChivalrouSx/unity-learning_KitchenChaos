using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectFactory kitchenObjectFactory;

    private ClearCounter clearCounter;

    public KitchenObjectFactory GetKitchenObjectFactory()
    {
        return kitchenObjectFactory;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }
        this.clearCounter = clearCounter;
        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
