using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QueueableStateBase : MonoBehaviour
{
    [SerializeField] protected Queueable queueable;
    private void Start()
    {
        
    }
    public abstract void StartState(QueueableAnimations customerAnimations);
    public abstract void UpdateState(QueueableAnimations customerAnimations);
    public abstract void TriggerEnterState(QueueableAnimations customerAnimations, Collider other);
}
