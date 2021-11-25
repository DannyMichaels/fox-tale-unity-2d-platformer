using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
  private Animator animator;

  public float bounceForce = 20f;

  // Start is called before the first frame update
  void Start()
  {
    animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    // https://www.reddit.com/r/Unity2D/comments/b10gdo/comparetag_vs_othertag/
    // compareTag is faster and doesn't allocate memory
    // It also ensures that the string actually exists, and returns an error if the sting is null.

    if (other.CompareTag("Player"))
    {
      MakePlayerBounce();
      animator.SetTrigger("Bounce"); // activate bounce animation
    }
  }

  // @method MakePlayerBounce.
  // @desc make the player bounce like Sonic on a spring pad.
  private void MakePlayerBounce()
  {
    // PlayerController.instance.theRB.velocity = new Vector2(PlayerController.instance.theRB.velocity.x, bounceForce); // maintain X, new Y.
    StartCoroutine(PlayerBounceCo());
  }

  /* @method PlayerBounceCo
     @desc a courotine: Make the player bounce first.
     then set a bool for is BouncedByPad to block him from jumping again (Think how sonic bounce pad works)
     doing this so player can't jump while being bounced 
  */
  private IEnumerator PlayerBounceCo()
  {
    PlayerController.instance.theRB.velocity = new Vector2(PlayerController.instance.theRB.velocity.x, bounceForce); // maintain X, new Y. the actual code that makes the player bounce

    PlayerController.instance.isBouncedByPad = true; // setting a bool so Player is blocked from jumping again.

    // first initial wait
    yield return new WaitForSeconds(1f * (bounceForce / 100));

    // if the player isn't on the ground, wait a bit.
    while (!PlayerController.instance.isOnGround)
    {
      yield return new WaitForSeconds(1f * (bounceForce / 100));
    }

    if (PlayerController.instance.isOnGround)
    {
      PlayerController.instance.isBouncedByPad = false; // player is on ground, so he can jump again.
    }
  }
}
