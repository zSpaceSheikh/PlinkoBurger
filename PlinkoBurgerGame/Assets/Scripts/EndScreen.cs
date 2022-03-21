using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.SceneManagement;
//using TMPro;
using UnityEngine.UI;


public class EndScreen : MonoBehaviour
{
    
    public TextMeshPro pointsText;
    public TextMeshPro ordersText;
    
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    
    // Start is called before the first frame update
    void Start()
    {
        // set the points and number of orders values on the screen
        pointsText.text = Score.S.playerScore.ToString();
        ordersText.text = Score.S.numOrders.ToString();
        
        
        actions.Add("Thanks come again", GameStartScreen);
        actions.Add("Come again", GameStartScreen);
        actions.Add("Next Shift", GameStartScreen);
        actions.Add("Have a nice day", GameStartScreen);

        // voice recognition stuff
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }
    
    private void GameStartScreen()
    {
        //AudioManager.S.EndSound();
        SceneManager.LoadScene("StartScene");
    }
    
    private void RecognizedSpeech(PhraseRecognizedEventArgs Speech)
    {
        //Debug.Log(Speech.text);
        actions[Speech.text].Invoke();
    }
}