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

    //State
    bool inSafeZone;
    float health;
    Coroutine safeZoneRegen;
    //Cached Component Reference


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SafezoneHeal()
    {
        while (inSafeZone)
        {
            health += regeneration;
            Mathf.Clamp(health, 0f, maxHealth);
            yield return new WaitForSeconds(regenSpeed);

        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Safezone")
        {
            inSafeZone = true;
            safeZoneRegen = StartCoroutine(SafezoneHeal());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Safezone")
        {
            inSafeZone = false;
            StopCoroutine(safeZoneRegen);
        }
    }
}
