using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : CustomerStateBase
{
    Vector3 pos;
    public override void StartState(CustomerAnimations customerAnimations)
    {
        CustomerManager.Instance.CreateQueue(customer);
        if(customer.CheckQueue())
        {
            customer.ChangeCurrentState(customer.queueState);
        }
        pos = CustomerManager.Instance.GetQueueHead();
        customer.SetDestination(pos);
    }

    public override void UpdateState(CustomerAnimations customerAnimations)
    {
        if (Vector3.Distance(customer.transform.position, pos) < .7f)
        {
            customer.CurrentState = customer.orderState;
        }
    }
    public override void TriggerEnterState(CustomerAnimations customerAnimations, Collider other)
    {
        

    }


}
