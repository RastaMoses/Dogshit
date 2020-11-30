using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteActivator : MonoBehaviour
{
    bool activated;
    private void Start()
    {
        activated = false;
    }
    public void Activate()
    {
        activated = true;
    }

    public bool GetActivated()
    {
        return activated;
    }
}
