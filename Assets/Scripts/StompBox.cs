using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
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
      DestroyEnemy(other);
    }
  }

  private void DestroyEnemy(Collider2D enemy)
  {
    // enemy.gameObject.SetActive(false);
    enemy.transform.parent.gameObject.SetActive(false);
  }
}
