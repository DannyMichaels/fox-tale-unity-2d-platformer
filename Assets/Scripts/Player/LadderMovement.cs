using UnityEngine;

public class LadderMovement : MonoBehaviour
{
  private float vertical;
  public float climbSpeed = 8f;
  public bool isOnLadder;
  public bool isClimbing;
  private float initialGravityScale;
  private Animator animator;
  private float initialAnimSpeed;

  [SerializeField] private Rigidbody2D theRB;

  void Start()
  {
    animator = GetComponent<Animator>();
    initialAnimSpeed = animator.speed;
  }

  void Awake()
  {
    initialGravityScale = theRB.gravityScale;
  }

  void Update()
  {
    vertical = Input.GetAxisRaw("Vertical");

    if (isOnLadder && Mathf.Abs(vertical) > 0f)
    {
      isClimbing = true; // set isClimbing to true when pressing up when ladder is infront of player.
    }


    // if (vertical < 0f)
    // {
    //   animator.SetBool("isClimbing", false);
    //   animator.SetBool("isClimbingDown", true);
    // }

    if (Mathf.Abs(vertical) == 0 && isClimbing)
    {
      animator.speed = 0f; // freeze animator because player is on ladder but not moving
    }
    else
    {
      animator.speed = initialAnimSpeed; // reset animation because is climbing
    }


    animator.SetBool("isClimbing", isClimbing);
  }

  private void FixedUpdate()
  {
    if (isClimbing)
    {
      theRB.gravityScale = 0f; // stop gravity scale so when player doesn't hold movement keys the player doesn't fall down
      animator.speed = initialAnimSpeed; // continue the animation
      theRB.velocity = new Vector2(theRB.velocity.x, vertical * climbSpeed); // move on Y axis
    }
    else
    {
      theRB.gravityScale = initialGravityScale; // reset gravity scale if not climbing.
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Ladder"))
    {
      isOnLadder = true;
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.CompareTag("Ladder"))
    {
      isOnLadder = false;
      isClimbing = false;
    }
  }
}
