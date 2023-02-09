using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueState : CustomerStateBase
{
    public bool isUpdate;
    [SerializeField] private Vector3 _queuePlace;
    public Vector3 queuePlace
    {
        get => _queuePlace;
        set
        {
            _queuePlace = value;
        }
    }
    public override void StartState(QueueableAnimations customerAnimations)
    {
        queueable = GetComponentInParent<Queueable>();
        queueable.StartMove();
        if (!isUpdate)
        {
            _queuePlace = CustomerManager.Instance.GetQueuePos();
        }
        isUpdate = false;
        queueable.SetDestination(_queuePlace);
    }

    public override void UpdateState(QueueableAnimations customerAnimations)
    {
        if (Vector3.Distance(queueable.transform.position, _queuePlace) < .7f)
        {
            queueable.CurrentState = queueable.queueWaitState;
        }
    }
    public override void TriggerEnterState(QueueableAnimations customerAnimations, Collider other)
    {

    }

}
