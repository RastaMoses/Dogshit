﻿using UnityEngine;
using System.Collections;
public class Gate : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public float speed;
    public Transform startPos;
    [SerializeField] bool delayBeforeMove = false;
    [SerializeField] float delay = 0f;

    //State
    bool addDelay;
    Vector3 nextPos;
    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
        if (delayBeforeMove)
        {
            addDelay = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (addDelay)
        {
            StartCoroutine(MoveWithDelay());
        }
        else
        {
            Move();
        }
        
    }
    IEnumerator MoveWithDelay()
    {
        
        yield return new WaitForSeconds(delay);
        addDelay = false;
        
    }
    
    private void Move()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }
}
