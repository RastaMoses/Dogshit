using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnder : MonoBehaviour
{

    SceneLoader sceneLoader;

    void OnTriggerEnter2D (Collider2D collision)
    {
        Debug.Log("Player Triggers");
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.LoadNextLevel();
        
    }
}
