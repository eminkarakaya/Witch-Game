using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftQuest : Quest
{
    public override void Trigger()
    {
        QuestManager.Instance.RemoveQuest(this);
    }
}
