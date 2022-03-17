using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    
    void Start()
    {
        actions.Add("Exit the game please", ExitGame);
        actions.Add("I want to quit Plinko Burger", QuittingPlinkoBurger);
        
        // voice recognition stuff
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }
    
    private void RecognizedSpeech(PhraseRecognizedEventArgs Speech)
    {
        //Debug.Log(Speech.text);
        actions[Speech.text].Invoke();
    }
    
    
    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void QuittingPlinkoBurger()
    {
        // play a buzzer sound for quitting the game
        AudioManager.S.OrderFail();
        
        SceneManager.LoadScene("StartScene");
    }
}