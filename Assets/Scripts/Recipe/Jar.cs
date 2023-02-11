using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Inputs;
public class Jar : MonoBehaviour
{
    [SerializeField] private InputData _inputData;
    [SerializeField] private float dur;
    [SerializeField] private Ease ease;
    public ItemType itemType;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] bool isClick = false;
    void Update()
    {
        if(_inputData.IsEnd)
            isClick = false;
    }
    public void Movement()
    {
        if(isClick) return;
        isClick = true;
        var item = Instantiate(itemPrefab,transform.position,Quaternion.identity);
        item.transform.DOMove(Camera.main.GetComponent<CameraController.CameraController>().potionPos.position + new Vector3(0,2,0), .3f).
            OnComplete(()=>
            {
                if(itemType == ItemType.Leaf)
                {

                    item.transform.DOMoveY(Cauldron.Instance.transform.position.y+.5f,dur);
                    // Sequence seq;
                    // seq = DOTween.Sequence();
                    // seq.Append(item.transform.DOMoveZ(item.transform.position.z + 1,dur/3)).SetEase(ease);
                    // seq.Join(item.transform.DOMoveZ(item.transform.position.z + -1,dur/3)).SetEase(ease);
                    // seq.Join(item.transform.DOMoveZ(item.transform.position.z + 1,dur/3)).SetEase(ease);
                    
                    StartCoroutine(FallLeaf(item.transform));
                    // item.transform.DOMoveZ(item.transform.position.z + 1,dur/3).OnComplete(()=>item.transform.DOMoveZ(item.transform.position.z -1,dur/3)).OnComplete(()=>item.transform.DOMoveZ(item.transform.position.z ,dur/3));
                }
                else
                {
                    item.transform.DOMoveY(Cauldron.Instance.transform.position.y,.3f);
                }
                Cauldron.Instance.AddRecipe(itemType);
            });
    }
    IEnumerator FallLeaf(Transform target)
    {
        float startZ = target.transform.position.z;
        float curvedPos = 0;
        float t = 0;
        float finalPosZ = target.transform.position.z;
        while(t<= dur)
        {
            var lerpVal = t/dur;
            // curvedPos = Mathf.Lerp(startPos.z,finalPosZ,lerpVal);
            curvedPos = startZ + animationCurve.Evaluate(lerpVal);
            target.transform.position = new Vector3(target.transform.position.x,target.transform.position.y,curvedPos);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
