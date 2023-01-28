using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;
using Select;
public class StageManager : Singleton<StageManager>
{
    [SerializeField] private GameObject leftArrow, rightArrow;
    int stageCount = 2;
    public int index;
    [SerializeField] private InputData data;
    public delegate void OnSwipeLeft();
    public delegate void OnSwipeRight();
    public OnSwipeLeft onSwipeLeft;
    public OnSwipeLeft onSwipeRight;

    private void Start()
    {
        CheckArrow();
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
    public void Left()
    {
        if (SelectManager.Instance.GetSelectedObject() != null)
            return;
        if (index < stageCount - 1)
        {
            index++;
            CheckArrow();
            onSwipeRight?.Invoke();
        }
    }
    public void Right()
    {
        if (SelectManager.Instance.GetSelectedObject() != null)
            return;
        if (index > 0)
        {
            index--;
            CheckArrow();
            onSwipeLeft?.Invoke();
        }
    }
    private void CheckArrow()
    {
        if (index == 0)
        {
            rightArrow.SetActive(false);
            leftArrow.SetActive(true);
        }
        
        else if(index == 1)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
        }
        else
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);

        }
        
    }
}
