using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPlayer : MonoBehaviour
{
  public MapPoint currentPoint;

  public float moveSpeed = 10f;

  private bool isLevelLoading;

  public LevelSelectManager theManager;

  private bool isInputBlocked = false;

  // Start is called before the first frame update
  void Start()
  {
    isInputBlocked = false;
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
    // if (nextPoint.isLevel && nextPoint.isLocked) return; // don't move if level is locked.
    currentPoint = nextPoint;
    LevelSelectUIController.instance.HideInfoPanel();
    AudioManager.instance.PlaySFX("MAP_MOVEMENT");
  }

  private void HandleMovePlayer()
  {
    transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);
  }

  public void HandleSelectLevel()
  {
    if (!currentPoint.isLevel) return; // don't continue if it's not a level
    if (currentPoint.isLocked) return; // don't continue if the level is locked
    if (currentPoint.levelToLoad == "") return; // don't continue if there isn't a level to load

    LevelSelectUIController.instance.ShowInfoPanel(currentPoint); // if we're at this point then show the info panel

    if (Input.GetButtonDown("Jump"))
    {
      if (isInputBlocked) return;

      isInputBlocked = true; // avoid button spam so lvl selected SFX doesn't get spammed

      isLevelLoading = true;
      theManager.LoadLevel();
    }
  }
}
