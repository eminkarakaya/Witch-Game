using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Select;
public class CustomerManager : QueueableManager 
{
    public static CustomerManager Instance;
    //[SerializeField] private Transform _parentCanvasOrder;
        
    
    [SerializeField] private List<CustomerOrder> _currentOrder;
    public List<CustomerOrder> CurrentOrder { get => _currentOrder; set { _currentOrder = value; }}
    void Awake()
    {
        if(Instance == null)
        {
            Instance = FindObjectOfType<CustomerManager>();
        }
        Instance = this;
    }

    private void OnEnable()
    {
        //totalCustomerCount = PlayerPrefs.GetInt(TOTALCUSTOMER);
        //savedTotalCustomerCount = PlayerPrefs.GetInt(TOTALCUSTOMER);
    }


    private void OnDisable()
    {
        //PlayerPrefs.SetInt(TOTALCUSTOMER, savedTotalCustomerCount);
    }


    public bool CheckCustomerOrder(ColorType colorType)
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
            CurrentQueueable.CurrentState = CurrentQueueable.exitState;
            foreach (var obj in FindObjectOfType<Table>().orderObjects)
            {
                Destroy(obj);
            }
            FindObjectOfType<Table>().ResetIndex();
            currentCustomerCount--;
            savedTotalCustomerCount++;
        }
    }
    
}
