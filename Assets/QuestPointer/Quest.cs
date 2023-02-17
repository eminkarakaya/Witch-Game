using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public abstract class Quest : MonoBehaviour
{

    protected GameObject questObject;
    private void OnEnable()
    {
        questObject = this.gameObject;
    }
    void OutlineOn()
    {
        Outline outline = questObject.AddComponent<Outline>();
        outline.OutlineColor = Color.green;
    }
    public abstract void Trigger();
}
