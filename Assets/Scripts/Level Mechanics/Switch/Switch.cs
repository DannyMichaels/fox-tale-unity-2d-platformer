using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
  public GameObject objectToSwitch; // object to switch, example: door (so door can open when switched)

  private SpriteRenderer theSR;
  public Sprite downSprite;

  private bool hasSwitched;

  public bool deactivateOnSwitch; // make object dissapear when switched, else it will make object appear

  void Start()
  {
    InitializeSR();
  }

  void Update()
  {

  }

  private void InitializeSR()
  {
    theSR = GetComponent<SpriteRenderer>();
  }


  private void OnTriggerEnter2D(Collider2D other)
  {
    // if player is collided with switch
    if (other.CompareTag("Player") && !hasSwitched) // make sure it wasn't switched
    {
      if (deactivateOnSwitch)
      {
        // deactivate the object to switch (ex: door will be deactivated)
        objectToSwitch.SetActive(false);
      }
      else
      {
        // activate the object to switch (ex: platform will appear)
        objectToSwitch.SetActive(true);
      }

      theSR.sprite = downSprite; // set the sprite to the down sprite
      hasSwitched = true; // hasSwitched will now be true
    }
  }
}
