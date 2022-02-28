using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Windows.Speech;
using System.Linq;
using System.Reflection;
using Random = UnityEngine.Random;

public class PlinkoChaos : MonoBehaviour
{
    public CameraShake cameraShake;
    
    public ParticleSystem redConfetti;
    public ParticleSystem yellowConfetti;
    public ParticleSystem blueConfetti;
    public ParticleSystem greenConfetti;
    public ParticleSystem confettiExplosion;
    public ParticleSystem rightTrashFire;
    public ParticleSystem leftTrashFire;

    public GameObject pegs;
    
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    
    void Start()
    {
        actions.Add("Oops", Fire);
        actions.Add("Umm", Fire);
        actions.Add("Whoops", Fire);
        actions.Add("Oh no", Fire);

        actions.Add("Sorry", ManagerAudio);
        
        actions.Add("What", Shake);
        actions.Add("Come on", Shake);
        
        actions.Add("Fuck", ConfettiExplosion);
        actions.Add("Shit", YellowConfetti);
        actions.Add("Damn", RedConfetti);
        actions.Add("Bitch", BlueConfetti);
        actions.Add("Hell", GreenConfetti);
        
        actions.Add("Oh god", MissingPegs);
        actions.Add("Oh gosh", MissingPegs);
        actions.Add("Oh my god", MissingPegs);
        actions.Add("OMG", MissingPegs);
        
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
    
    private void Fire()
    {
        Debug.Log("Kitchen Fire!");
        
        AudioManager.S.FireCrackle();
        StartCoroutine(FireForABit());
    }
    
    private void ConfettiExplosion()
    {
        Debug.Log("Confetti!");
        
        confettiExplosion.Play();
        AudioManager.S.PartyHorn();
    }
    
    private void BlueConfetti()
    {
        Debug.Log("Confetti!");
        
        blueConfetti.Play();
        AudioManager.S.PartyHorn();
    }
    
    private void GreenConfetti()
    {
        Debug.Log("Confetti!");
        
        greenConfetti.Play();
        AudioManager.S.PartyHorn();
    }
    
    private void YellowConfetti()
    {
        Debug.Log("Confetti!");
        
        yellowConfetti.Play();
        AudioManager.S.PartyHorn();
    }
    
    private void RedConfetti()
    {
        Debug.Log("Confetti!");
        
        redConfetti.Play();
        AudioManager.S.PartyHorn();
    }
    
    private void ManagerAudio()
    {
        Debug.Log("Grumpy Manager");
    }
    
    private void Shake()
    {
        Debug.Log("ScreenShake");
        AudioManager.S.ShakeWhoosh();
        StartCoroutine(cameraShake.Shake(0.5f, 0.1f));
    }
    
    private void MissingPegs()
    {
        Debug.Log("MissingPegs");
        AudioManager.S.PegPop();
        StartCoroutine(RemovePegs());
    }
    
    IEnumerator RemovePegs()
    {
        pegs.SetActive(false);
        
        yield return new WaitForSeconds(3f);
        
        pegs.SetActive(true);
    }

    IEnumerator FireForABit()
    {
        rightTrashFire.Play();
        leftTrashFire.Play();
        
        yield return new WaitForSeconds(3f);
        
        rightTrashFire.Stop();
        leftTrashFire.Stop();
    }

}
