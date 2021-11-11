using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
  public SpriteRenderer theSR;
  public Sprite checkPointOn, checkPointOff;

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
    bool isCollidedWithPlayer = other.CompareTag("Player");
    if (isCollidedWithPlayer)
    {
      CheckpointController.instance.DeactivateCheckpoints();  // turn off all previous checkpoints
      theSR.sprite = checkPointOn; // turn on the newly found checkpoint
    }
  }


  public void ResetCheckPoint()
  {
    theSR.sprite = checkPointOff;
  }
}
