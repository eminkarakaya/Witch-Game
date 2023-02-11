using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    GlowStoneDust,
    Water,
    Spider,
    Leaf
}
public class Cauldron : Singleton<Cauldron>
{
    public List<RecipeList> allRecipes;
    public List<Recipe> currentRecipes;

    public void AddRecipe(ItemType itemType)
    {
        foreach (var item in currentRecipes)
        {
            if(itemType == item.type)
            {
                item.count ++;
                return;
            }
        }
        currentRecipes.Add(new Recipe(itemType,1));
    }
    public void EmptyCauldron()
    {
        currentRecipes.Clear();
    }
}
[System.Serializable]
public class Recipe
{
    public ItemType type;
    public int count;
    public Recipe(ItemType itemType,int count)
    {
        this.type = itemType;
        this.count = count;
    }

}
[System.Serializable]
public class RecipeList
{
    public List<Recipe> recipe;
}
    
