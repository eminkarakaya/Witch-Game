using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Inputs;
using TMPro;
public class Jar : MonoBehaviour , PotionItem
{
    #region potionItem
    
    [SerializeField] private GameObject _potionImageType;
    public int count { get; set; }
    public ItemType potionItemType { get; set; }
    public TextMeshProUGUI countText { get; set; }
    
    public GameObject potionTypeImage { get => _potionImageType; set{_potionImageType = value;}}
    #endregion
    
    [SerializeField] private InputData _inputData;
    [SerializeField] private float dur;
    [SerializeField] private Ease ease;
    public ItemType itemType;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] bool isClick = false;
    void OnEnable()
    {
        Cauldron.Instance.clearCauldron += ResetCount;
    }
    void OnDisable()
    {
        Cauldron.Instance.clearCauldron -= ResetCount;
    }
    void Start()
    {
        potionItemType = itemType;
    }
    void Update()
    {
        if(_inputData.IsEnd)
            isClick = false;
    }
    public void Movement()
    {
        if(isClick) return;
        if(!Cauldron.Instance.CheckShake())  return;
        isClick = true;
        var item = Instantiate(itemPrefab,transform.position,Quaternion.identity);
        CreatePotionItemType();
        Cauldron.Instance.AddRecipeItem(item);
        item.transform.DOMove(Camera.main.GetComponent<cam.CamController.CameraController>().potionPos.position + new Vector3(0,2,0), .3f).
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
    public void CreatePotionItemType()
    {
        foreach (var item in ItemTypeHolder.Instance.itemTypes)
        {
            if(item == potionItemType)
            {
                count ++;
                countText.text = count.ToString();
                return;
            }
        }
        var obj = Instantiate(potionTypeImage,ItemTypeHolder.Instance.parent);
        countText = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        ItemTypeHolder.Instance.counts.Add(count);
        ItemTypeHolder.Instance.itemTypes.Add(potionItemType);
        count ++;
        countText.text = count.ToString();
        
    }
    public void ResetCount()
    {
        count = 0;
    }
}
