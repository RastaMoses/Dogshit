using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour
{
    [SerializeField][Range(0,100)] float healAmount = 50;
    PlayerHealth player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = FindObjectOfType<PlayerHealth>();
        player.Heal(healAmount);
        Destroy(gameObject);
    }

}
