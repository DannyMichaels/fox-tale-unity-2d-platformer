using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
  public Transform[] points;
  public float moveSpeed;
  private int currentPoint;

  public SpriteRenderer theSR;

  // distanceToAttackPlayer: the range for enemy to attack player, chaseSpeed: speed to be chasing player
  public float distanceToAttackPlayer, chaseSpeed;

  // Start is called before the first frame update
  void Start()
  {
    RemovePointsParent();
  }

  // Update is called once per frame
  void Update()
  {
    /*
      if the distance between the current position and the player controller position is greater than the distance to attack the player 
      it means the player is NOT within range.
     */
    bool playerIsNotInRange = Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttackPlayer;

    if (playerIsNotInRange)
    {
      MoveNormally();
    }
    else
    {
      // player is in range, chase him.
      ChasePlayer();
    }
  }

  private void MoveNormally()
  {
    MoveTorwardsCurrentPoint();
    HandleChangePoint();
    HandleSpriteFacingDirection();
  }

  // @method MoveTorwardsCurrentPoint
  // @desc enemy constantly will be moving torwards current point.
  private void MoveTorwardsCurrentPoint()
  {
    transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime); // MoveTorwards(current, target, maxDistanceDelta)
  }

  // @method HandleChangePoint
  // @desc increase the currentPoint if the distance between current position and the enemy. if it's max point go back to 0.
  private void HandleChangePoint()
  {

    // if it's close to current point (just before it hits)
    if (Vector3.Distance(transform.position, points[currentPoint].position) < .05f)
    {
      currentPoint++;

      if (currentPoint >= points.Length)
      {
        currentPoint = 0;
      }
    }
  }


  /* @method RemovePointsParent
     @desc loop through all points and make sure they have no transform parent
     the enemy would be chasing after point 1 but because it's a child of the element: 
     we need to make srue that these points are no longer children because otherwise the enemy won't move properly. 
  */
  private void RemovePointsParent()
  {
    // loop through all points and make sure they have no transform parent
    foreach (Transform point in points)
    {
      point.parent = null;
    }
  }


  // @method HandleSpriteFacingDirection
  // @desc make enemy face left or right
  private void HandleSpriteFacingDirection()
  {
    bool shouldFaceRight = transform.position.x < points[currentPoint].position.x;
    bool shouldFaceLeft = transform.position.x > points[currentPoint].position.x;

    if (shouldFaceRight)
    {
      theSR.flipX = true;
    }
    else if (shouldFaceLeft)
    {
      theSR.flipX = false;
    }
  }

  private void ChasePlayer()
  {
    transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, chaseSpeed * Time.deltaTime);
  }
}