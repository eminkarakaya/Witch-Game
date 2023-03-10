using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerManager : Singleton<SellerManager>
{
    //[SerializeField] private Transform _parentCanvasOrder;
    public Seller currentSeller;
    public Book book;
    [SerializeField] private RecipeList _recipeList;
    
    public RecipeList RecipeList{get=>_recipeList; set{_recipeList = value;}}
    
    public void AcceptOffer()
    {
        if(GameManager.Instance.GetMoney() < currentSeller.sellState.cost.GetMoney()) return;
        PopupManager.Instance.SetPopupText (currentSeller.sellState.popupString);
        QueueableManager.Instance.CloseButtons();
        Cauldron.Instance.AddAllRecipes(_recipeList);
        _recipeList.Unlock();
        currentSeller.CurrentState = currentSeller.exitState;
        currentSeller = null;
        QueueableManager.Instance.CurrentQueueable = null;
    }
   
}
