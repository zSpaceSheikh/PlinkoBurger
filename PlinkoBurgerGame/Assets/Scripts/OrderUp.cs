using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Windows.Speech;
using System.Linq;


public class OrderUp : MonoBehaviour
{
    public GameObject order1Floor;
    public GameObject order1Ceiling;
    public float floorDelay;
    
    private bool serve = true;
    
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private void Start()
    {
        // hid the bin ceiling 
        order1Ceiling.SetActive(false);
        
        // add 'order up' to our voice commands dictionary
        actions.Add("order up", Order1Check);

        // voice recognition stuff
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }
    
    
    void Update()
    {
        // if we press the space bar, and its ready to serve, serve!
        if (Input.GetKeyDown(KeyCode.Space) && serve)
        {
            Order1Check();
        }
    }

    private void Order1Check()
    {
        if (serve)
        {
            order1Floor.SetActive(false);
            order1Ceiling.SetActive(true);

            // activate register sound 
            AudioManager.S.RegisterSound();
            
            serve = false;

            StartCoroutine(SendOrder());
        }
    }

    IEnumerator SendOrder()
    {
        // wait a little bit
        WaitForSeconds delay = new WaitForSeconds(floorDelay);

        yield return delay;
        
        // then turn the order bin floor on
        order1Floor.SetActive(true);
        order1Ceiling.SetActive(false);

        Score.S.readyToCheck = true;
        
        WaitForSeconds delay2 = new WaitForSeconds(0.25f);

        yield return delay2;
        
        Orders.S.newOrder = true;
        serve = true;
    }
    
    
    private void RecognizedSpeech(PhraseRecognizedEventArgs Speech)
    {
        //Debug.Log(Speech.text);
        actions[Speech.text].Invoke();
    }
}
