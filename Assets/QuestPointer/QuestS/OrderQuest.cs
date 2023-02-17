using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderQuest : Quest
{
    OrderState orderState;

    void Awake()
    {
        orderState = GetComponent<OrderState>();
        // QuestManager.Instance.AddAtQuest(this,0);
    }
    void Update()
    {
        if(orderState.speechBubble.activeSelf)
        {
            QuestManager.Instance.RemoveQuest(this);
        }
    }
    public override void Trigger()
    {

    }
}
