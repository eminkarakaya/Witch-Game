using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTypeHolder : Singleton<ItemTypeHolder>
{
    public Transform parent;
    public List<ItemType> itemTypes = new List<ItemType>();
    public List<int> counts; 
    void OnEnable()
    {
        StageManager.Instance.onSwipeLeft += CheckImage;
        StageManager.Instance.onSwipeRight += CheckImage;
    }
    void OnDisable()
    {
        StageManager.Instance.onSwipeRight -= CheckImage;
        StageManager.Instance.onSwipeLeft -= CheckImage;   
    }
    private void CheckImage()
    {
        if(StageManager.Instance.index == StageManager.CAULDRONINDEX)
        {
            parent.gameObject.SetActive(true);
        }
        else
        {
            parent.gameObject.SetActive(false);
        }
    }
    public void ClearItems()
    {
        
        for (int i = 0; i <  parent.childCount; i++)
        {
            
            Destroy(parent.GetChild(i).gameObject);
        }
       
        itemTypes.Clear();
    }
}
