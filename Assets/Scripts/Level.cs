﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //Config params
    [SerializeField] float levelDamage = 5f;
    [SerializeField] float damageInterval = 1f;
    [SerializeField] int respawnLimit = 3;
    [SerializeField] Transform respawnPoint;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] float respawnDelay = 2f;

    //States
    int respawnCount;


    private void Start()
    {
        respawnCount = 0;
    }
    public float GetLevelDamage()
    {
        return levelDamage;
    }

    public float GetDamageInterval()
    {
        return damageInterval;
    }

    public bool GetRespawn()
    {
        bool allowRespawn;
        if (respawnCount>= respawnLimit)
        {
            allowRespawn = false;
            
        }
        else
        {
            allowRespawn = true;
            respawnCount++;
        }
        return allowRespawn;
    }

    
    

    public void RespawnPlayer()
    {

        StartCoroutine(WaitForRespawn());
        
        
    }
    private IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        Instantiate(playerPrefab, respawnPoint.position, respawnPoint.rotation);
    }

    public IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(respawnDelay);

    }
}
