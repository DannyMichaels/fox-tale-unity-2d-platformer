using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUIController : MonoBehaviour
{
  public static LevelSelectUIController instance;

  public Image fadeScreen;
  public float fadeSpeed;
  private bool shouldFadeToBlack, shouldFadeFromBlack;

  public GameObject levelInfoPanel;

  public Text levelName, gemsFound, gemsTarget, bestTime, timeTarget;

  void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    FadeFromBlack(); // fade out as soon as lvl starts
  }

  // Update is called once per frame
  void Update()
  {
    HandleFadeScreen();
  }


  private void HandleFadeScreen()
  {

    if (shouldFadeToBlack)
    // fade into black screen
    {
      HandleFadeToBlack();
    }


    if (shouldFadeFromBlack)
    {
      HandleFadeFromBlack();
    }
  }


  private void HandleFadeToBlack()
  {
    // multiply fadeSpeed by how long it takes each update frame to go by (every update frame make it move a fraction torwards that)
    float newAlphaValue = Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime); // change alpha value (opacity)

    fadeScreen.color = new Color(
      fadeScreen.color.r,
      fadeScreen.color.g,
      fadeScreen.color.b,
      newAlphaValue
    );


    // if faded all the way in to black
    if (fadeScreen.color.a == 1f)
    {
      shouldFadeToBlack = false;
    }
  }

  private void HandleFadeFromBlack()
  {
    // fade away from black screen back to normal
    float newAlphaValue = Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime); // change alpha value (opacity)

    fadeScreen.color = new Color(
      fadeScreen.color.r,
      fadeScreen.color.g,
      fadeScreen.color.b,
      newAlphaValue
    );


    // if faded all the way in to black
    if (fadeScreen.color.a == 0f)
    {
      shouldFadeFromBlack = false;
    }
  }

  public void FadeToBlack()
  {
    shouldFadeToBlack = true;
    shouldFadeFromBlack = false;
  }

  public void FadeFromBlack()
  {
    shouldFadeToBlack = false;
    shouldFadeFromBlack = true;
  }

  public void ShowInfoPanel(MapPoint levelInfo)
  {
    levelName.text = levelInfo.levelName;

    gemsFound.text = "FOUND: " + levelInfo.gemsCollected;
    gemsTarget.text = "IN LEVEL: " + levelInfo.totalGems;

    timeTarget.text = "TARGET: " + levelInfo.targetTime + "s";

    bestTime.text = "BEST: ";                                                       // F1: display as float number with 1 decimal place, F2: 2 decimal places
    bestTime.text += levelInfo.bestTime == 0 ? "---" : levelInfo.bestTime.ToString("F2") + "s";

    levelInfoPanel.SetActive(true);
  }

  public void HideInfoPanel()
  {
    levelInfoPanel.SetActive(false);
  }
}
