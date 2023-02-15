using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellState : SellerStateBase
{
    [SerializeField] private GameObject _speechBubble;
    [SerializeField] private ColorType colorType;
    [SerializeField] private RecipeList _recipeList;
    public Cost cost;
    [HideInInspector] public string popupString;
    public override void StartState(QueueableAnimations customerAnimations)
    {
        popupString = colorType + PopupManager.POTION_ADDED_TEXT;
        _speechBubble.SetActive(true);
        cost = _speechBubble.GetComponentInChildren<Cost>();
        _recipeList = Cauldron.Instance.GetRecipeList(colorType);
        SellerManager.Instance.RecipeList = _recipeList;
        SellerManager.Instance.currentSeller = queueable.GetComponent<Seller>();
        QueueableManager.Instance.CurrentQueueable = queueable;
        QueueableManager.Instance.OpenButtonForSeller();
    }
    public override void UpdateState(QueueableAnimations customerAnimations)
    {

    }
    public override void TriggerEnterState(QueueableAnimations customerAnimations, Collider other)
    {

    }
}
