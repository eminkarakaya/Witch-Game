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
            Debug.Log(customer.orderState);
            queueable.CurrentState = customer.orderState;
        }
    }
    public override void TriggerEnterState(QueueableAnimations customerAnimations, Collider other)
    {
        

    }


}
