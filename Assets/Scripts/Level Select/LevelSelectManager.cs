using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
  public LevelSelectPlayer thePlayer;

  private MapPoint[] allPoints;

  // Start is called before the first frame update
  void Start()
  {
    InitializeAllPoints();
    InitializePlayerCurrentPoint();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void LoadLevel()
  {
    StartCoroutine(LoadLevelCoroutine());
  }

  public IEnumerator LoadLevelCoroutine()
  {
    AudioManager.instance.PlaySFX("LEVEL_SELECTED");

    LevelSelectUIController.instance.FadeToBlack();

    yield return new WaitForSeconds((1f / LevelSelectUIController.instance.fadeSpeed) + .25f);

    SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
  }

  private void InitializeAllPoints()
  {
    allPoints = FindObjectsOfType<MapPoint>(); // find all map points and make sure all points is equal to that
  }

  private void InitializePlayerCurrentPoint()
  {
    // if player doesn't have "CurrentLevel" (set in LevelManager) key then that means we don't need to run this.
    if (!PlayerPrefs.HasKey("CurrentLevel")) return;


    foreach (MapPoint point in allPoints)
    {
      // if it's not a level there is no point, just continue to the next iteration of the loop
      if (!point.isLevel) continue;

      // if the points levelToLoad is equal to the value of PlayerPrefs["currentLevel"]
      if (point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
      {
        thePlayer.transform.position = point.transform.position;     // move the player to the point position
        thePlayer.currentPoint = point; // set the current point to be equal to that point
      }
    }
  }
}
