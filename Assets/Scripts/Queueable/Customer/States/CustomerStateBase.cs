using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomerStateBase : QueueableStateBase
{
    protected Customer customer;
    void Start()
    {
        if(queueable.TryGetComponent(out Customer customer))
        {
            this.customer = customer;
        }
    }
}
