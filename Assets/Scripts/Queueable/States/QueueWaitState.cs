using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueWaitState : CustomerStateBase
{
    public override void StartState(QueueableAnimations customerAnimations)
    {
        queueable = GetComponentInParent<Queueable>();
        queueable.transform.LookAt(QueueableManager.Instance.transform);
        queueable.Stop();
            
    }
    public override void UpdateState(QueueableAnimations customerAnimations)
    {
        if (QueueableManager.Instance.GetQueueCustomer(0) == queueable)
        {
            queueable.CurrentState = queueable.moveState;
        }
    }
    public override void TriggerEnterState(QueueableAnimations customerAnimations, Collider other)
    {
       
    }


}
