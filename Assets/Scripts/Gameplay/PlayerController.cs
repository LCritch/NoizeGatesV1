using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb2d;
	public GameObject PlayerCube;
    private Lean.LeanFinger Finger;

    //http://stackoverflow.com/questions/27712233/swipe-gestures-on-android-in-unity
    //http://forum.unity3d.com/threads/jumping-script-in-a-platformer.75398/



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
    private bool CanDash;
	public bool isOnBottom;
	public bool isOnTop;
    public bool isDashing;


    //Bools for holding info on what each playerstate does
    private bool Jump;
    public bool Dash;
    private bool SwitchUp;
    private bool SwitchDown;


	//init for Lean touch input

	protected virtual void OnEnable()
	{
		// Hook into the OnFingerTap event
        //Lean.LeanTouch.OnFingerTap += OnFingerTap;

		// Hook into the OnSwipe event
		Lean.LeanTouch.OnFingerSwipe += OnFingerSwipe;

	}
	
	protected virtual void OnDisable()
	{
		// Unhook into the OnFingerTap event
        //Lean.LeanTouch.OnFingerTap -= OnFingerTap;

		// Hook into the OnSwipe event
		Lean.LeanTouch.OnFingerSwipe -= OnFingerSwipe;

	}

    // Use this for initialization
    void Start()
    {
        Physics2D.gravity = new Vector2(0, -9.81f);
        DashSpeed = 3.0f;
		isOnBottom = true;

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            grounded = true;
        Debug.Log("Player Grounded");

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            grounded = false;
        Debug.Log("Player in air");
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if(touch.phase == TouchPhase.Ended && touch.tapCount == 1)
            {
                if (grounded && CanJump)
                {
                    Jump = true;
                }
            }
        }


            if (grounded)
            {
                DashAmount = 0.0f;
                isDashing = false;
                CanJump = true;
            }

        if (!grounded)
        {
            JumpHeight = 0.0f;
            if (DashAmount < 1.0f)
            {
                CanDash = true;

            }
            CanJump = false;

        }

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


		transform.Translate(Vector3.right * 8.0f * Time.fixedDeltaTime);

        if (Dash)
        {
            rb2d.mass = 1.0f;
            rb2d.AddRelativeForce(new Vector2 (DashSpeed,0),ForceMode2D.Impulse);
			DashAmount = DashAmount+1.0f;
            isDashing = true;
            Dash = false;
            CanDash = false;
        }

        if (!Dash)
        {
            rb2d.mass = 1.35f;
        }

        if (Jump)
        {
            JumpHeight = 500.0f;
            if (isOnTop)
            {
                rb2d.AddForce(Vector2.down * JumpHeight);
            }
            else if (isOnBottom)
            {
                rb2d.AddForce(Vector2.up * JumpHeight);
            }
            Jump = false;
            CanJump = false;

        }


		if (SwitchUp)
		{
            //rb2d.gravityScale = -1;
            Physics2D.gravity = new Vector2(0, 9.81f);

            if (SwitchUp && grounded)
			{
				isOnTop = true;
				isOnBottom = false;
			}
			
		}
		else if (SwitchDown)
		{
			//make gravity normal so that the player is on the bottom half of the screen
            //rb2d.gravityScale = 1;
            Physics2D.gravity = new Vector2(0, -9.81f);
            if (SwitchDown && grounded)
			{
				isOnTop = false;
				isOnBottom = true;
			}
          
		}


    }

    //public void OnFingerTap(Lean.LeanFinger Finger)
    //{
    //    if (Finger.IsOverGui == false)
    //    {
    //        if (grounded && CanJump)
    //        {
    //            Debug.Log("Player Touch Jumped");
    //            Jump = true;
    //        }
    //    }
    //}

    public void OnFingerSwipe(Lean.LeanFinger Finger)
    {
        Vector2 swipe = Finger.SwipeDelta;

        if (swipe.x > Mathf.Abs(swipe.y) && CanDash && !grounded)
        {
            Dash = true;
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













}


