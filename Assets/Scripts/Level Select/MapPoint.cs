using UnityEngine;

public class MapPoint : MonoBehaviour
{
  public MapPoint up, right, down, left;
  public bool isLevel, isLocked;
  public string levelToLoad, levelToCheck; // levelToCheck: previous lvl

  // Start is called before the first frame update
  void Start()
  {
    HandleLevelLocked();
  }

  // Update is called once per frame
  void Update()
  {

  }

  // check if the level should be locked or not
  private void HandleLevelLocked()
  {
    if (isLevel && levelToLoad != null)
    {
      isLocked = true; // by default, all mapPoints will be locked.

      if (levelToCheck != null)
      {
        // if it has the levelToCheck (previous level) key it means the level has been played.
        if (PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
        {

          // if it's equal to 1 it means that the level has been unlocked
          if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
          {
            isLocked = false; // set isLocked to false so player can select this level
          }
        }
      }
    }
  }
}
