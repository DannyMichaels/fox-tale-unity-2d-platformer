using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPlayer : MonoBehaviour
{
  public MapPoint currentPoint;

  public float moveSpeed = 10f;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    SetPlayerPosition();
    HandleMoveOnButtonPress();
  }

  private void HandleMoveOnButtonPress()
  {
    if (!(Vector3.Distance(transform.position, currentPoint.transform.position) < .1f)) return; // make sure the player is stopping when hitting a point.

    // GetAxis will gradually move from 0 to 1, GetAxis will be exactly  what the player is pressing as soon as pressed.
    bool moveRight = Input.GetAxisRaw("Horizontal") > .5f;
    bool moveLeft = Input.GetAxisRaw("Horizontal") < -.5f;
    bool moveUp = Input.GetAxisRaw("Vertical") > .5f;
    bool moveDown = Input.GetAxisRaw("Vertical") < -.5f;

    if (moveRight)
    {
      if (currentPoint.right != null)
      {
        SetNextPoint(currentPoint.right);
      }
    }

    else if (moveLeft)
    {
      if (currentPoint.left != null)
      {
        SetNextPoint(currentPoint.left);
      }
    }

    else if (moveUp)
    {
      if (currentPoint.up != null)
      {
        SetNextPoint(currentPoint.up);
      }
    }

    else if (moveDown)
    {
      if (currentPoint.down != null)
      {
        SetNextPoint(currentPoint.down);
      }
    }

  }

  public void SetNextPoint(MapPoint nextPoint)
  {
    currentPoint = nextPoint;
  }

  private void SetPlayerPosition()
  {
    transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);
  }
}
