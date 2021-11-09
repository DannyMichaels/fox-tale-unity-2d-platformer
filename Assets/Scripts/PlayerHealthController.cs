using UnityEngine;

// static vars do not show up in the inspector 

public class PlayerHealthController : MonoBehaviour
{
  public static PlayerHealthController instance; // instance = Singleton: create a version of this script that only one version of it can exist

  public int currentHealth, maxHealth;


  // Awake is called just right before the Start function gets called (as soon as the game starts running)
  private void Awake()
  {
    instance = this; // this would be the PlayerHealthController
  }



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
    updateUIHeartsDisplay();
  }

  public void CheckPlayerDead()
  {
    if (currentHealth <= 0)
    {
      DestroyPlayer();
    }
  }


  public void updateUIHeartsDisplay()
  {

    UIController.instance.UpdateHealthDisplay();
  }

  public void DestroyPlayer()
  {
    gameObject.SetActive(false); // make player dissapear
  }
}
