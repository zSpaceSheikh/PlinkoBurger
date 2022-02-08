using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private List<string> _collectedIngredients = new List<string>();

    public bool readyToCheck = false;
    
    public int playerScore = 0;
    public int numOrders = 0;

    public static Score S;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }

    void Update()
    {
        if (readyToCheck)
        {
            Debug.Log("Ingredients:" + _collectedIngredients.Count);
            Debug.Log("Order List:" + Orders.S.ingredientsChecklist.Length);
            
            // add 1 to the total number of completed orders
            numOrders++;

            // add a little time if they submitted an order with stuff in it
            if (_collectedIngredients.Count > 1)
            {
                Timer.S.AddTime();
            }
            
            // for each ingredient we collected check against each ingredient in the order
            for (int i=0; i < _collectedIngredients.Count; i++)
            {
                for (int j = 0; j < Orders.S.ingredientsChecklist.Length; j++)
                {
                    Debug.Log(":" + _collectedIngredients[i] + Orders.S.ingredientsChecklist[j] +":");
                    
                    if (_collectedIngredients[i] == Orders.S.ingredientsChecklist[j])
                    {
                        playerScore += 1;
                    }
                    
                    Debug.Log("score: " + playerScore);
                }
            }

            readyToCheck = false;
            _collectedIngredients.Clear();
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Checking order...");
        
        _collectedIngredients.Add(collision.gameObject.tag);
        Destroy(collision.gameObject);
    }
}