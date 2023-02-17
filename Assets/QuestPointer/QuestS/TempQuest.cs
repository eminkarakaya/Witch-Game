using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempQuest : Quest
{
    void Start()
    {
        QuestManager.Instance.RemoveQuest(this);
    }
    public override void Trigger()
    {
        
    }
}
