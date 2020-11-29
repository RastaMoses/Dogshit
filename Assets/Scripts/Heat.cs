using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heat : MonoBehaviour
{

    [SerializeField] Color minColor;
    [SerializeField] Color maxColor;

    float changeAmount;
    PlayerHealth player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
            GetComponent<SpriteRenderer>().color = Color.Lerp(maxColor, minColor, changeAmount);
        }
    }
}
