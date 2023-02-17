using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PotionComplateData : MonoBehaviour
{
    public Color color;
    public TextMeshProUGUI textMeshPro;
    public string text;
    public Sprite potionSprite;
    public Image image;
    public Image parentImage;
    public PotionComplateData(Color color,string text,Sprite potionSprite)
    {
        this.color = color;
        this.text = text;
        this.potionSprite = potionSprite;
    }
    public void AssignData(Color color,string text,Sprite potionSprite)
    {
        this.color = color;
        this.textMeshPro.text = text;
        this.potionSprite = potionSprite;
        image.sprite = this.potionSprite;
        parentImage.color = color;
    }
}
