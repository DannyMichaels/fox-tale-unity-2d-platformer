using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPlayer : MonoBehaviour
{
  public MapPoint currentPoint;

  public float moveSpeed = 10f;

  private bool isLevelLoading;

  public LevelSelectManager theManager;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    HandleMovePlayer();
    SetCurrentPointOnButtonPress();
    HandleSelectLevel();
  }

  private void SetCurrentPointOnButtonPress()
  {
    if (!(Vector3.Distance(transform.position, currentPoint.transform.position) < .1f)) return; // make sure the player is stopping when hitting a point.
    if (isLevelLoading) return; // if level is loading don't allow to move around

    // GetAxis will gradually move from 0 to 1, GetAxis will be exactly  what the player is pressing as soon as pressed.
    bool rightPressed = Input.GetAxisRaw("Horizontal") > .5f;
    bool leftPressed = Input.GetAxisRaw("Horizontal") < -.5f;
    bool upPressed = Input.GetAxisRaw("Vertical") > .5f;
    bool downPressed = Input.GetAxisRaw("Vertical") < -.5f;

    if (rightPressed)
    {
      if (currentPoint.right != null)
      {
        SetNextPoint(currentPoint.right);
      }
    }

    else if (leftPressed)
    {
      if (currentPoint.left != null)
      {
        SetNextPoint(currentPoint.left);
      }
    }

    else if (upPressed)
    {
      if (currentPoint.up != null)
      {
        SetNextPoint(currentPoint.up);
      }
    }

    else if (downPressed)
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

  private void HandleMovePlayer()
  {
    transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);
  }

  public void HandleSelectLevel()
  {
    if (!currentPoint.isLevel) return;

    if (Input.GetButtonDown("Jump"))
    {
      isLevelLoading = true;

      theManager.LoadLevel();
    }
  }
}
