using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour
{
    [SerializeField][Range(0,100)] float healAmount = 50f;
    [SerializeField] float regenSpeed = 0.03f;
    [SerializeField] float healIncrements = 5f;
    PlayerHealth player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = FindObjectOfType<PlayerHealth>();
        player.HealthorbHeal(healAmount, regenSpeed, healIncrements);
        Destroy(gameObject);
    }

}
