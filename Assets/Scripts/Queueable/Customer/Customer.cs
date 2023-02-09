using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : Queueable
{
    [Header("States")]
    [Space(10)]

    [SerializeField] public OrderState orderState;
    
}
