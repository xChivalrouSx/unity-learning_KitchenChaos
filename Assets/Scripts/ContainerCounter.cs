using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayyerGrabbedObject;


    [SerializeField] private KitchenObjectFactory kitchenObjectFactory;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectFactory.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayyerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }

}
