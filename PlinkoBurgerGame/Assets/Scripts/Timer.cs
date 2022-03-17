using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLength;
    private float currentTime;
    
    private float secPrev = 15f;
    public bool startGameTimer;

    public float orderAddTime = 5f;
    public Light sceneLight;

    //public TextMeshProUGUI countdownText;
    public Text countdownText;
    
    public static Timer S;
    
    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }
    
    void Start()
    {
        //countdownText = FindObjectOfType<TextMeshProUGUI>();
        countdownText.text = "";
        
        currentTime = timeLength;
        
        startGameTimer = false;
    }

    void Update()
    {
        if (startGameTimer)
        {
            if (currentTime > 0)
            {
                currentTime -= 1 * Time.deltaTime;
            }
            else
            {
                currentTime = 0;
                GameEnd();
            }
            //countdownText.text = currentTime.ToString("0");
        
            DiplayTime(currentTime);
        }
    }

    void DiplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        // fix the formatting so it appears in minutes and seconds
        float mins = Mathf.FloorToInt(timeToDisplay / 60);
        float secs = Mathf.FloorToInt(timeToDisplay % 60);
        
        //display the time to the text box
        countdownText.text = string.Format("{0:00}:{1:00}", mins, secs);
        
        
        // sound an alarm for the last 10 seconds
        if (secs < 11 && mins == 0)
        {
            //Debug.Log("secs: " + secs + "  secPrev: " + secPrev);
            if (secPrev - secs == 1)
            {
                if (secs == 0) { AudioManager.S.EndSound(); }
                else
                {
                    // make the alarm sound go off
                    AudioManager.S.TimerAlarm();
                    
                    // flash the lights red on the even numbers
                    if (secs % 2f == 0f)
                    {
                        sceneLight.color = Color.red;
                    }
                    // flash the lights white on the even numbers
                    else{sceneLight.color = Color.white;}
                    
                    
                }
            }
            
            secPrev = secs;
        }
    }
    
    public void AddTime()
    {
        // add time to the timer
        currentTime += orderAddTime;
    }
    

    private void GameEnd()
    {
        SceneManager.LoadScene("EndScene");
    }

}
