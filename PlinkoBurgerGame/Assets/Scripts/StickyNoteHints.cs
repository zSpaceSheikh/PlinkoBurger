using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickyNoteHints : MonoBehaviour
{
    public GameObject sticky;
    public Text stickyText;
    
    private float currentTime = 0f;
    public bool hasSpoken = false;
    
    public static StickyNoteHints S;
    
    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }

    private void Start()
    {
        stickyText.text = "";
        sticky.SetActive(false);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        // Debug.Log(currentTime);

        if (currentTime > 10f && !hasSpoken)
        {
            StartCoroutine(ActivateSticky());
        }
        
    }
    
    IEnumerator ActivateSticky()
    {
        // play a sound so that the player knows something happened
        
        
        // turn on the sticky note and set the text
        sticky.SetActive(true);
        stickyText.text = "Say the name of an ingredient to the plinko machine to get started";
        
        yield return new WaitForSeconds(20f);
        
        sticky.SetActive(false);
        stickyText.text = "";

        hasSpoken = true;
    }
}