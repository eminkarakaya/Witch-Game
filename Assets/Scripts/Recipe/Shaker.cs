using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class Shaker : MonoBehaviour , PotionItem
{
    #region potionItem
    
    [SerializeField] private GameObject _potionImageType;
    public int count { get; set; }
    public ItemType potionItemType { get; set; }
    [SerializeField] private TextMeshProUGUI _countText;
    public TextMeshProUGUI countText { get =>_countText ; set{_countText = value;} }
    
    public GameObject potionTypeImage { get => _potionImageType; set{_potionImageType = value;}}
    #endregion
    public GameObject itemTypeImage;
    public ItemType itemType;
    public Vector3 pos,rot;
    public Transform parentSalt;
    [SerializeField] private Transform _head;
    [SerializeField] private GameObject _dustPrefab;
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
        
        pos = transform.position;
        rot = transform.rotation.eulerAngles;
    }
    public void ResetCount()
    {
        count = 0;
    }

    public void SetParentSalt(Transform transform)
    {
        parentSalt.transform.SetParent(transform);
    }
    public void CreateDustPrefab()
    {
        var obj = Instantiate(_dustPrefab,_head.position,Quaternion.identity);
        Cauldron.Instance.AddRecipeItem(obj);
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
        ItemTypeHolder.Instance.itemTypes.Add(potionItemType);
        ItemTypeHolder.Instance.counts.Add(count);
        countText = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        count ++;
        Debug.Log(count + " count " + countText.text);
        countText.text = count.ToString();
        
    }
}

