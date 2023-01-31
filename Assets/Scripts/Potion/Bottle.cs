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
   
    Pink,
    Cyan,
    Brown,
    White,
    Black,
    Yellow,
    Green
}
public class Bottle : MonoBehaviour
{
    [SerializeField] private List<ColorType> halfColors = new List<ColorType>();
    [SerializeField] private Transform parent;
    [SerializeField] private Vector3 pos;
    [SerializeField] private List<ColorTypeClass> colorTypes;
    [SerializeField] private Material materialPrefab;
    [SerializeField] private float capacity, full, fillAmount;
    [SerializeField] private float lerp;
    private Image _currentImage;
    private ColorTypeClass currentColorTypeClass;
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
        //currentColorTypeClass = null;
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
        if(currentColorTypeClass.half)
        {
            if(!halfColors.Contains(currentColorTypeClass.colorType))
            {
                halfColors.Add(currentColorTypeClass.colorType);
                if(halfColors.Count == 2)
                {
                    colorType = MixColors(halfColors[0], halfColors[1]);
                    Debug.Log("mixed + " + colorType);
                }
            }
        }
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
    public bool GetCurrentColorFull()
    {
        if (currentColorTypeClass == null)
            return false;
        return currentColorTypeClass.full;
    }
    public ColorType MixColors(ColorType colorType1, ColorType colorType2)
    {
        switch (colorType1)
        {
            
            case ColorType.Red:
                switch (colorType2)
                {
                    case ColorType.Null:
                        return ColorType.Null;
                    case ColorType.Red:
                        return ColorType.Red;
                    case ColorType.Blue:
                        return ColorType.Purple;
                    case ColorType.Purple:
                        return ColorType.Pink;
                    case ColorType.Green:
                        return ColorType.Yellow;
                    case ColorType.Pink:
                        return ColorType.Pink;
                    case ColorType.White:
                        return ColorType.Pink;

                    default:
                        break;
                }
                break;
            case ColorType.Blue:
                switch (colorType2)
                {
                    case ColorType.Null:
                        return ColorType.Null;
                    case ColorType.Red:
                        return ColorType.Purple;
                    case ColorType.Blue:
                        return ColorType.Blue;
                    case ColorType.Purple:
                        return ColorType.Null;
                    case ColorType.Green:
                        return ColorType.Cyan;
                    case ColorType.Pink:
                        return ColorType.Purple;
                    default:
                        break;
                }
                break;
            case ColorType.Purple:
                switch (colorType2)
                {
                    case ColorType.Null:
                        return ColorType.Null;
                    case ColorType.Red:
                        return ColorType.Pink;
                    case ColorType.Blue:
                        return ColorType.Purple;
                    case ColorType.Purple:
                        return ColorType.Purple;
                    case ColorType.Green:
                        return ColorType.Brown;
                    case ColorType.Pink:
                        return ColorType.Purple;
                    default:
                        break;
                }
                break;
            case ColorType.Green:
                switch (colorType2)
                {
                    case ColorType.Null:
                        return ColorType.Null;
                    case ColorType.Red:
                        return ColorType.Yellow;
                    case ColorType.Blue:
                        return ColorType.Cyan;
                    case ColorType.Purple:
                        return ColorType.Brown;
                    case ColorType.Green:
                        return ColorType.Green;
                    case ColorType.Pink:
                        return ColorType.Null;
                    default:
                        break;
                }
                break;
            case ColorType.Pink:
                switch (colorType2)
                {
                    case ColorType.Null:
                        return ColorType.Null;
                    case ColorType.Red:
                        return ColorType.Pink;
                    case ColorType.Blue:
                        return ColorType.Purple;
                    case ColorType.Purple:
                        return ColorType.Purple;
                    case ColorType.Green:
                        return ColorType.Null;
                    case ColorType.Pink:
                        return ColorType.Pink;
                    
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        return ColorType.Null;
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

