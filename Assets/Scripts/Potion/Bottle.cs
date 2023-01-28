using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ColorType
{
    Null,
    Red,
    Blue,
    Purple,
    Green,
    Pink
}
public class Bottle : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Vector3 pos;
    [SerializeField] private List<ColorTypeClass> colorTypes;
    [SerializeField] private Material materialPrefab;
    [SerializeField] private float capacity, full, fillAmount;
    [SerializeField] private float lerp;
    private Image _currentImage;
    [SerializeField] private Color? _currentColor  = null;
    public ColorType colorType;
    public Potion potion;
    private void OnEnable()
    {
        StageManager.Instance.onSwipeLeft += CheckPotionImageActive;
        StageManager.Instance.onSwipeRight += CheckPotionImageActive;
    }
    private void OnDisable()
    {
        StageManager.Instance.onSwipeLeft -= CheckPotionImageActive;
        StageManager.Instance.onSwipeRight -= CheckPotionImageActive;
        
    }
    private void Start()
    {
        potion = GetComponent<Potion>();
        GetComponent<MeshRenderer>().material = Instantiate(materialPrefab);
        GetComponent<MeshRenderer>().material.SetFloat("_Fill",-.5f);
    }
    public void FillBottle(Potion potion)
    {
        if (full >= capacity)
            return;
        ColorTypeClass currentColorTypeClass = null;
        foreach (var item in colorTypes)
        {
            if (potion.colorType == item.colorType)
            {
                currentColorTypeClass = item;
                break;
            }
        }
        
        if (_currentColor == null || _currentColor != currentColorTypeClass.color)
        {
            _currentImage = Instantiate(potion.fillImage, Vector3.zero, Quaternion.identity, parent);
            _currentImage.transform.SetSiblingIndex(0);
            _currentImage.GetComponent<RectTransform>().anchoredPosition = pos;
        }
        _currentColor = currentColorTypeClass.color;
        _currentImage.fillAmount = (full / capacity);
        full += fillAmount;
        currentColorTypeClass.Amount += fillAmount;
        if (currentColorTypeClass.full)
        {
            colorType = currentColorTypeClass.colorType;
        }
        GetComponent<MeshRenderer>().material.SetFloat("_Fill", (full / capacity) - .5f);
        this.potion.color = Color.Lerp(this.potion.color, potion.color, fillAmount / full);
        GetComponent<MeshRenderer>().material.SetColor("_Color_1",this.potion.color);
        GetComponent<MeshRenderer>().material.SetColor("_SideColor",this.potion.color);
    }
   
    private void CheckPotionImageActive()
    {
        if (StageManager.Instance.index == 0)
        {
            parent.gameObject.SetActive(true);
        }
        else if (StageManager.Instance.index == 1)
        {
            parent.gameObject.SetActive(false);
        }
    }
    public void TogglePotionImage(bool value)
    {
        parent.gameObject.SetActive(value);
    }
}
[System.Serializable]
public class ColorTypeClass
{
    public bool half, full;
    public ColorType colorType;
    public Color color;
    private float amount;
    private float halfAmount = 35, fullAmount = 70;
    public float Amount
    {
        get { return amount; }
        set 
        { 
            amount = value; 
            if(amount >= halfAmount)
                half = true;
            else
                half = false;
            
            if (amount >= fullAmount)
                full = true;
            else
                full = false;
        }
    }
}

