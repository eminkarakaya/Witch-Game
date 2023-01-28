using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    public ColorType colorType;
    public Image fillImage;
    public Color color;
    private void Start()
    {
        //switch (colorType)
        //{
        //    case ColorType.Red:
        //        color = Color.red;
        //        break;
        //    case ColorType.Blue:
        //        color = Color.blue;
        //        break;
        //    case ColorType.Purple:
        //        color = new Color(148, 0, 211);
        //        break;
        //    case ColorType.Green:
        //        color = Color.green;
        //        break;
        //    case ColorType.Pink:
        //        color = new Color(255, 0, 255);
        //        break;
        //    default:
        //        break;
        //}
        color = GetComponent<MeshRenderer>().material.GetColor("_Color_1");
    }
    
}
