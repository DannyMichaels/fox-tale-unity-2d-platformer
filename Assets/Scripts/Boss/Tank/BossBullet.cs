using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
  public float speed; // how fast it goes

  // Start is called before the first frame update
  void Start()
  {
    AudioManager.instance.PlaySFX("BOSS_SHOT");
  }

  // Update is called once per frame
  void Update()
  {
    MoveBullet();
  }

  void MoveBullet()
  {

    // -speed because it should move to the left on x, multiply with the scale to get a negative or a positive number as to where the bullet should go (left or right)
    // if localscale is a positive number then x axis would be negative (-speed * posNum) so the bullet would go left, but if localScale is negative then - minus - is a positive number so the bullet would go right.
    transform.position += new Vector3(-speed * transform.localScale.x * Time.deltaTime, 0f, 0f);
  }

  void OnTriggerEnter2D(Collider2D other)
  {

    if (other.CompareTag("Player"))
    {
      PlayerHealthController.instance.DealDamage();
    }

    AudioManager.instance.PlaySFX("BOSS_IMPACT");

    Destroy(gameObject); // destroy the bullet after hitting
  }
}
