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
    [SerializeField] float resetSpeed = 10f;
    [Header("Colors")]
    [SerializeField] Color maxLifeColor;
    [SerializeField] Color minLifeColor;
    [SerializeField] float minLight = 0.2f;
    [Header("Misc")]
    [SerializeField] GameObject playerBlockPrefab;
    [SerializeField] float deathAnimationDuration = 3f;

    //State
    bool killingSelf;
    bool inSafeZone;
    bool safe;
    float maxLightIntensity;
    float maxLightRadius;
    [SerializeField] float health; //Serialized for Debug Purpose
    Coroutine safeZoneRegen;
    //Cached Component Reference
    Level level;
    Light2D playerLight;
    float colorChange;

    // Start is called before the first frame update
    void Start()
    {
        playerLight = GetComponent<Light2D>();
        maxLightIntensity = playerLight.intensity;
        maxLightRadius = playerLight.pointLightOuterRadius;
        level = FindObjectOfType<Level>();
        health = maxHealth;
        StartCoroutine(Melt());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerColor();
        CheckPlayerHealth();
        KillSelf();
    }

    private IEnumerator SafezoneHeal()
    {
        while (inSafeZone)

        {
            if (killingSelf)
            {
                yield return new WaitForSeconds(regenSpeed);
            }
            else
            {
                health += regeneration;
                health = Mathf.Clamp(health, 0f, maxHealth);
                yield return new WaitForSeconds(regenSpeed);

            }

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


    public float GetColorChange()
    {
        return colorChange;
    }
    void PlayerColor()
    {
        colorChange = health / maxHealth;
        GetComponent<SpriteRenderer>().color = Color.Lerp(minLifeColor, maxLifeColor, colorChange);
        playerLight.intensity = colorChange * maxLightIntensity;
        playerLight.pointLightOuterRadius = colorChange * maxLightRadius + minLight;
        
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
        GetComponent<PlayerSound>().PlayDeathSFX();
        if (level.GetRespawn() == true)
        {
            Instantiate(playerBlockPrefab, transform.position,Quaternion.identity);
            
            level.RespawnPlayer();
            Destroy(gameObject);
            gameObject.SetActive(false);
            
        }
        else
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            
            StartCoroutine(GameOver());
        }
    }
    private IEnumerator GameOver()
    {
        
        yield return new WaitForSeconds(deathAnimationDuration);
        FindObjectOfType<SceneLoader>().ReloadLevel();
    }

    void KillSelf()
    {
        if (Input.GetKey(KeyCode.R))
        {
            killingSelf = true;
            health = health - (resetSpeed  * Time.deltaTime);
        }
        else
        {
            killingSelf = false;
        }
    }
    private IEnumerator Heal(float healAmount,float regenSpeed, float healIncrements)
    {
        for (float i = 0; i < healAmount; i += healIncrements)
        {
            health += healIncrements;
            health = Mathf.Clamp(health, 0, 100);
            yield return new WaitForSeconds(regenSpeed);
        }
    }

    public void HealthorbHeal(float healAmount, float regenSpeed, float healIncrements)
    {
        StartCoroutine(Heal(healAmount, regenSpeed, healIncrements));
    }
}
