using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueableManager : Singleton<QueueableManager>
{
    public Transform exitTransform;
    public AudioClip moneyClip;
    
    [SerializeField] private float frequency;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private GameObject _customerPrefab;
    [SerializeField] protected List<Queueable> queue = new List<Queueable>();
    public List<Transform> createdQueueTransform;
    
    private Queueable _currentQueueable;
    public Queueable CurrentQueueable { get => _currentQueueable; set
        {
            _currentQueueable = value;
            if(_currentQueueable.TryGetComponent(out Customer customer))
            {
                
            }

        }
    }
    
    protected int currentCustomerCount = 0,totalCustomerCount = 0,savedTotalCustomerCount = 0;
    [SerializeField] private List<Queueable> queueableList;
    const string TOTALCUSTOMER = "TotalCustomer";
    private void Start()
    {
        StartCoroutine(CreateCustomer());
    }


    private IEnumerator CreateCustomer()
    {
        while(true)
        {
            if(currentCustomerCount < GetQueueCapacity())
            {
                if (queueableList.Count <= totalCustomerCount)
                    totalCustomerCount = 0;
                Debug.Log(totalCustomerCount);
                Debug.Log(queueableList.Count + " count");
                var obj = Instantiate(queueableList[totalCustomerCount], _spawnTransform.position, Quaternion.identity);
                currentCustomerCount++;
                totalCustomerCount++;
            }
            
                
            yield return new WaitForSeconds(frequency);
        }
    }
    //private Customer RandomCustomer()
    //{

    //}
    private int GetQueueCapacity()
    {
        return createdQueueTransform.Count;
    }
    public void CreateQueue(Queueable queueable)
    {
        if (!queue.Contains(queueable))
        {
            queue.Add(queueable);
        }
    }


    public void UpdateQueue(Queueable queueable)
    {
        if (queue.Contains(queueable))
        {
            queue.Remove(queueable);
        }
        for (int i = 0; i < queue.Count; i++)
        {

            // queue[i].queueState.oncekiState = queue[i].currState;
            queue[i].queueState.queuePlace = createdQueueTransform[i].position;
            queue[i].queueState.isUpdate = true;
            queue[i].CurrentState = queue[i].queueState;
        }
    }
     public Queueable GetQueueCustomer(int index)
    {
        return queue[index];
    }
    public Vector3 GetQueuePos()
    {
        if(queue.Count == 0)
        {
            return createdQueueTransform[0].position;
        }
        return createdQueueTransform[queue.Count-1].position;
        
    }
    public Vector3 GetQueueHead()
    {
        return createdQueueTransform[0].position;
    }
    public void SetCustomerIndex()
    {
        currentCustomerCount--;
        savedTotalCustomerCount++;
    }
}
