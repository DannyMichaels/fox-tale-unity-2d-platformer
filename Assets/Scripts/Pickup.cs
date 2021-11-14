using UnityEngine;

// for picking items up
public class Pickup : MonoBehaviour
{
  public bool isGem, isHealingCollectible;

  private bool isCollected; // avoid duplicate spam collection of same item (if player has multiple colliders)

  public GameObject pickupEffect;

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
    // equivalent of other.tag === "Player"
    if (other.CompareTag("Player") && !isCollected)
    {
      if (isGem)
      {
        onGemCollected();
      }

      if (isHealingCollectible)
      {
        onHealthCollected();
      }
    }
  }


  private void onGemCollected()
  {
    LevelManager.instance.gemsCollected += 1; // increment gems collected

    isCollected = true;
    Destroy(gameObject); // destroy the collected item

    CreatePickupEffectAnimation();

    AudioManager.instance.PlaySFX(6); // play the gem collected SFX

    UIController.instance.UpdateGemCount(); // update the text of gems count in the ui
  }


  private void onHealthCollected()
  {
    if (PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)
    {
      PlayerHealthController.instance.HealPlayer();

      isCollected = true;

      Destroy(gameObject);


      CreatePickupEffectAnimation();

      AudioManager.instance.PlaySFX(7); // play the health collected SFX
    }
  }


  // CreatePickupEffectAnimation: this creates a Pickup Effect object
  private void CreatePickupEffectAnimation()
  {
    Instantiate(pickupEffect, transform.position, transform.rotation); //obj, pos, rot
  }
}
