using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public float speed;
    public Transform startPos;
    [SerializeField] bool activateOnStart;
    bool activated;
    Vector3 nextPos;
    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (activateOnStart)
        {
            activated = true;
        }
        else
        {
            activated = GetComponentInParent<RemoteActivator>().GetActivated();
        }
        
        if (activated)
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

    
}
