using UnityEngine;

public class MapPoint : MonoBehaviour
{
  public MapPoint up, right, down, left;
  public bool isLevel, isLocked;
  public string levelToLoad, levelToCheck; // levelToCheck: previous lvl
  public string levelName;

  public int gemsCollected, totalGems;
  public float bestTime, targetTime;

  // Start is called before the first frame update
  void Start()
  {
    HandleLevelLocked();
    HandleLevelStats();
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

      // line 43: edgecase: when user never completed level 1, pauses and goes to level select, make sure that lvl 1 is not locked because it wasn't completed
      if (levelToLoad == levelToCheck)
      {
        isLocked = false;
      }
    }
  }

  private void HandleLevelStats()
  {
    if (!isLevel || isLevel && levelToLoad == null) return;

    // set gems stats
    if (PlayerPrefs.HasKey(levelToLoad + "_gems"))
    {
      gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems");
    }

    // set time stats
    if (PlayerPrefs.HasKey(levelToLoad + "_time"))
    {

      bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time");
    }
  }
}
