using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Book : MonoBehaviour
{
    [SerializeField] private GameObject closeIcon;
    [SerializeField] private List<GameObject> canvases;
    [SerializeField] private Transform child;
    Vector3 oldPos;
    Quaternion oldRot;
    [SerializeField] private Vector3 childRot;
    [SerializeField] private float dur;
    bool isOpened;
    private void Start()
    {
        oldRot = transform.rotation;
        oldPos = transform.position;
    }
    public void OpenBook()
    {
        if (isOpened) return;
        
        isOpened = true;
        transform.DOMove(CameraController.CameraController.Instance.bookPos.position, dur);
        transform.DORotate(CameraController.CameraController.Instance.bookPos.rotation.eulerAngles, dur);
        child.DOLocalRotate(Vector3.zero, dur);
        closeIcon.SetActive(true);
    }
    public void CloseBook()
    {
        if (!isOpened) return;
        closeIcon.SetActive(false);
        isOpened = false;
        transform.DOMove(oldPos, dur);
        transform.DORotate(oldRot.eulerAngles, dur);
        child.DOLocalRotate(childRot, dur);
    }
}
