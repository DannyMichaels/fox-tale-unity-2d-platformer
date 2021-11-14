using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public string startScene;
  private bool isGamepadConnected;

  public GameObject[] clickableButtons;

  // Start is called before the first frame update
  void Start()
  {
    DetectGamepad();
    OnGamepadDetectionChange();
  }

  // Update is called once per frame
  void Update()
  {
    DetectGamepad();
    OnGamepadDetectionChange();
  }

  public void StartGame()
  {
    SceneManager.LoadScene(startScene);
  }

  public void ExitGame()
  {
    Application.Quit();
    // Debug.Log("Exiting Game");
  }

  private void DetectGamepad()
  {
    //Get Joystick Names
    string[] temp = Input.GetJoystickNames();

    //Check whether array contains anything
    if (temp.Length > 0)
    {
      //Iterate over every element
      for (int i = 0; i < temp.Length; ++i)
      {
        //Check if the string is empty or not
        if (!string.IsNullOrEmpty(temp[i]))
        {
          //Not empty, controller temp[i] is connected
          Debug.Log("Controller " + i + " is connected using: " + temp[i]);
          isGamepadConnected = true;
        }
        else
        {
          //If it is empty, controller i is disconnected
          //where i indicates the controller number
          Debug.Log("Controller: " + i + " is disconnected.");
          isGamepadConnected = false;
        }
      }
    }
  }


  // if we have a gamepad we need to replace clickable buttons with something else that a controller can use.
  private void OnGamepadDetectionChange()
  {
    if (isGamepadConnected)
    {
      DeactivateClickButtons();
    }
    else
    {
      ActivateClickButtons();
    }
  }

  private void DeactivateClickButtons()
  {
    foreach (GameObject button in clickableButtons)
    {
      button.SetActive(false);
    }
  }

  private void ActivateClickButtons()
  {
    foreach (GameObject button in clickableButtons)
    {
      button.SetActive(true);
    }
  }
}
