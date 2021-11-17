using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectCameraController : MonoBehaviour
{

  // minPosition: lowest camera can go, and farthest left it can go. 
  // maxPosition: highest camera can go, and farthest right it can go.
  public Vector2 minPosition, maxPosition;

  public Transform target;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  // LateUpdate happens just after Update is called on all scripts (so it will follow AFTER the player has moved)
  void LateUpdate()
  {
    MakeCameraFollowTarget();
  }

  private void MakeCameraFollowTarget()
  {
    float xPosition = Mathf.Clamp(target.position.x, minPosition.x, maxPosition.x);
    float yPosition = Mathf.Clamp(target.position.y, minPosition.y, maxPosition.y);

    transform.position = new Vector3(xPosition, yPosition, transform.position.z);
  }
}
