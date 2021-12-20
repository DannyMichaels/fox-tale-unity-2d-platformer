using UnityEngine;

public class BossTankMine : MonoBehaviour
{
  public GameObject explosion;

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
    if (other.CompareTag("Player"))
    {
      Destroy(gameObject); // destroy the mine;

      Instantiate(explosion, transform.position, transform.rotation); // instantiate an explosion

      PlayerHealthController.instance.DealDamage(); // damage the player

      AudioManager.instance.PlaySFX("ENEMY_EXPLODE"); // play explosion SFX
    }
  }

  public void Explode()
  {
    Destroy(gameObject);

    AudioManager.instance.PlaySFX("ENEMY_EXPLODE"); // play explosion SFX

    Instantiate(explosion, transform.position, transform.rotation); // instantiate an explosion
  }
}
