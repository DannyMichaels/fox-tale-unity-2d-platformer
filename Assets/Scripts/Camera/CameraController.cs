using UnityEngine;

public class CameraController : MonoBehaviour
{
  public Transform target; // for example: the target is the player
  public Transform farBackground, middleBackground;

  public float minHeight, maxHeight; // as low as we want our camera to go, as high as we want our camera to go

  // private float lastXPosition;
  // private float lastYPosition;
  private Vector2 lastPosition;

  // Start is called before the first frame update
  void Start()
  {
    // setLastXAndYPositions();
    setLastCameraPosition();
  }

  // Update is called once per frame
  void Update()
  {
    FollowTargetVerticalAndHorizontal();
    MakeBackgroundFollowCamera();
    // setLastXAndYPositions();
    setLastCameraPosition();
  }

  // void FollowTargetHorizontal()
  // {
  //   transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
  // }

  void FollowTargetVerticalAndHorizontal()
  {
    /* transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

     float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);  // get Y position for camera but make sure it's only between min and max height
     transform.position = new Vector3(transform.position.x, clampedY, transform.position.z); */


    float clampedY = Mathf.Clamp(target.position.y, minHeight, maxHeight);  // get Y position for camera but make sure it's only between min and max height
    transform.position = new Vector3(target.position.x, clampedY, transform.position.z);
  }


  void MakeBackgroundFollowCamera()
  {
    // float amountToMoveX = transform.position.x - lastXPosition;   // parallax horizontal
    // float amountToMoveY = transform.position.y - lastYPosition;   // parallax vertical
    // farBackground.position += new Vector3(amountToMoveX, amountToMoveY, 0f);
    // middleBackground.position += new Vector3(amountToMoveX * .5f, amountToMoveY * .5f, 0f);


    // both vertical and horizontal
    Vector2 amountToMove = new Vector2(transform.position.x - lastPosition.x, transform.position.y - lastPosition.y);
    farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
    middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * .5f;
  }


  // void setLastXAndYPositions()
  // {
  //   lastXPosition = transform.position.x;
  //   lastYPosition = transform.position.y;
  // }

  void setLastCameraPosition()
  {
    lastPosition = transform.position; // get x and y, because this is Vector2 and not 3 it will chop off the Z value
  }
}
