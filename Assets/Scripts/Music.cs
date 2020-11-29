using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private void Awake()
    {
        int gameObjectCount = FindObjectsOfType<Music>().Length;
        if (gameObjectCount > 1)
        {

            Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ChangeMusic(AudioClip newMusic)
    {
        GetComponent<AudioSource>().clip = newMusic;
        GetComponent<AudioSource>().Play();
    }
}
