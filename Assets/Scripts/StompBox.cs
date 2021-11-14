using UnityEngine;

public class StompBox : MonoBehaviour
{
  public GameObject deathEffect;

  public GameObject collectible; // drop collectible when enemy destroyed.
  [Range(0, 100)] public float chanceToDrop; // chance to spawn random collectible (has to be in range from 0 to 100)


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
      if (PlayerController.instance.isOnGround) return; // this shouldn't continue if player is on ground.

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
    PlayEnemyExplodeSFX();
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


  private void PlayEnemyExplodeSFX()
  {
    // enemy explode is element 3 in the sound effects array for Audio Manager
    AudioManager.instance.PlaySFX(3);
  }
}
