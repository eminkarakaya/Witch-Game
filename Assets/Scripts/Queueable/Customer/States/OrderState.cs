using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OrderState : CustomerStateBase
{
    
    public GameObject speechBubble;
    [SerializeField] private List<CustomerOrder> order;
    public override void StartState(QueueableAnimations customerAnimations)
    {
        speechBubble.SetActive(true);
        InitOrders();
        queueable.transform.LookAt(CustomerManager.Instance.transform);
        queueable.Stop();
        CustomerManager.Instance.CurrentOrder = order;
        QueueableManager.Instance.CurrentQueueable = customer;
        QueueableManager.Instance.OpenButtonForCustomer();
    }
    private void InitOrders()
    {
        order = speechBubble.transform.GetComponentsInChildren<CustomerOrder>().ToList();
    }
    public override void UpdateState(QueueableAnimations customerAnimations)
    {
       
    }
    public override void TriggerEnterState(QueueableAnimations customerAnimations, Collider other)
    {

    }
    //public List<GameObject> GetOrderImages()
    //{
    //    return order.Select(x => x.gameObject).ToList();
    //}

}
