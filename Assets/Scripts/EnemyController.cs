using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public float moveSpeed;

  public Transform leftPoint, rightPoint;

  private bool isMovingRight;

  private Rigidbody2D theRB;
  public SpriteRenderer theSR;

  public float moveTime, waitTime;
  private float moveCount, waitCount;

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
    HandleEnemyMovementHorizontal();
  }

  private void HandleEnemyMovementHorizontal()
  {
    bool shouldMove = moveCount > 0;

    if (shouldMove)
    {
      moveCount -= Time.deltaTime;

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

      if (moveCount <= 0)
      {
        // Debug.Log("waitCount is <= 0 waitCount is being set to wait time : " + waitCount + " " + waitTime);
        // waitCount = waitTime;
        waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f); // randomise that
      }
    }
    else if (!shouldMove)
    {
      // stop moving
      waitCount -= Time.deltaTime;
      theRB.velocity = new Vector2(0f, theRB.velocity.y);
      // Debug.Log("Enemy waiting to move (stopped moving)");

      if (waitCount <= 0)
      {
        //  start moving again.
        // Debug.Log("waitCount is <= 0, Enemy will start to move again");
        // moveCount = moveTime;
        moveCount = Random.Range(moveTime * .75f, waitTime * 1.25f); // randomise that
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
    moveCount = moveTime;
  }

}
