using System.Collections.Generic;
using UnityEngine.UI; // This is so that it should find the Text component
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
  public string startScene, continueScene;
  private bool isGamepadConnected;

  public GameObject[] clickableButtons;

  // for the gamepad buttons when clickable buttons are disabled
  public GameObject[] selectButtons;
  private bool isStartSelectHovered = true;
  private bool isExitSelectHovered = false;
  private bool isContinueSelectHovered = false;

  // Start is called before the first frame update
  void Start()
  {
    DetectGamepad();
  }

  // Update is called once per frame
  void Update()
  {
    // NOTE: this isn't an elegant way to handle menu controls with gamepad, check how it's done on PauseMenu.cs with SetSelectedGameObject
    // all of this logic of having separate select and clickable buttons is not needed
    DetectGamepad();
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
          // Not empty, controller temp[i] is connected
          // Debug.Log("Controller " + i + " is connected using: " + temp[i]);
          isGamepadConnected = true;
          OnGamepadDetectionChange();
        }
        else
        {
          // If it is empty, controller i is disconnected
          // where i indicates the controller number
          // Debug.Log("Controller: " + i + " is disconnected.");
          isGamepadConnected = false;
          OnGamepadDetectionChange();
        }
      }
    }

  }


  // if we have a gamepad we need to replace clickable buttons with something else that a controller can use.
  // this is actually not necessary at all, check PauseMenu.cs to see how to handle gamepad controls on buttons and menus.
  private void OnGamepadDetectionChange()
  {
    if (isGamepadConnected)
    {
      ActivateGamepadSelect();
      DeactivateClickButtons();
      HandleGamepadButtons();
    }
    else
    {
      DeActivateGamepadSelect();
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


  private void ActivateGamepadSelect()
  {
    if (!selectButtons[0].activeSelf && !selectButtons[1].activeSelf)
    {
      foreach (GameObject selectButton in selectButtons)
      {
        selectButton.SetActive(true);
      }
    }
  }

  private void DeActivateGamepadSelect()
  {
    if (selectButtons[0].activeSelf && selectButtons[1].activeSelf)
    {
      foreach (GameObject selectButton in selectButtons)
      {
        selectButton.SetActive(false);
      }
    }
  }

  public void HandleGamepadButtons()
  {
    if (!isGamepadConnected) return;

    if (Input.GetAxisRaw("Vertical") > 0)
    {
      if (isStartSelectHovered)
      {
        isStartSelectHovered = false;
        isContinueSelectHovered = true;
      }
      else
      {
        isStartSelectHovered = true;
      }

      isExitSelectHovered = false;
    }

    if (Input.GetAxisRaw("Vertical") < 0)
    {
      isContinueSelectHovered = false;
      isStartSelectHovered = false;
      isExitSelectHovered = true;
    }

    GameObject selectButtonStart = selectButtons[0];
    GameObject selectButtonExit = selectButtons[1];
    GameObject selectButtonContinue = selectButtons[2];


    if (isStartSelectHovered)
    {
      selectButtonStart.GetComponentInChildren<Text>().color = Color.yellow;
      if (Input.GetButtonDown("Jump"))
      {
        StartGame();
      }
    }
    else
    {
      selectButtonStart.GetComponentInChildren<Text>().color = Color.white;
    }

    if (isExitSelectHovered)
    {
      selectButtonExit.GetComponentInChildren<Text>().color = Color.yellow;

      if (Input.GetButtonDown("Jump"))
      {
        ExitGame();
      }
    }
    else
    {
      selectButtonExit.GetComponentInChildren<Text>().color = Color.white;
    }


    if (isContinueSelectHovered)
    {
      selectButtonContinue.GetComponentInChildren<Text>().color = Color.yellow;

      if (Input.GetButtonDown("Jump"))
      {
        ContinueGame();
      }
    }
    else
    {
      selectButtonContinue.GetComponentInChildren<Text>().color = Color.white;
    }
  }
}
