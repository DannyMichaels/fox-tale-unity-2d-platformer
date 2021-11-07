using UnityEngine;

public class CameraController : MonoBehaviour
{
  public Transform target; // for example: the target is the player

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    FollowTargetHorizontal();
  }

  void FollowTargetHorizontal()
  {
    transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
  }
}
