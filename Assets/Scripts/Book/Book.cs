using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Book : MonoBehaviour
{
    public List<PotionRecipe> potionRecipeBookObj;
    [SerializeField] private GameObject closeIcon;
    [SerializeField] private List<GameObject> canvases;
    [SerializeField] private Transform child;
    Vector3 oldPos;
    Quaternion oldRot;
    [SerializeField] private Vector3 childRot;
    [SerializeField] private float dur;
    bool isOpened;
    [SerializeField] private List<GameObject> potionRecipes = new List<GameObject>();
    private void Start()
    {
        oldRot = transform.rotation;
        oldPos = transform.position;
    }
    public bool IsOpen()
    {
        return isOpened;
    }
    public void OpenBook()
    {
        if (isOpened) return;
        
        isOpened = true;
        transform.DOMove(cam.CamController.CameraController.Instance.bookPos.position, dur);
        transform.DORotate(cam.CamController.CameraController.Instance.bookPos.rotation.eulerAngles, dur);
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
    public GameObject GetRecipe(ColorType colorType,out GameObject questionMark)
    {
        foreach (var item in potionRecipeBookObj)
        {
            if(item.colorType == colorType)
            {
                questionMark = item.questionMark;
                return item.recipe;
            }
        }
        questionMark = null;
        return null;
    }
    
}
[System.Serializable]
public class PotionRecipe
{
    public ColorType colorType;
    public GameObject questionMark,recipe;
    public void Unlock()
    {
        questionMark.SetActive(false);
        recipe.SetActive(true);
    }
}

