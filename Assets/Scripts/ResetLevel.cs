using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevel : MonoBehaviour
{
    [SerializeField] float pressKeyDuration = 2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reset());
        }
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(pressKeyDuration);
        if (Input.GetKey(KeyCode.R))
        {
            GetComponent<SceneLoader>().ReloadLevel();
        }
        
    }
}
