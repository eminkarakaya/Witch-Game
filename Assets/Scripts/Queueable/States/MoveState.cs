using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : CustomerStateBase
{
    Vector3 pos;
    public override void StartState(QueueableAnimations customerAnimations)
    {
        
        QueueableManager.Instance.CreateQueue(queueable);
        if(queueable.CheckQueue())
        {
            queueable.ChangeCurrentState(queueable.queueState);
        }
        pos = QueueableManager.Instance.GetQueueHead();
        queueable.SetDestination(pos);
    }

    public override void UpdateState(QueueableAnimations customerAnimations)
    {
        if (Vector3.Distance(queueable.transform.position, pos) < .7f)
        {
            if(queueable.TryGetComponent(out Customer customer))
            {
                queueable.CurrentState = customer.orderState;
            }
            else if(queueable.TryGetComponent(out Seller seller))
            {
                queueable.CurrentState = seller.sellState;
            }
        }
    }
    public override void TriggerEnterState(QueueableAnimations customerAnimations, Collider other)
    {
        

    }


}
