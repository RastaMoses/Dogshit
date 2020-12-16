using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] float delayBetweenJumps = 0.05f;
	

	const float k_GroundedRadius = 0.6f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private bool m_Jump;
	private Rigidbody2D m_Rigidbody2D;
	 // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
		m_Jump = true;
		
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
		bool wasGrounded = m_Grounded;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
			m_Grounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		bool wasGrounded = m_Grounded;

		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
			
			
			m_Grounded = true;
		}
		if (!wasGrounded && m_Grounded)
        {
			OnLandEvent.Invoke();
		}


			

	}
    public void Move(float move, bool crouch, bool jump)
	{
		

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			
			

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			if (m_Jump)
            {
				Debug.Log("Jump");
				StartCoroutine(Jump());

			}
			// Add a vertical force to the player.
			
		}
	}

	private IEnumerator Jump()
    {
		
		m_Grounded = false;
		m_Jump = false;
		m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		GetComponent<PlayerSound>().PlayJumpSFX();
		yield return new WaitForSeconds(delayBetweenJumps);
		m_Jump = true;
    } 

	
}
