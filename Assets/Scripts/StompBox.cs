using UnityEngine;

public class StompBox : MonoBehaviour
{
  public GameObject deathEffect;


  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Enemy")
    {
      // Debug.Log("Hit enemy");
      OnEnemyStomped(other);
    }
  }

  // when enemy gets stomped on Mario style
  private void OnEnemyStomped(Collider2D enemy)
  {
    DestroyEnemy(enemy);
    CreateDeathEffectAnimation(enemy);
  }


  private void DestroyEnemy(Collider2D enemy)
  {
    // enemy.gameObject.SetActive(false);
    enemy.transform.parent.gameObject.SetActive(false);
  }

  private void CreateDeathEffectAnimation(Collider2D enemy)
  {
    Instantiate(deathEffect, enemy.transform.position, enemy.transform.rotation);
  }
}
