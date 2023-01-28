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
    public override void StartState(CustomerAnimations customerAnimations)
    {
        customer = GetComponentInParent<Customer>();
        customer.StartMove();
        if (!isUpdate)
        {
            _queuePlace = CustomerManager.Instance.GetQueuePos();
        }
        isUpdate = false;
        customer.SetDestination(_queuePlace);
    }

    public override void UpdateState(CustomerAnimations customerAnimations)
    {
        if (Vector3.Distance(customer.transform.position, _queuePlace) < .7f)
        {
            customer.CurrentState = customer.queueWaitState;
        }
    }
    public override void TriggerEnterState(CustomerAnimations customerAnimations, Collider other)
    {

    }

}
