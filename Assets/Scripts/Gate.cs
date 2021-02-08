using UnityEngine;
using System.Collections;
public class Gate : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public float speed;
    public bool startToPos1;
    [SerializeField] bool activateOnStart = false;
    [SerializeField] bool delayBeforeMove = false;
    [SerializeField] float delay = 0f;
    [SerializeField] AudioSource audioSource;
    //State
    
    RemoteActivator remoteActivator;
    bool addDelay;
    bool activated;
    Vector3 nextPos;
    bool pulsePlayed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInParent<RemoteActivator>())
        {
            remoteActivator = GetComponentInParent<RemoteActivator>();
        }
        else
        {
            Debug.LogError(gameObject.name + "No Remote Activator Component found");
        }
        activated = false;
        if (activateOnStart)
        {
            activated = true;
            
        }

        if (startToPos1)
        {
            
            nextPos = pos1.position;
            
        }
        else
        {
            
            nextPos = pos2.position;
            
        }
        
        if (delayBeforeMove)
        {
            addDelay = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!activateOnStart)
        {
            activated = remoteActivator.GetActivated();
            
        }
        
        if (activated)
        {
            if (addDelay)
            {
                StartCoroutine(MoveWithDelay());
            }
            else
            {
                if (!pulsePlayed)
                {
                    StartCoroutine(remoteActivator.PulseLight());
                    pulsePlayed = true;
                    audioSource.Play();
                }
                Move();
            }
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
        else if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
            
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        
    }

    
}
