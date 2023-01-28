using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private int _speed;

    public int Speed
    {
        get { return _speed; }
        set
        {
            _speed = value;
            agent.speed = _speed;
        }
    }

    [SerializeField] private CustomerData customerData;
    private CustomerAnimations customerAnimations;
    [Header("States")]
    [Space(10)]
    [SerializeField] public MoveState moveState;
    [SerializeField] public OrderState orderState;
    [SerializeField] public ExitState exitState;
    [SerializeField] public QueueState queueState;
    [SerializeField] public QueueWaitState queueWaitState;
    [Space(10)]
    [SerializeField] private CustomerStateBase _currentState;
    public CustomerStateBase CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            _currentState.StartState(customerAnimations);
        }
    }

    private NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = customerData.speed;
        CurrentState = moveState;
        customerAnimations = GetComponent<CustomerAnimations>();
        CurrentState.StartState(customerAnimations);
    }
    private void Update()
    {
        _currentState.UpdateState(customerAnimations);
    }
    private void OnTriggerEnter(Collider other)
    {
        CurrentState.TriggerEnterState(customerAnimations, other);
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
        agent.speed = customerData.speed;
    }
    public bool CheckQueue()
    {
       if(CustomerManager.Instance.GetQueueCustomer(0) == this)
       {
            return false;
       }
        return true;
    }
}
