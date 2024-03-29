using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public static PlayerController instance;
  // the value for these vars can be changed in the inspector tab
  public float moveSpeed;
  public Rigidbody2D theRB; // the rigid body
  public float jumpForce;
  public bool isOnGround; // keep track of wether player is on ground or not (only public so I can use it in StompBox.cs).
  public Transform groundCheckPoint;
  public LayerMask whatIsGround;

  private bool canDoubleJump;

  public bool isBouncedByPad; // check if player is bounced by pad to block jumping

  private Animator animator;
  private SpriteRenderer theSR; // the sprite renderer

  public float knockBackLength, knockBackForce;
  private float knockBackCounter;

  // bounceForce: bounce force after stomping enemy
  public float bounceForce;

  public bool stopInput;

  public bool isCrouching;


  private void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    animator = GetComponent<Animator>();
    theSR = GetComponent<SpriteRenderer>();

    HandleErrors();
  }

  // Update is called once per frame
  void Update()
  {
    bool isKnockedBack = knockBackCounter > 0;

    if (!PauseMenu.instance.isPaused && !stopInput) // only allow these if isn't paused and input isn't stopped
    {
      if (isKnockedBack)
      {
        UseKnockBackEffect();
      }
      else
      {
        HandleMoveHorizontal();
        HandleJump();
        HandleSpriteFacingDirection();
        HandleCrouch();
      }
    }

    UseAnimationEffect();
  }

  private void HandleMoveHorizontal()
  {

    // input.GetAxis will return a negative or positive integer. The value will be in the range -1...1 for keyboard and joystick input devices.
    // https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
    // https://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html
    theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y); // for horizontal movement
  }

  private void HandleJump()
  {
    CheckIsOnGround();
    CheckCanDoubleJump();

    if (isBouncedByPad) return;

    // edit -> project settings -> input manager, name of input, NOT set key of input
    if (Input.GetButtonDown("Jump")) // GetButtonDown: the very moment a button is pressed (not held down, for held down use GetButton) 
    {
      if (isOnGround)
      {
        MakePlayerJump();
      }
      // if not on ground and canDoubleJump (gets set with checkCanDoubleJump), then we can jump once more if Input.getButton is == 'Jump'
      else if (canDoubleJump)
      {
        MakePlayerJump();
        canDoubleJump = false; // after done one more jump it's false. 
      }
    }
  }


  /*
    @method checkIsOnGround
    @desc 
       create a circle at this position, make a radius of .2, and then check and see what is on the ground layer
       and if there are any objects on the ground layer it will set it to true, else false.
      This is to avoid unlimited spam jumping, you cannot jump unless you're on the ground.
  */
  private void CheckIsOnGround()
  {
    // one way to debug this is click on player, then on the inspect tab click the 3 dots at the right, and click Debug.
    isOnGround = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
  }

  private void CheckCanDoubleJump()
  {
    if (isOnGround)
    {
      canDoubleJump = true;
    }
  }

  private void MakePlayerJump()
  {
    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);

    PlayJumpSFX();
  }

  private void PlayJumpSFX()
  {
    AudioManager.instance.PlaySFX("PLAYER_JUMP"); // index 10 of soundEffects array.
  }

  /* 
    @method useAnimationEffect
    @desc
      Listen to changes of player properties to render certain animations. 
      if isOnGround changes, change it in the animator, same thing for other properties
  */
  private void UseAnimationEffect()
  {
    animator.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x)); // always get positive value (for when moving left will return negative value)
    animator.SetBool("isOnGround", isOnGround);
    animator.SetBool("isCrouching", isCrouching);
  }

  private void HandleSpriteFacingDirection()
  {
    bool isMovingLeft = theRB.velocity.x < 0;
    bool isMovingRight = theRB.velocity.x > 0;

    if (isMovingLeft)
    {
      theSR.flipX = true;
    }
    else if (isMovingRight)
    {
      theSR.flipX = false;
    }
  }

  public void KnockBack()
  {
    knockBackCounter = knockBackLength;
    theRB.velocity = new Vector2(0f, knockBackForce); // move player in Y axis with knockback force (a little knockback up)

    animator.SetTrigger("hurt");
  }



  // this runs when the player is being knocked back (isKnockedBack is true)
  private void UseKnockBackEffect()
  {
    knockBackCounter -= Time.deltaTime; // set counter when

    bool isFacingRight = !theSR.flipX;


    // push him in the X axis
    if (isFacingRight)
    {
      theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y); // a little x axis knockback (push player to left)
    }
    else
    {
      theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y); // a little x axis knockback (push player to right)
    }
  }

  public void Bounce()
  {
    theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);

    // PlayJumpSFX();
  }


  // when 2 colldiers touch against each other
  private void OnCollisionEnter2D(Collision2D other)
  {

    // for moving platforms when player is ontop.
    if (other.gameObject.CompareTag("Platform"))
    {
      // player parent will be the platform so he will follow the moving platform
      transform.parent = other.transform;
    }
  }


  // when two colliders stop touching each other
  private void OnCollisionExit2D(Collision2D other)
  {
    if (other.gameObject.CompareTag("Platform"))
    {
      transform.parent = null; // unparent the platform from the player
    }
  }

  private void HandleErrors()
  {
    if (!PauseMenu.instance)
    {
      Debug.LogError("No PauseMenu provided!");
    }
  }


  private void HandleCrouch()
  {
    if (Input.GetButtonDown("Horizontal"))
    {
      isCrouching = false;
    }

    else if (Input.GetButtonDown("Crouch"))
    {
      isCrouching = true;
    }
    else if (Input.GetButtonUp("Crouch"))
    {
      isCrouching = false;
    }
  }
}
