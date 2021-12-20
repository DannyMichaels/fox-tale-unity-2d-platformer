using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
  public GameObject theBossBattle;


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
      // activate the boss battle
      theBossBattle.SetActive(true);

      gameObject.SetActive(false); // deactivate boss activator (don't need it anymore)

      AudioManager.instance.PlayBossMusic();
    }
  }
}
