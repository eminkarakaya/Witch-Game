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
    private float capacity, full, fillAmount = 1;
    [SerializeField] private float lerp,halfFull;
    private Image _currentImage;
    // private float halfFull;
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
        SetCapacity();
        GetComponent<MeshRenderer>().material.SetFloat("_Fill",-(halfFull));
        full = -halfFull;
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
        GetComponent<MeshRenderer>().material.SetFloat("_Fill", ((full / capacity) * halfFull*2) - (halfFull));
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






    // [ContextMenu("CalculateVolume")]

    float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v321 = p3.x * p2.y * p1.z;
        float v231 = p2.x * p3.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;
        float v132 = p1.x * p3.y * p2.z;
        float v213 = p2.x * p1.y * p3.z;
        float v123 = p1.x * p2.y * p3.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }

    float VolumeOfMesh(Mesh mesh)
    {
        float volume = 0;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }
        float scale  = transform.localScale.y;
        Transform currentTransform = this.transform;
        while(true)
        {
            if(currentTransform.parent != null)
            {
                scale *= currentTransform.parent.localScale.y;
                currentTransform = currentTransform.parent;
            }
            else
                break;
        }
        return Mathf.Abs(volume * scale);
    }
    



    private void SetCapacity()
    {
        
        capacity = halfFull*2*100;
        }
    //
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

