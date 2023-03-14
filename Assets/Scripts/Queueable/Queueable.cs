using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Queueable : MonoBehaviour
{
    [SerializeField] private GameObject _rejectButton;
    [SerializeField] private QueueableData queueableData;
    private QueueableAnimations queueableAnimations;
    [Header("States")]
    [Space(10)]
    [SerializeField] public ExitState exitState;
    [SerializeField] public MoveState moveState;
    [SerializeField] public QueueState queueState;
    [SerializeField] public QueueWaitState queueWaitState;
    [Space(10)]
    [SerializeField] private QueueableStateBase _currentState;
    public QueueableStateBase CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            _currentState.StartState(queueableAnimations);
        }
    }

    private NavMeshAgent agent;
    private void Start()
    {
        queueableAnimations = GetComponent<QueueableAnimations>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = queueableData.speed;
        CurrentState = moveState;
        CurrentState.StartState(queueableAnimations);
    }
    private void Update()
    {
        _currentState.UpdateState(queueableAnimations);
    }
    private void OnTriggerEnter(Collider other)
    {
        CurrentState.TriggerEnterState(queueableAnimations, other);
    }
    public void SetDestination(Transform destination)
    {
        agent.SetDestination(destination.position);
    }
    public void ChangeCurrentState(CustomerStateBase state)
    {
        CurrentState = state;
    }
    public void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public void Stop()
    {
        agent.speed = 0;
    }
    public void StartMove()
    {
        agent.speed = queueableData.speed;
    }
    public bool CheckQueue()
    {
       if(QueueableManager.Instance.GetQueueCustomer(0) == this)
       {
            return false;
       }
        return true;
    }
}
