using UnityEngine;

// for picking items up
public class Pickup : MonoBehaviour
{
  public bool isGem;

  private bool isCollected; // avoid duplicate spam collection of same item (if player has multiple colliders)

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
        LevelManager.instance.gemsCollected += 1;

        isCollected = true;

        Destroy(gameObject); // destroy the collected gem
      }
    }
  }
}
