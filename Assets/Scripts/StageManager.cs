using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;
using Select;
public class StageManager : Singleton<StageManager>
{
    [SerializeField] private GameObject leftArrow, rightArrow;
    public const int CAULDRONINDEX = 2,SELLINDEX = 1,POTIONINDEX = 0;
    int stageCount = 3;
    public int index;
    [SerializeField] private InputData data;
    public delegate void OnSwipeLeft();
    public delegate void OnSwipeRight();
    public OnSwipeLeft onSwipeLeft;
    public OnSwipeLeft onSwipeRight;
    
    
    private void Start()
    {
        ToggleArrowImages();
    }
    //private void Update()
    //{
    //    //Debug.Log (data.SwipeLeft + " " + index);
    //    if(data.SwipeLeft)
    //    {
    //        if(index > 0)
    //        {
    //            CheckArrow();
    //            index--;
    //            onSwipeLeft?.Invoke();
    //        }
    //    }
    //    if (data.SwipeRight)
    //    {
    //        if(index < stageCount-1)
    //        {
    //            CheckArrow();
    //            index++;
    //            onSwipeRight?.Invoke();
    //        }
    //    }
    //}
    private bool CheckCanRotate()
    {
        if (SelectManager.Instance.GetSelectedObject() != null) 
            return false;
        if(ShakerMovement.Instance.IsCurrentShaker())
            return false;
        Book [] books = FindObjectsOfType<Book>();
        foreach (var item in books)
        {
            if(item.IsOpen())
                return false;
        }
        return true;
    }
    public void ForceLeft()
    {
        if (index < stageCount - 1)
        {
            index++;
            
            onSwipeRight?.Invoke();
        }
    }
    public void ForceRight()
    {
        if (index > 0)
        {
            index--;
            
            onSwipeLeft?.Invoke();
        }
    }
    public void Left()
    {
        if (!CheckCanRotate()) 
            return;
        if (index < stageCount - 1)
        {
            index++;
            ToggleArrowImages();
            onSwipeRight?.Invoke();
        }
    }
    public void Right()
    {
        if (!CheckCanRotate()) 
            return;
        if (index > 0)
        {
            index--;
            ToggleArrowImages();
            onSwipeLeft?.Invoke();
        }
    }
    private void ToggleArrowImages()
    {
        if (index == 0)
        {
            rightArrow.SetActive(false);
            leftArrow.SetActive(true);
        }
        
        else if(index == 1)
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
        }
        else if(index == 2)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
            // leftArrow.SetActive(true);
            // rightArrow.SetActive(true);

        }
        
    }
    public void ToggleArrows(bool value,bool Check = false)
    {
        if(value == false)
        {
            rightArrow.SetActive(value);
            leftArrow.SetActive(value);    
        }
        else
        {   
            ToggleArrowImages();
        }
    }
}
