using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public List <Transform> potionTransforms;
    public List<GameObject> orderObjects;
    public int index;
    public int IncreaseAndGetIndex()
    {
        index++;
        return index - 1;
    }
    public void ResetIndex()
    {
        index = 0;
    }

}
