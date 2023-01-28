using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OrderState : CustomerStateBase
{
    public GameObject speechBubble;
    [SerializeField] private List<Order> order;
    public override void StartState(CustomerAnimations customerAnimations)
    {
        speechBubble.SetActive(true);
        customer.transform.LookAt(CustomerManager.Instance.transform);
        customer.Stop();
        CustomerManager.Instance.CurrentOrder = order;
        CustomerManager.Instance.CurrentCustomer = customer;
    }

    public override void UpdateState(CustomerAnimations customerAnimations)
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            customer.ChangeCurrentState(customer.exitState);
        }
    }
    public override void TriggerEnterState(CustomerAnimations customerAnimations, Collider other)
    {

    }
    //public List<GameObject> GetOrderImages()
    //{
    //    return order.Select(x => x.gameObject).ToList();
    //}

}
