using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public float moveSpeed;

  public Transform leftPoint, rightPoint;

  private bool isMovingRight;

  private Rigidbody2D theRB;
  public SpriteRenderer theSR;

  // Start is called before the first frame update
  void Start()
  {
    InitializeRB();
    InitializeRightAndLeftPoints();
    InitializeEnemyMovement();
  }

  // Update is called once per frame
  void Update()
  {
    HandleEnemyMovement();
  }

  private void HandleEnemyMovement()
  {
    if (isMovingRight)
    {
      theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y); // move to right

      theSR.flipX = true; // flip sprite to face right direction (by default the sprite is facing left so true will set it to right)

      if (transform.position.x > rightPoint.position.x)
      {
        isMovingRight = false;
      }
    }
    else
    {
      // moving to the left
      theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y); // move to left

      theSR.flipX = false; // flip sprite to face left direction (by default the sprite is facing left so false will set it to face left)

      if (transform.position.x < leftPoint.position.x)
      {
        isMovingRight = true;
      }

    }
  }

  private void InitializeRB()
  {
    theRB = GetComponent<Rigidbody2D>();
  }

  private void InitializeRightAndLeftPoints()
  {
    // remove the parent so when the frog moves they stay in the same position.
    leftPoint.parent = null;
    rightPoint.parent = null;
  }

  private void InitializeEnemyMovement()
  {
    isMovingRight = true;
  }
}
