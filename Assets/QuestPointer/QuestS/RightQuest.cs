using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightQuest : Quest
{
    public override void Trigger()
    {
        Debug.Log("triggered");
        QuestManager.Instance.RemoveQuest(this);
    }
}
