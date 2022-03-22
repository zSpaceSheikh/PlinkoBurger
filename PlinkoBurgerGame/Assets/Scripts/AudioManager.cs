using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public AudioSource startScreenMusic;
    public AudioSource endScreenMusic;
    public AudioSource backgroundNoise;
    public AudioSource spawnDing;
    public AudioSource receiptPrint;
    public AudioSource registerOrder;
    public AudioSource timerAlarm;
    public AudioSource endSound;
    public AudioSource clockTick;
    public AudioSource orderFail;
    public AudioSource fireCrackle;
    public AudioSource partyHorn;
    public AudioSource pop;
    public AudioSource whoosh;
    public AudioSource trash;

    public AudioSource[] countdown;

    public AudioSource[] orderVoices;
    
    public static AudioManager S;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }
    
    
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    PlayerHitSound();
        //}
    }
    
    public void StartScreenMusic()
    {
        startScreenMusic.Play();
    }

    public void EndScreenMusic()
    {
        endScreenMusic.Play();
    }

    public void BackgroundNoise()
    {
        backgroundNoise.Play();
    }
    
    public void SpawnDing()
    {
        float wobble = Random.Range(0.85f,1.15f);
        spawnDing.pitch = wobble;
        spawnDing.PlayOneShot(spawnDing.clip, 1f);
    }
    
    public void ReceiptPrint()
    {
        receiptPrint.Play();
    }
    
    public void RegisterSound()
    {
        registerOrder.Play();
    }
    
    public void TimerAlarm()
    {
        timerAlarm.Play();
    }
    
    public void EndSound()
    {
        endSound.Play();
    }
    
    public void ClockTicks()
    {
        clockTick.Play();
    }
    
    public void OrderFail()
    {
        orderFail.Play();
    }
    
    public void FireCrackle()
    {
        fireCrackle.Play();
    }
    
    public void PartyHorn()
    {
        partyHorn.Play();
    }
    public void PegPop()
    {
        pop.Play();
    }
    public void ShakeWhoosh()
    {
        whoosh.Play();
    }
    
    public void TrashCompact()
    {
        trash.Play();
    }
}