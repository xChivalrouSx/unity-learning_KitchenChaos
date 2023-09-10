using System.Collections.Generic;
using UnityEngine;


public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListFactory recipeListFactory;

    private List<RecipeFactory> waitingRecipes;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 5f;
    private int waitingRecipesMax = 4;

    private void Awake()
    {
        Instance = this;

        waitingRecipes = new List<RecipeFactory>();
        spawnRecipeTimer = spawnRecipeTimerMax;
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipes.Count < waitingRecipesMax)
            {
                RecipeFactory recipeFactory = recipeListFactory.recipeFactoryList[Random.Range(0, recipeListFactory.recipeFactoryList.Count)];
                waitingRecipes.Add(recipeFactory);
                Debug.Log(recipeFactory.recipeName);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipes.Count; i++)
        {
            RecipeFactory waitingRecipeFactory = waitingRecipes[i];
            if (waitingRecipeFactory.kitchenObjectFactories.Count == plateKitchenObject.GetKitchenObjectFactories().Count)
            {
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectFactory recipeKitchenObjectFactory in waitingRecipeFactory.kitchenObjectFactories)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectFactory plateKitchenObjectFactory in plateKitchenObject.GetKitchenObjectFactories())
                    {
                        if (plateKitchenObjectFactory == recipeKitchenObjectFactory)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                        break;
                    }
                }
                if (plateContentsMatchesRecipe)
                {
                    Debug.Log("Player delivered the correnct recipe!..");
                    waitingRecipes.RemoveAt(i);
                    return;
                }
            }
        }
        Debug.Log("Player did not deliver a correnct recipe!..");
    }

}
