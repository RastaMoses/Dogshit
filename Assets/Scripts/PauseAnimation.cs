using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAnimation : MonoBehaviour
{
    [SerializeField] Color grungeTransparency;
    Animator animator;
    SpriteRenderer transparency;
    private void Start()
    {

        animator = GetComponent<Animator>();
        transparency = GetComponent<SpriteRenderer>();
        transparency.color = new Color(0,0,0,0);
    }

    public void StartAnimation()
    {
        transparency.color = grungeTransparency;
        animator.Play("Paused");
    }

    public void StopAnimation()
    {
        transparency.color = new Color(1,1,1,0);
        animator.Play("Unpaused");
    }
}
