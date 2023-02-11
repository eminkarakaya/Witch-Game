using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Shaker : MonoBehaviour
{
    public ItemType itemType;
    public Vector3 pos,rot;
    public Transform parentSalt;
    void Start()
    {
        pos = transform.position;
        rot = transform.rotation.eulerAngles;
    }
    public void SetParentSalt(Transform transform)
    {
        parentSalt.transform.SetParent(transform);
    }
}
