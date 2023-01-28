using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Order : MonoBehaviour
{
    [SerializeField] private int count;
    public Sprite potionSprite;
    public ColorType colorType;
    public Image checkImage;
    public TextMeshProUGUI countText;
    void Start()
    {
        GetComponent<Image>().sprite = potionSprite;
        countText.text = count.ToString();
    }

    public void DecreaseCount(List<Order> list)
    {
        count--;
        countText.text = count.ToString();
        if(count == 0)
        {
            countText.enabled = false;
            if (list.Contains(this))
                list.Remove(this);
            checkImage.enabled = true;
        }
    }
}
