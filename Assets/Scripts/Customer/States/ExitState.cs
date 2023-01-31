using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitState : CustomerStateBase
{
    Vector3 pos;
    public override void StartState(CustomerAnimations customerAnimations)
    {
        pos = CustomerManager.Instance.exitTransform.position;
        customer.StartMove();
        customer.SetDestination(pos);
        CustomerManager.Instance.UpdateQueue(customer);
    }

    public override void TriggerEnterState(CustomerAnimations customerAnimations, Collider other)
    {
        
    }

    public override void UpdateState(CustomerAnimations customerAnimations)
    {
        if (Vector3.Distance(customer.transform.position, pos) < 2f)
        {
            Destroy(customer.gameObject);
        }
    }
}
