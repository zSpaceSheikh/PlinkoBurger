using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinIngredientManager : MonoBehaviour
{
    public bool detectedIngredient = false;
    public string detectedIngredientTag;
    
    public static BinIngredientManager S;
    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        detectedIngredient = true;
        Debug.Log("New ingredient in bin = " + collision.gameObject.tag);
        detectedIngredientTag = collision.gameObject.tag;
    }
}
