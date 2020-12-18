using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    
    CharacterController2D playerController;
    private void Awake()
    {
        playerController = GetComponentInParent<CharacterController2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            playerController.m_Grounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool wasGrounded = playerController.m_Grounded;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {


            playerController.m_Grounded = true;
        }
        if (!wasGrounded && playerController.m_Grounded)
        {
            playerController.OnLandEvent.Invoke();
        }
    }
}
