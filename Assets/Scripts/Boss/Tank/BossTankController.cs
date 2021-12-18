using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
BOSS FLOW:

1. is shooting. 
2. is hurt (goes on ground, waits a bit) THEN STARTS MOVING. 
3. back to shooting

*/

public class BossTankController : MonoBehaviour
{
  public enum bossStates { shooting, hurt, moving };
  public bossStates currentState;

  public Transform theBoss;
  public Animator animator;

  // [Header("string")] @desc: Categorize for Unity UI.
  [Header("Movement")]
  public float moveSpeed;
  public Transform leftPoint, rightPoint;
  private bool shouldMoveRight;


  [Header("Shooting")]
  public GameObject bullet;
  public Transform firePoint; // where the bullets are going to be fired from
  public float timeBetweenShots; // time to wait between each shot
  private float shotCounter;

  [Header("Hurt")]
  public float hurtTime; // time to wait after gets hurt before continuing
  private float hurtCounter; // counter to start after gets hurt

  public GameObject theHitBox;

  // Start is called before the first frame update
  void Start()
  {
    currentState = bossStates.shooting; // can also use the index number of the enum (currentState = 0)
  }

  // Update is called once per frame
  void Update()
  {
    UseBossStates();
    useDebug();
  }

  private void UseBossStates()
  {
    switch (currentState)
    {
      case bossStates.shooting:
        HandleBossShooting();
        break;

      case bossStates.hurt:
        HandleBossHurt();
        break;

      case bossStates.moving:
        HandleBossMove();
        break;
    }
  }

  private void HandleBossShooting()
  {
    shotCounter -= Time.deltaTime;

    if (shotCounter <= 0)
    {
      shotCounter = timeBetweenShots; // reset the counter 
      var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation); // create the bullet at the correct position and refer to it as newBullet.

      newBullet.transform.localScale = theBoss.localScale;  // make sure that the bullet has the same localScale as the boss
    }
  }

  private void HandleBossHurt()
  {
    if (hurtCounter > 0)
    {
      hurtCounter -= Time.deltaTime;

      if (hurtCounter <= 0)
      {
        // start moving if hurtCounter is less than or equal to 0.
        currentState = bossStates.moving;
      }
    }
  }

  private void HandleBossMove()
  {
    if (shouldMoveRight)
    {
      // move torwards right point
      theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

      bool reachedTheRightPoint = theBoss.position.x > rightPoint.position.x;

      if (reachedTheRightPoint)
      {
        ChangeFacingDirection("left");

        // stop moving to the right (will move to left on next invocation of this function).
        shouldMoveRight = false;

        EndBossMovement();
      }
    }
    else
    {
      // else: moving left

      // move torwards left point
      theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

      bool reachedTheLeftPoint = theBoss.position.x < leftPoint.position.x;

      if (reachedTheLeftPoint)
      {
        ChangeFacingDirection("right");

        // stop moving to the left (will move to right on next invocation of this function).
        shouldMoveRight = true;

        EndBossMovement();
      }
    }
  }

  private void ChangeFacingDirection(string directionToFace)
  {
    // change scale
    // using localScale so everything maintains it's position but flipped (including the firepoint, flipX won't flip firePoint)  
    if (directionToFace == "left")
    {
      theBoss.localScale = Vector3.one; // Vector3.one is equivalent to: new Vector3(1f, 1f, 1f)
    }
    else if (directionToFace == "right")
    {
      theBoss.localScale = new Vector3(-1f, 1f, 1f); // x, y, z
    }
  }

  private void EndBossMovement()
  {
    currentState = bossStates.shooting; // start shooting
    // shotCounter = timeBetweenShots; // start shotCounter
    shotCounter = 0f;

    animator.SetTrigger("StopMoving"); // stop moving on animator which will cause the Boss_Tank_Open animation into idle to run

    theHitBox.SetActive(true); // reactivate hitbox when reaching the other side (because it was deactivated in BossTankHitBox script after getting hit.)
  }

  public void TakeHit()
  {
    currentState = bossStates.hurt;
    hurtCounter = hurtTime;

    animator.SetTrigger("Hit"); // play hit animation which then goes to close animation
  }

  private void useDebug()
  {
    // #if/#endif only run this if in development mode
#if UNITY_EDITOR
    if (Input.GetKeyDown(KeyCode.H))
    {
      TakeHit();
    }
#endif
  }
}
