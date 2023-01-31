using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : Singleton<GameManager>
{
    private int _gold;
    [SerializeField] private TextMeshProUGUI _goldText;
    private TextParse[] textParses;
    private void Awake()
    {
        SetMoney(0);
    }
    private void OnEnable()
    {
        SetMoney(PlayerPrefs.GetInt("Money"));
    }
    private void OnDisable()
    {
        PlayerPrefs.SetInt("Money",GetMoney());
    }
    public int GetMoney()
    {
        return _gold;
    }
    public void SetMoney(int value)
    {
        _gold += value;
        _goldText.text = CaclText(_gold);
        textParses = FindObjectsOfType<TextParse>();
        for (int i = 0; i < textParses.Length; i++)
        {
            textParses[i].Check(textParses[i].GetComponent<Cost>().GetMoney());
        }
    }
    public static string CaclText(float value)
    {
        if (value == 0)
        {
            return "0";
        }
        if (value < 1000)
        {
            return String.Format("{0:0.0}", value);
        }
        else if (value >= 1000 && value < 1000000)
        {
            return String.Format("{0:0.0}", value / 1000) + "k";
        }
        else if (value >= 1000000 && value < 1000000000)
        {
            return String.Format("{0:0.0}", value / 1000000) + "m";
        }
        else if (value >= 1000000000 && value < 1000000000000)
        {
            return String.Format("{0:0.0}", value / 1000000000) + "b";
        }
        else if (value >= 1000000000000 && value < 1000000000000000)
        {
            return String.Format("{0:0.0}", value / 1000000000000) + "t";
        }
        else if (value >= 1000000000000000 && value < 1000000000000000000)
        {
            return String.Format("{0:0.0}", value / 1000000000000000) + "aa";
        }
        else if (value >= 1000000000000000000)
        {
            return String.Format("{0:0.0}", value / 1000000000000000) + "ab";
        }
        return value.ToString();
    }
}
