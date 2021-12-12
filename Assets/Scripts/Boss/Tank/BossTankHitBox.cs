using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitBox : MonoBehaviour
{
  public BossTankController bossController;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    // playerIsAboveHitBox: I want the player to be above the boss for the hit to be successful, because he can jump when being under the boss and that is bad if it would still hurt the boss.
    bool playerIsAboveHitBox = PlayerController.instance.transform.position.y > transform.position.y;

    if (other.CompareTag("Player") && playerIsAboveHitBox)
    {
      bossController.TakeHit();

      PlayerController.instance.Bounce();

      gameObject.SetActive(false); // turn off the hitbox, then boss moves to other side of screen, and reactive the hitbox
    }
  }
}
