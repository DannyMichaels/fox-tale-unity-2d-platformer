using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
  public Transform[] points;
  public float moveSpeed;
  public int currentPoint;

  public Transform platform;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    MoveTorwardsCurrentPoint();
    HandleChangePoint();
  }

  private void MoveTorwardsCurrentPoint()
  {
    platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, moveSpeed * Time.deltaTime); // MoveTorwards(current, target, maxDistanceDelta)
  }

  private void HandleChangePoint()
  {

    // if it's close to current point (just before it hits)
    if (Vector3.Distance(platform.position, points[currentPoint].position) < .05f)
    {
      currentPoint++;

      if (currentPoint >= points.Length)
      {
        currentPoint = 0;
      }
    }
  }
}
