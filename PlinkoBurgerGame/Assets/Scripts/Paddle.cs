using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;


public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody rigiB;

    public ParticleSystem ketchup;
    public ParticleSystem mustard;

    private float movement;


    void FixedUpdate()
    {
        movement = Input.GetAxisRaw("Horizontal");
        //rigiB.velocity = new Vector3(movement * speed, 0, 0);
        Vector3 forceVec = new Vector3(movement*speed, 0f, 0f);
        rigiB.AddForce(forceVec, ForceMode.Acceleration);
        
        //Debug.Log(movement);
        
        // bumper is moving to the right, shoot ketchup
        if (movement > 0)
        {
            ketchup.Play();
            mustard.Stop();
        }
    
        // bumper is moving to the left, shoot mustard
        if (movement < 0)
        {
            mustard.Play();
            ketchup.Stop();
        }
        
        // bumper is not moving, stop all particle effects
        else
        {
            ketchup.Stop();
            mustard.Stop();
        }
    }
}