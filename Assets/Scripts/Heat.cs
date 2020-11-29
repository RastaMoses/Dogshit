using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heat : MonoBehaviour
{

    [SerializeField] Color minColor;
    [SerializeField] Color maxColor;
    [SerializeField] float changeSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ColorFlash();
    }

    void ColorFlash()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(minColor, maxColor,Mathf.PingPong(Time.time, changeSpeed));

    }
}
