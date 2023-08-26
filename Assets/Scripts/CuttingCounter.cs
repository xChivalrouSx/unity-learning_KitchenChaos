using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    public event EventHandler<OnProgressChangeEventArgs> OnProgressChanged;
    public class OnProgressChangeEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeFactory[] cuttingRecipeFactories;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProgress = 0;

                CuttingRecipeFactory cuttingRecipeFactory = GetCuttingRecipeFactoryWithInput(GetKitchenObject().GetKitchenObjectFactory());
                OnProgressChanged?.Invoke(this, new OnProgressChangeEventArgs
                {
                    progressNormalized = (float)cuttingProgress / cuttingRecipeFactory.cuttingProgressMax
                });
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
                cuttingProgress++;
                OnCut?.Invoke(this, EventArgs.Empty);

                CuttingRecipeFactory cuttingRecipeFactory = GetCuttingRecipeFactoryWithInput(GetKitchenObject().GetKitchenObjectFactory());
                OnProgressChanged?.Invoke(this, new OnProgressChangeEventArgs
                {
                    progressNormalized = (float)cuttingProgress / cuttingRecipeFactory.cuttingProgressMax
                });

                if (cuttingProgress >= cuttingRecipeFactory.cuttingProgressMax)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(outputKitchenObjectFactory, this);
                }
            }
        }
    }

    private KitchenObjectFactory GetOutputForInput(KitchenObjectFactory inputKitchenObjectFactory)
    {
        CuttingRecipeFactory cuttingRecipeFactory = GetCuttingRecipeFactoryWithInput(inputKitchenObjectFactory);
        if (cuttingRecipeFactory != null)
        {
            return cuttingRecipeFactory.output;
        }
        return null;
    }

    private CuttingRecipeFactory GetCuttingRecipeFactoryWithInput(KitchenObjectFactory inputKitchenObjectFactory)
    {
        foreach (CuttingRecipeFactory cuttingRecipeFactory in cuttingRecipeFactories)
        {
            if (cuttingRecipeFactory.input == inputKitchenObjectFactory)
            {
                return cuttingRecipeFactory;
            }
        }
        return null;
    }

}
