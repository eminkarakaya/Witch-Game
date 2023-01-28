using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomerStateBase : MonoBehaviour
{
    [SerializeField] protected Customer customer;
    private void Awake()
    {
        //ai = GetComponentInParent<AI>();
    }
    public abstract void StartState(CustomerAnimations customerAnimations);
    public abstract void UpdateState(CustomerAnimations customerAnimations);
    public abstract void TriggerEnterState(CustomerAnimations customerAnimations, Collider other);

}
