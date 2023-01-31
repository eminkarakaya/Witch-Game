using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullQuest : Quest
{
    private Bottle bottle;
    private void OnEnable()
    {
        bottle = GetComponent<Bottle>();
    }
    private void Update()
    {
        if (bottle.GetCurrentColorFull() && QuestManager.Instance.CheckCurrentQuest(this))
        {
            QuestManager.Instance.RemoveQuest(this);

        }
    }
    public override void Trigger()
    {
        
    }
}
