using UnityEngine;

// static vars do not show up in the inspector 

public class PlayerHealthController : MonoBehaviour
{
  public static PlayerHealthController instance; // instance = Singleton: create a version of this script that only one version of it can exist

  public int currentHealth, maxHealth;

  public float invincibleLength; // how long do we want our player to be invincible after he got hit.
  public float invincibleCounter;

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
    HandleInvincibility();
  }


  public void DealDamage()
  {
    bool isInvincible = invincibleCounter > 0;

    if (isInvincible) return; // don't damage player if he is invincible

    currentHealth -= 1;
    CheckPlayerDead();
    updateUIHeartsDisplay();
  }

  public void CheckPlayerDead()
  {


    if (currentHealth <= 0)
    {
      currentHealth = 0;
      DestroyPlayer();
    }
    else
    {
      makePlayerInvicible(); // avoid player getting hit multiple times in a period of time (classic platformer thing)
    }

  }

  public void makePlayerInvicible()
  {
    invincibleCounter = invincibleLength;
  }

  public void HandleInvincibility()
  {
    bool isInvincible = invincibleCounter > 0;
    // if the player is invincible, make sure that a second after he is not invincible
    if (isInvincible)
    {
      /* Time.deltaTime is the time it takes to get from one update from to the next.
       so a 60 fps game Time.deltaTime would be 1/60th of a second, if it was 30fps it would be 1/30  */
      invincibleCounter -= Time.deltaTime;
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
