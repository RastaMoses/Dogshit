using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBlock : MonoBehaviour
{
    [SerializeField] List<RemoteActivator> devicesActivated;
    [SerializeField] Sprite pushedButtonSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0;  i < devicesActivated.Count; i++)
        {
            devicesActivated[i].Activate();
        }
        GetComponent<SpriteRenderer>().sprite = pushedButtonSprite;
    }
}
