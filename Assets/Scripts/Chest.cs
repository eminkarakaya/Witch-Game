using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CameraController;
public class Chest : MonoBehaviour
{
    [SerializeField] private Vector3 rot;
    public GameObject potion;
    public GameObject CreatePotion()
    {
        var obj = Instantiate(potion.gameObject,transform.position,Quaternion.Euler(rot), Camera.main.transform);
        //obj.transform.SetParent(Camera.main.transform);
        MovePotion(obj);
        return obj;
    }
    public void MovePotion(GameObject gameObject)
    {
        gameObject.transform.DOMove(Camera.main.GetComponent<CameraController.CameraController>().potionPos.position, .3f);
    }
}
