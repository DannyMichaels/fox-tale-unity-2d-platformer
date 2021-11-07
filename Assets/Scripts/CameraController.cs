using UnityEngine;

public class CameraController : MonoBehaviour
{
  public Transform target; // for example: the target is the player
  public Transform farBackground, middleBackground;

  private float lastXPosition;

  // Start is called before the first frame update
  void Start()
  {
    lastXPosition = transform.position.x;
  }

  // Update is called once per frame
  void Update()
  {
    FollowTargetHorizontal();
    MakeBackgroundFollowCamera();
    lastXPosition = transform.position.x;
  }

  void FollowTargetHorizontal()
  {
    transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    Debug.Log("transform position " + transform.position);
  }

  // know how much camera is moving.
  void MakeBackgroundFollowCamera()
  {
    float amountToMoveX = transform.position.x - lastXPosition;
    farBackground.position += new Vector3(amountToMoveX, 0f, 0f);
    middleBackground.position += new Vector3(amountToMoveX * .5f, 0f, 0f);
  }
}
