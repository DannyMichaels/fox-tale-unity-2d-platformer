using UnityEngine;

// static vars do not show up in the inspector 

public class PlayerHealthController : MonoBehaviour
{
  public static PlayerHealthController instance; // instance = Singleton: create a version of this script that only one version of it can exist

  public int currentHealth, maxHealth;

  public float invincibleLength; // how long do we want our player to be invincible after he got hit.
  public float invincibleCounter;

  private SpriteRenderer theSR;

  // Awake is called just right before the Start function gets called (as soon as the game starts running)
  private void Awake()
  {
    instance = this; // this would be the PlayerHealthController
  }



  // Start is called before the first frame update
  void Start()
  {
    currentHealth = maxHealth;

    theSR = GetComponent<SpriteRenderer>();
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

  // HealPlayer: used when cherry is being pickedup
  public void HealPlayer()
  {
    currentHealth += 1;

    // handle weird edge-case if for some reason that happens
    if (currentHealth > maxHealth)
    {
      currentHealth = maxHealth;
    }

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
    // the logic
    invincibleCounter = invincibleLength;

    setPlayerOpacity(0.5f);
    PlayerController.instance.KnockBack();
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

      bool isNotInvincible = invincibleCounter <= 0; // it gets set in the line above so we can still check here

      if (isNotInvincible)
      {
        setPlayerOpacity(1f);
      }
    }
  }

  private void setPlayerOpacity(float opacity)
  {
    // unity interperet RGBA values from 0 to 1 instead of 0 to 255
    // change player opacity to make him appear that way (the a in rgba)
    theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, opacity); // keep rgb as is, change a (transparency)
  }

  public void updateUIHeartsDisplay()
  {
    UIController.instance.UpdateHealthDisplay();
  }

  public void DestroyPlayer()
  {
    // gameObject.SetActive(false); // make player dissapear

    LevelManager.instance.OnPlayerDeath(); // will respawn player
  }
}
