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
    [SerializeField] float deathAnimationDuration = 3f;
    
    //State
    bool inSafeZone;
    bool safe;
    float maxLightIntensity;
    float maxLightRadius;
    [SerializeField] float health; //Serialized for Debug Purpose
    Coroutine safeZoneRegen;
    //Cached Component Reference
    Level level;
    Light2D light;
    

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
        maxLightIntensity = light.intensity;
        maxLightRadius = light.pointLightOuterRadius;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            PlayerDeath();
        }
    }

    void PlayerColor()
    {
        float t = health / maxHealth;
        GetComponent<SpriteRenderer>().color = Color.Lerp(minLifeColor, maxLifeColor, t);
        light.intensity = t * maxLightIntensity;
        light.pointLightOuterRadius = t * maxLightRadius;
        
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
            StartCoroutine(GameOver());
        }
    }
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(deathAnimationDuration);
        FindObjectOfType<SceneLoader>().ReloadLevel();
    }
}
