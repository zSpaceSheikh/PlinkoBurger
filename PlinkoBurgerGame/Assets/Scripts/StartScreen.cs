using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public GameObject lights1;
    public GameObject lights2;
    private bool swapScene;
    private GameObject test;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        // set the bool check for the lights
        swapScene = false;
        
        // add the start key words
        actions.Add("Welcome to plinko burger, home of the plinko burger, can I take your order?", GameStart);
        actions.Add("game start", GameStart);
        
        // start the music
        AudioManager.S.StartScreenMusic();
        
        // voice recognition stuff
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
        
        // set the lights 
        lights1.SetActive(true);
        lights2.SetActive(true);
        
        // activate the blinking lights coroutine
        StartCoroutine(BlinkLights());
    }


    private void GameStart()
    {
        AudioManager.S.EndSound();
        AudioManager.S.startScreenMusic.Stop();
        SceneManager.LoadScene("PlinkoBurgerSingleOrder");
    }
    
    private void RecognizedSpeech(PhraseRecognizedEventArgs Speech)
    {
        swapScene = true;
        Debug.Log(Speech.text);
        actions[Speech.text].Invoke();
    }


    IEnumerator BlinkLights()
    {
        //Debug.Log("Entered BlinkLights()");
        
        // swap the lights
        lights1.SetActive(false);
        lights2.SetActive(true);
        
        // wait a bit
        yield return new WaitForSeconds(1.5f);
        
        // swap the lights back
        lights1.SetActive(true);
        lights2.SetActive(false);
        
        // wait a bit
        yield return new WaitForSeconds(1.5f);

        if (!swapScene)
        {
            //Debug.Log("Re-entering BlinkLights()");
            StartCoroutine(BlinkLights());
        }
        //Debug.Log("Exiting BlinkLights()");
    }
    
}
