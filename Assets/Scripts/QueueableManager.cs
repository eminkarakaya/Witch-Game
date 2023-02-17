using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueableManager : Singleton<QueueableManager>
{
    public Transform exitTransform;
    public AudioClip moneyClip;
    [SerializeField] private GameObject acceptBtn,rejectBtn;
    [SerializeField] private float frequency;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private GameObject _customerPrefab;
    [SerializeField] private List<Queueable> queue = new List<Queueable>();
    public List<Transform> createdQueueTransform;
    
    [SerializeField] private Queueable _currentQueueable;
    public Queueable CurrentQueueable { get => _currentQueueable; set
        {
            _currentQueueable = value;
        }
    }
    
    [SerializeField] private int currentCustomerCount = 0,totalCustomerCount = 0,savedTotalCustomerCount = 0;
    [SerializeField] private List<Queueable> queueableList;
    const string TOTALCUSTOMER = "TotalCustomer";
    void OnEnable()
    {
        StageManager.Instance.onSwipeRight += CheckButton;
        StageManager.Instance.onSwipeLeft += CheckButton;
        // totalCustomerCount = PlayerPrefs.GetInt(TOTALCUSTOMER);
        // savedTotalCustomerCount = PlayerPrefs.GetInt(TOTALCUSTOMER);

    }
    void OnDisable()
    {
        // PlayerPrefs.SetInt(TOTALCUSTOMER, savedTotalCustomerCount);
        StageManager.Instance.onSwipeRight -= CheckButton;
        StageManager.Instance.onSwipeLeft -= CheckButton;
    }
    private void Start()
    {
        StartCoroutine(CreateCustomer());
    }

    public void CloseButtons()
    {
        acceptBtn.SetActive(false);
        rejectBtn.SetActive(false);
    }

    public void IncreaseSavedCustomerCount()
    {
        Debug.Log("Incerease");
        savedTotalCustomerCount++;
    }

    public void OpenButtonForSeller()
    {
        if(StageManager.Instance.index == StageManager.SELLINDEX)
        {
            acceptBtn.SetActive(true);
            rejectBtn.SetActive(true);
        }
    }
    public void OpenButtonForCustomer()
    {
        if(StageManager.Instance.index == StageManager.SELLINDEX)
            rejectBtn.SetActive(true); 
    }
    private IEnumerator CreateCustomer()
    {
        while(true)
        {
            if(currentCustomerCount < GetQueueCapacity())
            {
                if (queueableList.Count <= totalCustomerCount)
                    totalCustomerCount = 0;
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
    public void DecreaseQueueableCount()
    {
        currentCustomerCount --;
    }
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
    public void RejectOffer()
    {
        if(CurrentQueueable != null)
        {
            QueueableManager.Instance.CloseButtons();
            CurrentQueueable.CurrentState = CurrentQueueable.exitState;
            CurrentQueueable = null;
            foreach (var obj in FindObjectOfType<Table>().orderObjects)
            {
                Destroy(obj);
            }
        }
    }
    public void CheckButton()
    {
        if(CurrentQueueable == null) return;
        if(StageManager.Instance.index == StageManager.SELLINDEX)
        {
            
            if(CurrentQueueable.TryGetComponent(out Customer customer))
            {
                OpenButtonForCustomer();
            }
            else
            {
                OpenButtonForSeller();
            }
        }
        else
            CloseButtons();
    }
}
