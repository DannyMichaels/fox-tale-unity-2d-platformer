using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
  public string startScene, continueScene;
  private bool isGamepadConnected = false;
  private bool prevIsGamepadConnected = true;

  public GameObject continueButton;
  public GameObject startButton;

  // Start is called before the first frame update
  void Start()
  {
    DetectGamepad();
    InitializeContinueButtonVisibility();
  }

  // Update is called once per frame
  void Update()
  {
    DetectGamepad();
    HandleLoseFocus();
  }

  public void StartGame()
  {
    SceneManager.LoadScene(startScene);
  }

  public void ExitGame()
  {
    Application.Quit();
    Debug.Log("Exiting Game");
  }

  public void ContinueGame()
  {
    SceneManager.LoadScene(continueScene);
  }

  private void InitializeContinueButtonVisibility()
  {
    // if it has that key it means first level has been played
    if (PlayerPrefs.HasKey(startScene + "_unlocked"))
    {
      continueButton.SetActive(true);
    }
    else
    {
      continueButton.SetActive(false);
    }
  }

  private void DetectGamepad()
  {
    isGamepadConnected = Utils.DetectGamepad(); // utils from Utils.cs
    OnGamepadDetectionChange();
  }

  private void OnGamepadDetectionChange()
  {
    // don't rerun this if the state didn't change
    if (prevIsGamepadConnected == isGamepadConnected) return;

    if (isGamepadConnected)
    {
      // for gamepad
      EventSystem.current.SetSelectedGameObject(null); // clear selected object
      EventSystem.current.SetSelectedGameObject(startButton); // highlight the resume button when opening (so controller can use this)
    }
    else
    {
      EventSystem.current.SetSelectedGameObject(null); // clear selected object
    }

    prevIsGamepadConnected = isGamepadConnected;
  }




  // @method HandleLoseFocus
  // @desc handle an edge case where user clicks away from buttons and buttons get unhighlighted which means controller can't work
  private void HandleLoseFocus()
  {
    if (!isGamepadConnected) return;

    if (!EventSystem.current.currentSelectedGameObject)
    {
      EventSystem.current.SetSelectedGameObject(startButton); // highlight the resume button when opening (so controller can use this)
    }
  }

}
