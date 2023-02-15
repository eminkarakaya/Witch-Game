using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum AnimationType
{
    Lineer,
    Circle
}
public class TouchAnimation : MonoBehaviour
{
    [SerializeField] private AnimationType animationType;
    [SerializeField] private Transform start, finish;
    [SerializeField] private float duration,_radius,angle,angleMultiply;
    [SerializeField] private Ease ease;
    void OnEnable()
    {
        switch (animationType)
        {
            case AnimationType.Lineer:
                transform.position = start.position;
                transform.DOMove(finish.position, duration).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
                break;
            case AnimationType.Circle:
                StartCoroutine(Circle());
                break;
        }
    }

    private IEnumerator Circle()
    {
        transform.position = Vector3.zero;
        while(true)
        {
            float x = _radius * Mathf.Cos(angle*Mathf.Deg2Rad) ;
            float z = _radius * Mathf.Sin(angle*Mathf.Deg2Rad) ;
            angle += angleMultiply;
            GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero + new Vector3(x,z, transform.position.z);
            yield return null;
        }
    }
}