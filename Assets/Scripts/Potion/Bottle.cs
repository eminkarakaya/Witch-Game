using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ColorType
{
    Red,
    Blue,
    Purple,
    Green,
    Pink
}
public class Bottle : MonoBehaviour
{
    [SerializeField] private List<ColorTypeClass> colorTypes;
    [SerializeField] private Material materialPrefab;
    [SerializeField] private float capacity, full, fillAmount;
    public Potion potion;
    float totalRed = 0f;
    float totalGreen = 0f;
    float totalBlue = 0f;
    [SerializeField] private float lerp;

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
            }
        }

        full += fillAmount;
        currentColorTypeClass.amount += fillAmount;
        GetComponent<MeshRenderer>().material.SetFloat("_Fill", (full / capacity) - .5f);
        this.potion.color = Color.Lerp(this.potion.color, potion.color, fillAmount / full);
        GetComponent<MeshRenderer>().material.SetColor("_Color_1",this.potion.color);
        GetComponent<MeshRenderer>().material.SetColor("_SideColor",this.potion.color);
    }
   
    public void SetColor(List<Potion> colors)
    {
        Color result = new Color(0, 0, 0);
        foreach (var item in colors)
        {
            result += item.color;
        }
    }

    [ContextMenu("Mix")]
    public void Mix()
    {
        potion.color = Color.Lerp(potion.color, colorTypes[1].color, colorTypes[1].amount/full);
        GetComponent<MeshRenderer>().material.SetColor("_SideColor",potion.color);
        GetComponent<MeshRenderer>().material.SetColor("_Color_1", potion.color);
    }

}
[System.Serializable]
public class ColorTypeClass
{
    public ColorType colorType;
    public Color color;
    public float amount;
}

