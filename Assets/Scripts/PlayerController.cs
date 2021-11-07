using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  // the value for these vars can be changed in the inspector tab
  public float moveSpeed;
  public Rigidbody2D theRB; // the rigid body
  public float jumpForce;
  private bool isOnGround; // keep track of wether player is on ground or not.
  public Transform groundCheckPoint;
  public LayerMask whatIsGround;
  private bool canDoubleJump;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    handleMoveHorizontal();
    handleJump();
  }

  private void handleMoveHorizontal()
  {
    // input.GetAxis will return a negative or positive integer. The value will be in the range -1...1 for keyboard and joystick input devices.
    // https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
    // https://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html
    theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y); // for horizontal movement
  }

  private void handleJump()
  {
    checkIsOnGround();
    checkCanDoubleJump();

    // edit -> project settings -> input manager, name of input, NOT set key of input
    if (Input.GetButtonDown("Jump")) // GetButtonDown: the very moment a button is pressed (not held down, for held down use GetButton) 
    {
      if (isOnGround)
      {
        makePlayerJump();
      }
      // if not on ground and canDoubleJump (gets set with checkCanDoubleJump), then we can jump once more if Input.getButton is == 'Jump'
      else if (canDoubleJump)
      {
        makePlayerJump();
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
  private void checkIsOnGround()
  {
    // one way to debug this is click on player, then on the inspect tab click the 3 dots at the right, and click Debug.
    isOnGround = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
  }


  private void checkCanDoubleJump()
  {
    if (isOnGround)
    {
      canDoubleJump = true;
    }
  }

  private void makePlayerJump()
  {
    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
  }
}
