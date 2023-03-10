using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldAnimBubble : Singleton<GoldAnimBubble>
{
    [SerializeField] private Material mat;
    [SerializeField] GameObject goldAnimPrefab;
    public PotionComplateData potionComplateData;
    public void EarnGoldAnim(int earnedGold, Transform transform)
    {
        var pos = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        var obj = Instantiate(goldAnimPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = GameManager.CaclText(earnedGold);
        obj.transform.DOMove(pos, 1f);
        Color color = new Color(255, 255, 255, 0);
        //obj.GetComponent<SpriteRenderer>().DOColor(color,1);
        obj.transform.GetChild(0).gameObject.GetComponent<Image>().material = Instantiate(mat);
        obj.transform.GetChild(0).gameObject.GetComponent<Image>().material.DOFade(0, 1).OnComplete(() => Destroy(obj)).OnComplete(() => Destroy(obj.transform.GetChild(0).gameObject));
        GameManager.Instance.SetMoney(earnedGold);
    }
    public void PotionComplateAnim(Transform transform,PotionComplateData potionComplateData)
    {
        var pos = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        this.potionComplateData.AssignData(potionComplateData.color,potionComplateData.text,potionComplateData.potionSprite);
        var obj = Instantiate(this.potionComplateData, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.Euler(new Vector3(0,-90,0)));
        obj.transform.DOMove(pos, 1f).OnComplete(()=>Destroy(obj.gameObject));
        // Color color = new Color(255, 255, 255, 0);
        //obj.GetComponent<SpriteRenderer>().DOColor(color,1);
        // obj.transform.GetChild(0).gameObject.GetComponent<Image>().material.DOFade(0, 1).OnComplete(() => Destroy(obj)).OnComplete(() => Destroy(obj.transform.GetChild(0).gameObject));
    }
}
