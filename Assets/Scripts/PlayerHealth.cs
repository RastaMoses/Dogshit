using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Config Params

    [SerializeField] float maxHealth = 100f;
    [SerializeField] float regeneration = 10f;
    [SerializeField] float regenSpeed = 0.2f;
    [Header("Colors")]
    [SerializeField] Color maxLifeColor;
    [SerializeField] Color minLifeColor;
    //State
    bool inSafeZone;
    bool safe;
    [SerializeField] float health; //Serialized for Debug Purpose
    Coroutine safeZoneRegen;
    //Cached Component Reference


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        StartCoroutine(Melt());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerColor();
    }

    private IEnumerator SafezoneHeal()
    {
        while (inSafeZone)
        {
            health += regeneration;
            health = Mathf.Clamp(health, 0f, maxHealth);
            yield return new WaitForSeconds(regenSpeed);

        }

        

    }

    IEnumerator Melt()
    {
        var level = FindObjectOfType<Level>();
        while(true)
        {
            while (!safe)
            {

                health -= level.GetLevelDamage();
                yield return new WaitForSeconds(level.GetDamageInterval());
            }
            yield return new WaitForSeconds(level.GetDamageInterval());
        }
        

        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Safezone")
        {
            inSafeZone = true;
            safe = true;
            safeZoneRegen = StartCoroutine(SafezoneHeal());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Safezone")
        {
            inSafeZone = false;
            safe = false;
            StopCoroutine(safeZoneRegen);
        }
    }

    void PlayerColor()
    {
        float t = health / maxHealth;
        GetComponent<SpriteRenderer>().material.color = Color.Lerp(minLifeColor, maxLifeColor, t);
    }


}
