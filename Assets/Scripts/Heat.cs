using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Heat : MonoBehaviour
{

    [SerializeField] Color minColor;
    [SerializeField] Color maxColor;
    
    [SerializeField] float acceleration= 0.2f;

    [SerializeField] float changeAmount; //Serialized for Debug purpose
    PlayerHealth player;
    SpriteRenderer spriteColor;
    
    void Start()
    {
        spriteColor = GetComponent<SpriteRenderer>();
        
        
    }

   
    void Update()
    {
        player = FindObjectOfType<PlayerHealth>();
        ColorFlash();
        
    }

    void ColorFlash()
    {
        if (player != null)
        {

            changeAmount = player.GetColorChange();

            var changeFloat = Math.Pow(Convert.ToDouble(changeAmount), Convert.ToDouble(acceleration));
                spriteColor.color = Color.Lerp(maxColor, minColor,(float)changeFloat);
            
                
                
                
              
            
        }
    }
    
}
