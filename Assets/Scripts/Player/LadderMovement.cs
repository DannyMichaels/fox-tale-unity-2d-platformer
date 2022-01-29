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
      isClimbing = true;
    }


    // if (vertical < 0f)
    // {
    //   animator.SetBool("isClimbing", false);
    //   animator.SetBool("isClimbingDown", true);
    // }

    if (Mathf.Abs(vertical) == 0 && isClimbing)
    {
      animator.speed = 0f;
    }
    else
    {
      animator.speed = initialAnimSpeed;
    }


    animator.SetBool("isClimbing", isClimbing);
  }

  private void FixedUpdate()
  {
    if (isClimbing)
    {
      theRB.gravityScale = 0f;
      animator.speed = initialAnimSpeed;
      theRB.velocity = new Vector2(theRB.velocity.x, vertical * climbSpeed);
    }
    else
    {
      theRB.gravityScale = initialGravityScale;
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