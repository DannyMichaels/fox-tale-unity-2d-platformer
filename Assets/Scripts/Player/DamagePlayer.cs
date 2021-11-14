using UnityEngine;


public class DamagePlayer : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  // OnTriggerEnter2D: this is a built in Unity function
  // it's looking for a RigidBody that is colliding against it, a ground is also a RigidBody
  // make sure the Player and the Spikes (or any object that wants to hurt the player and has this script) have the Player tag.
  void OnTriggerEnter2D(Collider2D other) // the other collided object that is colliding with this object
  {
    // check that a player rigid body is hiting it and not any other rigid body.
    if (other.tag == "Player")
    {
      // Debug.Log("Hit: " + other);
      // FindObjectOfType<PlayerHealthController>().DealDamage(); // run the DealDamage function that is inside PlayerHealthController;

      PlayerHealthController.instance.DealDamage();  // run the DealDamage function that is inside PlayerHealthController (requires an singleton instance, is more optimized)
    }

  }
}
