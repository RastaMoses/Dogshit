using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteActivator : MonoBehaviour
{
    [SerializeField] float lightPulseDelay = 0.5f;
    Animator animator;
    bool activated;
    private void Start()
    {
        animator = GetComponent<Animator>();
        activated = false;
    }
    public void Activate()
    {
        activated = true;
    }
    
    public void Deactivate()
    {
        activated = false;
    }

    public bool GetActivated()
    {
        return activated;
        
    }

    public IEnumerator PulseLight()
    {
        yield return new WaitForSeconds(lightPulseDelay);
        animator.SetTrigger("Activate");
        GetComponent<AudioSource>().Play();
    }
}
