using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb2d;

    //http://stackoverflow.com/questions/27712233/swipe-gestures-on-android-in-unity



    //Float for basic player movements
    public float PlayerSpeed = 8.0f;
    private float JumpHeight;
    private float DashSpeed;
    public float FlyingTime;
	private float DashAmount;

    //Bools for checking for the different playerstates
    private bool PlayerAlive;
    public bool grounded;
    private bool CanJump;
    private bool CanFly;
    private bool CanDash;

    //Bools for holding info on what each playerstate does
    private bool Jump;
    private bool Dash;
    private bool Fly;
    private bool SwitchUp;
    private bool SwitchDown;


	//init for Lean touch input

	protected virtual void OnEnable()
	{
		// Hook into the OnFingerTap event
		Lean.LeanTouch.OnFingerTap += OnFingerTap;

		// Hook into the OnSwipe event
		Lean.LeanTouch.OnFingerSwipe += OnFingerSwipe;

		//Hook into the OnFingerHeldDown event
//		Lean.LeanTouch.OnFingerHeldDown += OnFingerHeldDown;
	}
	
	protected virtual void OnDisable()
	{
		// Unhook into the OnFingerTap event
		Lean.LeanTouch.OnFingerTap -= OnFingerTap;

		// Hook into the OnSwipe event
		Lean.LeanTouch.OnFingerSwipe -= OnFingerSwipe;

		//Hook into the OnFingerHeldDown event
//		Lean.LeanTouch.OnFingerHeldDown -= OnFingerHeldDown;
	}

    // Use this for initialization
    void Start()
    {
        JumpHeight = 0.0f;
        DashSpeed = 500.0f;

    }


//	public void OnFingerHeldDown(Lean.LeanFinger finger)
//	{
//		if (finger.IsOverGui == false && grounded && CanFly) 
//		{
//			Fly = true;
//		}
//
//	}

	public void OnFingerTap(Lean.LeanFinger finger)
	{
		if (finger.IsOverGui == false && grounded && CanJump) {
			Debug.Log ("Player Touch Jumped");
			Jump = true;

		}
	}

	public void OnFingerSwipe(Lean.LeanFinger finger)
	{
		Vector2 swipe = finger.SwipeDelta;

		if (swipe.x > Mathf.Abs(swipe.y) &&CanDash && !grounded)
		{
			Dash  = true;
		}

		if (swipe.y < -Mathf.Abs(swipe.x) && grounded)
		{
			SwitchDown = true;
			SwitchUp = false;
		}
		
		if (swipe.y > Mathf.Abs(swipe.x) && grounded)
		{
			SwitchUp = true;
			SwitchDown = false;
		}
	}

    // Update is called once per frame
    void Update()
    {

        


        if (Input.GetKeyDown(KeyCode.RightArrow) && CanDash && !grounded)
        {
            Dash = true;
        }

        if (Input.GetKey(KeyCode.UpArrow) && grounded && CanJump)
        {
            Debug.Log("Player Jumped");
            Jump = true;
        }
        
    }



    void FixedUpdate()
    {

		//Autoscroller so the player has to move on a vector2 in the x axis
//		Vector2 newVelocity = rb2d.velocity;
//		newVelocity.x = PlayerSpeed;
//		rb2d.velocity = newVelocity;

		transform.Translate(8.0f * Time.deltaTime, 0f, 0f);

        if (Dash)
        {
            rb2d.mass = 0.1f;
            rb2d.AddForce(Vector2.right * DashSpeed);
			DashAmount = DashAmount+1.0f;
            Dash = false;
			CanDash = false;
        }

        if (!Dash)
        {
            rb2d.mass = 1.0f;
        }

		if (Fly)
		{
			//add upwards force but on a relative time maybe delta? if the player collides with the other platform then switch the gravity to that side
			Debug.Log("Player Flying");
		}

        if (Jump && CanJump) {
			JumpHeight = 500.0f;
			rb2d.AddForce (Vector2.up * JumpHeight);
			Jump = false;

		} 

        if (grounded)
        {
			DashAmount = 0.0f;
            CanJump = true;
			CanFly = true;
            Debug.Log("Player Grounded");
        }

        if (!grounded)
        {
            JumpHeight = 0.0f;
            if(DashAmount < 1.0f)
			{
				CanDash = true;

			}
            CanJump = false;

        }

//		if (SwitchUp)
//		{
//			rb2d.AddForce(Vector2.up* 600.0f);
//			rb2d.gravityScale *= -1;
//			
//		}
//		else if (SwitchDown)
//		{
//			//make gravity normal so that the player is on the bottom half of the screen
//			rb2d.AddForce(Vector2.down* 600.0f);
//			rb2d.gravityScale *= 1;
//
//		}



    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            grounded = true;


    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            grounded = false;
        Debug.Log("Player in air");
    }












}


