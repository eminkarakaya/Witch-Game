using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueWaitState : CustomerStateBase
{
    public override void StartState(CustomerAnimations customerAnimations)
    {
        customer = GetComponentInParent<Customer>();
        customer.transform.LookAt(CustomerManager.Instance.transform);
        customer.Stop();
            
    }
    public override void UpdateState(CustomerAnimations customerAnimations)
    {
        if (CustomerManager.Instance.GetQueueCustomer(0) == customer)
        {
            customer.CurrentState = customer.moveState;
        }
    }
    public override void TriggerEnterState(CustomerAnimations customerAnimations, Collider other)
    {
       
    }


}
