using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class PlayerHealth : MonoBehaviour
{
    //Config Params

    [SerializeField] float maxHealth = 100f;
    [SerializeField] float regeneration = 10f;
    [SerializeField] float regenSpeed = 0.2f;
    [Header("Colors")]
    [SerializeField] Color maxLifeColor;
    [SerializeField] Color minLifeColor;
    [Header("Misc")]
    [SerializeField] GameObject playerBlockPrefab;
    
    //State
    bool inSafeZone;
    bool safe;
    
    [SerializeField] float health; //Serialized for Debug Purpose
    Coroutine safeZoneRegen;
    //Cached Component Reference
    Level level;
    
    

    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<Level>();
        health = maxHealth;
        StartCoroutine(Melt());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerColor();
        CheckPlayerHealth();
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
        GetComponent<Light2D>().intensity = t;
    }

    void CheckPlayerHealth()
    {
        if (health <= 0)
        {
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        if (level.GetRespawn() == true)
        {
            Instantiate(playerBlockPrefab, transform.position,Quaternion.identity);
            
            level.RespawnPlayer();
            Destroy(gameObject);
            gameObject.SetActive(false);
            
        }
        else
        {
            StartCoroutine(level.ResetLevel());
        }
    }
}
