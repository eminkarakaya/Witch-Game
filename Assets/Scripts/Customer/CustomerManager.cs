using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Select;
public class CustomerManager : Singleton<CustomerManager>
{
    //[SerializeField] private Transform _parentCanvasOrder;
    [SerializeField] private List<Customer> customerList;
    [SerializeField] private float frequency;
    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private GameObject _customerPrefab;
    [SerializeField] private List<Order> _currentOrder;
    [SerializeField] private List<Customer> queue = new List<Customer>();
    private Customer _currentCustomer;
    public List<Order> CurrentOrder { get => _currentOrder; set { _currentOrder = value; }}
    public Customer CurrentCustomer { get => _currentCustomer; set { _currentCustomer = value; }}
    public List<Transform> createdQueueTransform;
    public Transform exitTransform;
    public AudioClip moneyClip;
    int currentCustomerCount = 0;
    int totalCustomerCount = 0;
    int savedTotalCustomerCount = 0;
    const string TOTALCUSTOMER = "TotalCustomer";
    private void OnEnable()
    {
        //totalCustomerCount = PlayerPrefs.GetInt(TOTALCUSTOMER);
        //savedTotalCustomerCount = PlayerPrefs.GetInt(TOTALCUSTOMER);
    }
    private void OnDisable()
    {
        //PlayerPrefs.SetInt(TOTALCUSTOMER, savedTotalCustomerCount);
    }
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
                if (customerList.Count <= totalCustomerCount)
                    totalCustomerCount = 0;
                var obj = Instantiate(customerList[totalCustomerCount], _spawnTransform.position, Quaternion.identity);
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
    public bool CheckOrder(ColorType colorType)
    {
        foreach (var item in CurrentOrder)
        {
            if (item.colorType == colorType)
            {
                Debug.Log(item.colorType + " " + colorType + " " + item, item);
                return true;
            }
        }
        return false;
    }
    public void OrderComplate(ColorType colorType)
    {
        foreach (var item in CurrentOrder)
        {
            if (item.colorType == colorType)
            {
                item.DecreaseCount(CurrentOrder);
                break;
            }
            
        }
        if (CurrentOrder.Count == 0)
        {
            CurrentCustomer.CurrentState = CurrentCustomer.exitState;
            foreach (var obj in FindObjectOfType<Table>().orderObjects)
            {
                Destroy(obj);
            }
            FindObjectOfType<Table>().ResetIndex();
            currentCustomerCount--;
            savedTotalCustomerCount++;
        }
    }
    public void CreateQueue(Customer customer)
    {
        if (!queue.Contains(customer))
        {
            queue.Add(customer);
        }
    }
    public void UpdateQueue(Customer customer)
    {
        if (queue.Contains(customer))
        {
            queue.Remove(customer);
        }
        for (int i = 0; i < queue.Count; i++)
        {

            // queue[i].queueState.oncekiState = queue[i].currState;
            queue[i].queueState.queuePlace = createdQueueTransform[i].position;
            queue[i].queueState.isUpdate = true;
            queue[i].CurrentState = queue[i].queueState;
        }
    }
    public Customer GetQueueCustomer(int index)
    {
        return queue[index];
    }
    public Vector3 GetQueueHead()
    {
        return createdQueueTransform[0].position;
    }
    public Vector3 GetQueuePos()
    {
        if(queue.Count == 0)
        {
            return createdQueueTransform[0].position;
        }
        return createdQueueTransform[queue.Count-1].position;
    }
}
