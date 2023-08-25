using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private CuttingRecipeFactory[] cuttingRecipeFactories;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            KitchenObjectFactory outputKitchenObjectFactory = GetOutputForInput(GetKitchenObject().GetKitchenObjectFactory());
            if (outputKitchenObjectFactory != null)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectFactory, this);
            }
        }
    }

    private KitchenObjectFactory GetOutputForInput(KitchenObjectFactory inputKitchenObjectFactory)
    {
        foreach (CuttingRecipeFactory cuttingRecipeFactory in cuttingRecipeFactories)
        {
            if (cuttingRecipeFactory.input == inputKitchenObjectFactory)
            {
                return cuttingRecipeFactory.output;
            }
        }
        return null;
    }

}
