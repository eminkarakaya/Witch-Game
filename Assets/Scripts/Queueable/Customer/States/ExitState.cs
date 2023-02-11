using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitState : CustomerStateBase
{
    Vector3 pos;
    public override void StartState(QueueableAnimations customerAnimations)
    {
        pos = QueueableManager.Instance.exitTransform.position;
        queueable.StartMove();
        queueable.SetDestination(pos);
        QueueableManager.Instance.UpdateQueue(queueable);
    }

    public override void TriggerEnterState(QueueableAnimations customerAnimations, Collider other)
    {
        
    }

    public override void UpdateState(QueueableAnimations customerAnimations)
    {
        if (Vector3.Distance(queueable.transform.position, pos) < 2f)
        {
            Destroy(queueable.gameObject);
        }
    }
}
