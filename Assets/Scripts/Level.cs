using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] float levelDamage = 5f;
    [SerializeField] float damageInterval = 1f;
    public float GetLevelDamage()
    {
        return levelDamage;
    }

    public float GetDamageInterval()
    {
        return damageInterval;
    }
}
