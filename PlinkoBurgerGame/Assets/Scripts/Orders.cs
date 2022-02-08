using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Orders : MonoBehaviour
{
    public GameObject receipt;
    public GameObject orderText;

    private TextAsset ordersTxt;
    private string[] ordersList;
    private Text order_display;
    public string orderName;

    public string[] ingredientsChecklist;

    public bool newOrder = true;
    private bool firstOrder = true;

    public bool atPointB = false;
    
    // receipt start and end points
    private Vector3 pointAReceipt = new Vector3(-4.6f, -5.85f, -3f);
    private Vector3 pointBReceipt = new Vector3(-4.6f, -1.25f, -3f);
    // order text start and end points
    private Vector3 pointAText = new Vector3(-274f, -116f, -247f);
    private Vector3 pointBText = new Vector3(-274f, 308, -247f);

    public float receiptSpeed = 3f;
    
    private float t;
    private int prevR;

    public TextMeshProUGUI orderBinText;

    public Image timerBar;
    public float firstOrderTime;
    public float otherOrderTime;
    private float orderTime;
    private float timeLeft;
    private float prevTime;


    public static Orders S;
    
    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }
    
    
    void Start()
    {
        order_display = GetComponentInChildren<Text>();
        order_display.text = "";
        StartCoroutine(GetTextFromFile());
        
        // order up text set up
        orderBinText.text = "";
        
        // timer set up
        //timerBar = GetComponent<Image>();
        timeLeft = firstOrderTime;
        orderTime = firstOrderTime;
    }

    IEnumerator GetTextFromFile()
    {
        ordersTxt = Resources.Load("orders") as TextAsset;
        ordersList = ordersTxt.text.Split('\n');
        
        yield return null;
    }

    void Update()
    {
        // always start out by giving the player a very basic order 
        if (firstOrder && newOrder)
        {
            FirstOrder();
            firstOrder = false;
            newOrder = false;
            
            //move the receipt
            StartCoroutine(MoveReceipt());
        }

        // if the player needs the next order, spawn a new order
        if (newOrder)
        {
            NewOrder();
            newOrder = false;
            
            // update the timer to reset
            timeLeft = orderTime;

            //move the receipt
            StartCoroutine(MoveReceipt());
            
            // reset the order up text
            orderBinText.text = "";
        }
        else
        {
            // maybe check for new ingredients to check off from the list?
        }
        
        
        
        
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / orderTime;
        }
        // if the time has run out, get a new order
        else if (timeLeft <= 0)
        {
            // reset the time and bar
            orderTime = otherOrderTime;
            timeLeft = orderTime;
            timerBar.fillAmount = 1;
            
            // reset the order up text
            orderBinText.text = "";
            
            // play a buzzer sound for losing the order
            AudioManager.S.OrderFail();
            
            // get a new order
            NewOrder();
            newOrder = false;
            StartCoroutine(MoveReceipt());
        }
        
        float time = Mathf.FloorToInt(timeLeft % 60);

        //Debug.Log(time);
        if (time < 16)
        {
            // turn on the order up text
            orderBinText.text = "'ORDER UP'"; 
            
            if (prevTime - time == 1)
            {
                AudioManager.S.ClockTicks();
            }
            // update the previous time 
            prevTime = time;
        }

    }


    void FirstOrder()
    {
        // grab the first order
        string[] fullOrder = ordersList[0].Split('=');
        prevR = 0;
        
        // display the order name on the receipt
        orderName = fullOrder[0];
        OrderName.S.SetText();
        
        // make an array of the ingredients for checking the score later
        string temp = fullOrder[1].Replace("\n", "");
        ingredientsChecklist = temp.Split('+');
        
        // display the ingredients on the receipt
        string order = fullOrder[1].Replace("+", "\n");
        order_display.text = order;
        
        // play intro audio clip
        //AudioManager.S.orderVoices[0].Play(0);

    }

    void NewOrder()
    {
        // choose a random order
        int r = Random.Range(1, ordersList.Length);
        if (r == prevR)
        {
            prevR = r;
            r = Random.Range(1, ordersList.Length);
        }
        prevR = r;
        string[] fullOrder = ordersList[r].Split('=');
        
        // display the order name on the receipt
        orderName = fullOrder[0];
        OrderName.S.SetText();
        
        // make an array of the ingredients for checking the score later
        ingredientsChecklist = fullOrder[1].Split('+');
        
        // display the ingredients on the receipt
        string order = fullOrder[1].Replace("+", "\n");
        order_display.text = order;
        
        // play associated audio clip
        AudioManager.S.orderVoices[r-1].Play(0);
        
        // update the ordertime
        orderTime = otherOrderTime;
    }
    
    

    IEnumerator MoveReceipt()
    {
        // receipt print sound
        AudioManager.S.ReceiptPrint();
        
        receipt.transform.position = pointAReceipt;
        orderText.transform.localPosition = pointAText;
        t = 0;
        
        while (atPointB == false)
        {
            // wait a little bit so the receipt moves in steps
            yield return new WaitForSeconds(0.1f);
           //t += Time.deltaTime;
           t += 0.05f;
            
            // find the amount to move the receipt and text
            Vector3 pos = Vector3.Lerp(pointAReceipt, pointBReceipt, t);
            Vector3 posT = Vector3.Lerp(pointAText, pointBText, t);
            
            //Debug.Log(receipt.transform.position.y);
            
            // move the receipt
            receipt.transform.position = pos;
            orderText.transform.localPosition = posT;

            if (receipt.transform.position.y >= pointBReceipt.y)
            {
                atPointB = true;
            }
        }
        atPointB = false;
        yield return null;
    }
    
}
