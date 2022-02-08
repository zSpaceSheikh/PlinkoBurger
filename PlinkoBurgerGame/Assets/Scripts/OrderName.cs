using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderName : MonoBehaviour
{
    private Text orderName_display;
    
    public static OrderName S;
    
    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }
    
    void Start()
    {
        orderName_display = GetComponentInChildren<Text>();
        orderName_display.text = "";
    }

    public void SetText()
    {
        orderName_display.text = Orders.S.orderName;
    }
}