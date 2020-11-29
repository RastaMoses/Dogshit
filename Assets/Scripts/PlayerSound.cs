using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] AudioClip deathSFX;
    [SerializeField] List<AudioClip> jumpSFX;
    [SerializeField] List<AudioClip> landSFX;



    public void PlayJumpSFX()
    {
        int randomSFX = Random.Range(0, jumpSFX.Count);
        var clip = jumpSFX[randomSFX];
        AudioSource.PlayClipAtPoint(clip, gameObject.transform.position);
    }

    public void PlayDeathSFX()
    {
        
        AudioSource.PlayClipAtPoint(deathSFX, gameObject.transform.position);
    }
    public void PlayLandSFX()
    {
        int randomSFX = Random.Range(0, landSFX.Count);
        var clip = landSFX[randomSFX];
        AudioSource.PlayClipAtPoint(clip, gameObject.transform.position);
    }
}
