using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Select;
public class CustomerManager : MonoBehaviour 
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
    }


    private void OnDisable()
    {
        //
    }


    public bool CheckCustomerOrder(ColorType colorType)
    {
        foreach (var item in CurrentOrder)
        {
            if (item.colorType == colorType)
            {
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
            QueueableManager.Instance.CurrentQueueable.CurrentState = QueueableManager.Instance.CurrentQueueable.exitState;
            foreach (var obj in FindObjectOfType<Table>().orderObjects)
            {
                QueueableManager.Instance.CloseButtons();
                QueueableManager.Instance.CurrentQueueable = null;
                Destroy(obj);
            }
            FindObjectOfType<Table>().ResetIndex();
            // QueueableManager.Instance.SetCustomerIndex();
            
        }
    }
    
}
