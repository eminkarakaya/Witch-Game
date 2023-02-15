using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] private float _dur;
    Image img;
    TextMeshProUGUI popupText;
    public const string POTION_ADDED_TEXT = " potion added to formula book.";
    void Start()
    {
        img = GetComponent<Image>();
        popupText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetPopupText(string text)
    {
        popupText.text = text;
        popupText.DOFade(1,0).OnComplete(()=>popupText.DOFade(1,2).OnComplete(()=>popupText.DOFade(0,_dur)));
        img.DOFade(1,0).OnComplete(()=>img.DOFade(1,2).OnComplete(()=>img.DOFade(0,_dur)));
    }
}
