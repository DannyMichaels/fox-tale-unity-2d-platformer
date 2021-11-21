using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
  public static PauseMenu instance;

  // string levelSelect, mainMenu: the name of the component in the Scene (use inspector tab to add).
  // for example: in scene Testing 2 the string for mainMenu is Main_Menu
  public string levelSelect, mainMenu;

  public GameObject pauseScreen;
  public bool isPaused;

  public GameObject resumeButton; // keeping reference of button so I can highlight with gamepad

  void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
    {
      PauseUnpause();
    }

    HandleLoseFocus();
  }

  public void PauseUnpause()
  {
    if (isPaused)
    {
      // unPause
      isPaused = false;
      pauseScreen.SetActive(false);
      ResumeGame();
    }
    else
    {
      // pause
      isPaused = true;
      pauseScreen.SetActive(true);

      FreezeGame();

      // for gamepad
      EventSystem.current.SetSelectedGameObject(null); // clear selected object
      EventSystem.current.SetSelectedGameObject(resumeButton); // highlight the resume button when opening (so controller can use this)
    }
  }

  // open level select scene
  public void LevelSelect()
  {
    PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name); // save the current level so when we go to level select the player will be spawned on that point
    SceneManager.LoadScene(levelSelect); // load the level select scene
    ResumeGame(); // unpause the game
  }

  public void MainMenu()
  {
    SceneManager.LoadScene(mainMenu);
    ResumeGame();
  }

  private void FreezeGame()
  {
    Time.timeScale = 0f;
  }

  private void ResumeGame()
  {
    Time.timeScale = 1f;
  }

  // @method HandleLoseFocus
  // @desc handle an edge case where user clicks away from buttons and buttons get unhighlighted which means controller can't work
  private void HandleLoseFocus()
  {
    if (!EventSystem.current.currentSelectedGameObject && isPaused)
    {
      EventSystem.current.SetSelectedGameObject(resumeButton); // highlight the resume button when opening (so controller can use this)
    }
  }
}
