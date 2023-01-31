using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestQuest : Quest
{
    Chest chest;
    private void OnEnable()
    {
        chest = GetComponent<Chest>();
    }
    public override void Trigger()
    {
        GameObject obj = QuestManager.Instance.bottle;
        chest.MovePotion(obj);
        QuestSelectManager.Instance._selectedEmptyObject = obj;
        QuestManager.Instance.RemoveQuest(this);
    }
}
