using UnityEngine;

public class CameraController : MonoBehaviour
{
  public Transform target; // for example: the target is the player
  public Transform farBackground, middleBackground;

  public float minHeight, maxHeight; // as low as we want our camera to go, as high as we want our camera to go

  private float lastXPosition;
  private float lastYPosition;

  // Start is called before the first frame update
  void Start()
  {
    setLastXAndYPositions();
  }

  // Update is called once per frame
  void Update()
  {
    FollowTargetVerticalAndHorizontal();
    MakeBackgroundFollowCamera();
    setLastXAndYPositions();
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

    // parallax horizontal
    float amountToMoveX = transform.position.x - lastXPosition;
    farBackground.position += new Vector3(amountToMoveX, 0f, 0f);
    middleBackground.position += new Vector3(amountToMoveX * .5f, 0f, 0f);


    // parallax vertical
    float amountToMoveY = transform.position.y - lastYPosition;
    farBackground.position += new Vector3(amountToMoveY, 0f, 0f);
    middleBackground.position += new Vector3(amountToMoveY * .5f, 0f, 0f);
  }


  void setLastXAndYPositions()
  {
    lastXPosition = transform.position.x;
    lastYPosition = transform.position.y;
  }
}
