using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
  public int currentHealth, maxHealth;

  // Start is called before the first frame update
  void Start()
  {
    currentHealth = maxHealth;
  }

  // Update is called once per frame
  void Update()
  {

  }


  public void DealDamage()
  {
    currentHealth -= 1;
    CheckPlayerDead();
  }

  public void CheckPlayerDead()
  {
    if (currentHealth <= 0)
    {
      DestroyPlayer();
    }
  }

  public void DestroyPlayer()
  {
    gameObject.SetActive(false); // make player dissapear
  }
}
