using System.Collections.Generic;
using UnityEngine.UI; // This is so that it should find the Text component
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
  public string startScene;
  private bool isGamepadConnected;

  public GameObject[] clickableButtons;


  // for the gamepad buttons when clickable buttons are disabled
  public GameObject[] selectButtons;
  private bool isStartSelectHovered = true;
  private bool isExitSelectHovered = false;

  // Start is called before the first frame update
  void Start()
  {
    EventSystem.current.SetSelectedGameObject(clickableButtons[0]);
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
      isStartSelectHovered = true;
      isExitSelectHovered = false;
    }

    if (Input.GetAxisRaw("Vertical") < 0)
    {
      isStartSelectHovered = false;
      isExitSelectHovered = true;
    }

    GameObject selectButtonStart = selectButtons[0];
    GameObject selectButtonExit = selectButtons[1];


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
  }





  // @method HandleLoseFocus
  // @desc handle an edge case where user clicks away from buttons and buttons get unhighlighted which means controller can't work
  private void HandleLoseFocus()
  {
    if (!EventSystem.current.currentSelectedGameObject)
    {
      EventSystem.current.SetSelectedGameObject(clickableButtons[0]); // highlight the resume button when opening (so controller can use this)
    }
  }
}
