using System.Collections;
using System.Collections.Generic;
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
    }
  }
}
