using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] EnemyAI enemy;

   void OnTriggerEnter2D(Collider2D collider)
    {
        enemy.FollowPlayer();
    }
}
