using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBlock : MonoBehaviour
{
    [SerializeField] List<GameObject> devicesActivated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0;  i < devicesActivated.Count; i++)
        {
            devicesActivated
        }
    }
}
