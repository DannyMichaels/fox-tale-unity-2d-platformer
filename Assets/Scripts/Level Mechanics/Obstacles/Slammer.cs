using UnityEngine;

// slammer mario style (the thing that crashes down on player and kills him)
public class Slammer : MonoBehaviour
{
  public Transform theSlammer;
  public Transform slammerTarget;

  public float slamSpeed, waitAfterSlam, recoverySpeed;

  private float waitCounter;

  private Vector3 initialPosition;

  private Vector3 attackTarget;

  private bool continueAttack = false; // continue slamming down torwards player

  public float attackRangeX = 1.5f; // x axis for slammer target to compare.

  void Start()
  {
    initialPosition = theSlammer.transform.position;
  }

  void Update()
  {
    float distance = Mathf.Abs(slammerTarget.transform.position.x - PlayerController.instance.transform.position.x);

    bool playerInRange = distance <= attackRangeX;

    bool isAtSpawnPos = theSlammer.transform.position == initialPosition; // is the slammer in spawn position?

    if (playerInRange && isAtSpawnPos || continueAttack)
    {
      AttackPlayer();
    }

    if (waitCounter > 0)
    {
      waitCounter -= Time.deltaTime;
    }

    // if it's not in initial position and continue attack is false, it should go back up
    if (!isAtSpawnPos && !continueAttack)
    {
      MoveBackToInitialPos();
    }
  }

  private void AttackPlayer()
  {
    if (waitCounter > 0) return;

    theSlammer.transform.position = Vector3.MoveTowards(theSlammer.transform.position, slammerTarget.transform.position, slamSpeed * Time.deltaTime);

    bool hitSlammerTarget = Vector3.Distance(theSlammer.transform.position, slammerTarget.transform.position) <= .1f; // if hit target (ex: slammer target is ground)

    if (hitSlammerTarget)
    {
      waitCounter = waitAfterSlam;
      continueAttack = false; // then it will go back up
    }
    else
    {
      continueAttack = true; // continue going down
    }
  }

  private void MoveBackToInitialPos()
  {
    if (waitCounter > 0) return;

    theSlammer.transform.position = Vector3.MoveTowards(theSlammer.transform.position, initialPosition, recoverySpeed * Time.deltaTime);
  }
}
