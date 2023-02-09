using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerManager : QueueableManager
{
     public static SellerManager Instance;
    //[SerializeField] private Transform _parentCanvasOrder;
        
    
    
    void Awake()
    {
        if(Instance == null)
        {
            Instance = FindObjectOfType<SellerManager>();
        }
        Instance = this;
    }
    public void RejectOffer()
    {

    }
    public void AcceptOffer()
    {

    }

}
