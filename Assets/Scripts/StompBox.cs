using UnityEngine;

public class StompBox : MonoBehaviour
{
  public GameObject deathEffect;

  public GameObject collectible; // drop collectible when enemy destroyed.
  public float chanceToDrop; // chance to spawn random collectible


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
    BouncePlayer();
    HandleDropCollectible(enemy);
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

  private void BouncePlayer()
  {
    PlayerController.instance.Bounce();
  }

  private void HandleDropCollectible(Collider2D enemy)
  {
    float randomFloat = Random.Range(0, 100f); // random num from 0f to 100f (min and max inclusive)
    bool shouldDrop = randomFloat <= chanceToDrop;

    if (shouldDrop)
    {
      CreateCollectible(enemy);
    }
  }

  private void CreateCollectible(Collider2D enemy)
  {
    Instantiate(collectible, enemy.transform.position, enemy.transform.rotation);
  }
}
