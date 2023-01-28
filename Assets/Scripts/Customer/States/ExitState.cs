using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitState : CustomerStateBase
{
    public override void StartState(CustomerAnimations customerAnimations)
    {
        customer.StartMove();
        customer.SetDestination(CustomerManager.Instance.exitTransform.position);
        CustomerManager.Instance.UpdateQueue(customer);
    }

    public override void TriggerEnterState(CustomerAnimations customerAnimations, Collider other)
    {
        
    }

    public override void UpdateState(CustomerAnimations customerAnimations)
    {
        
    }
}
