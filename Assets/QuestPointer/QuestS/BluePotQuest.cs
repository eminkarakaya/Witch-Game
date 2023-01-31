using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Select;
public class BluePotQuest : Quest
{
    public override void Trigger()
    {
        Debug.Log("qsjopqewjqý");
        SelectManager.Instance.onSelectPotion?.Invoke(transform.parent.gameObject);
        QuestSelectManager.Instance.selectedObject = transform.parent.gameObject;
        SelectManager.Instance.SetSelectedObject(transform.parent.gameObject);
        QuestManager.Instance.RemoveQuest(this);
    }

}
