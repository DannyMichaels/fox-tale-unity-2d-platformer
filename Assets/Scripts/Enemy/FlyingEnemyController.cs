using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
  public Transform[] points;
  public float moveSpeed;
  private int currentPoint;

  public SpriteRenderer theSR;

  // distanceToAttackPlayer: the range for enemy to attack player, chaseSpeed: speed to be chasing player
  public float distanceToAttackPlayer, chaseSpeed;

  private Vector3 attackTarget;

  public float waitAfterAttack; // time to wait/pause after a chase/attack sequence has ended, if player is still close it will chase him again, else it will go back to moving normally
  private float attackCounter;

  // Start is called before the first frame update
  void Start()
  {
    RemovePointsParent();
  }

  // Update is called once per frame
  void Update()
  {
    bool isChasingPlayer = attackCounter > 0;

    if (isChasingPlayer)
    {
      attackCounter -= Time.deltaTime;
    }
    else
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
        // player is in range, chase & attack him.
        AttackPlayer();
      }
    }
  }

  private void MoveNormally()
  {
    attackTarget = Vector3.zero; // reset attackTarget to 0,0,0 (x,y,z) when player isn't in range.
    MoveTorwardsCurrentPoint();
    HandleChangePoint();
    HandleSpriteFacingDirection(points[currentPoint].position.x);
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
  private void HandleSpriteFacingDirection(float targetXPosition)
  {
    bool shouldFaceRight = transform.position.x < targetXPosition;
    bool shouldFaceLeft = transform.position.x > targetXPosition;

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
    // Vector3.zero = x0, y0, z0;

    if (attackTarget == Vector3.zero)
    {
      attackTarget = PlayerController.instance.transform.position; // set attackTarget to player pos
    }

    transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

    HandleSpriteFacingDirection(attackTarget.x); // make sure enemy is facing player while doing this
  }

  // note this method deals no damage to the player, it just makes sure the enemy chases him and then sets the counter accordingly
  private void AttackPlayer()
  {

    ChasePlayer();

    if (Vector3.Distance(transform.position, attackTarget) <= .1f)
    {
      // if the distance is super close, that means the player has been attacked
      attackCounter = waitAfterAttack; // wait that length of time and then allow to move torwards player
      attackTarget = Vector3.zero;
    }
  }
}