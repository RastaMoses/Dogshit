using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBlock : MonoBehaviour
{
    [SerializeField] List<RemoteActivator> devicesActivated;
    [SerializeField] Sprite pushedButtonSprite;
    bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!activated)
        {
            for (int i = 0; i < devicesActivated.Count; i++)
            {
                devicesActivated[i].Activate();
            }
            GetComponent<SpriteRenderer>().sprite = pushedButtonSprite;
            GetComponent<AudioSource>().Play();
            activated = true;
        }
        
    }
}
