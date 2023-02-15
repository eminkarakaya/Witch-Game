using UnityEngine;
using TMPro;
public interface PotionItem
{
    public int count { get; set; }
    public ItemType potionItemType { get; set; }
    
    public GameObject potionTypeImage { get; set; }
    
    public TextMeshProUGUI countText { get; set; }
    
}