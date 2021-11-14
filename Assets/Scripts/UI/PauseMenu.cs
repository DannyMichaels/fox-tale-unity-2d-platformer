using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  // string levelSelect, mainMenu: the name of the component in the Scene (use inspector tab to add).
  // for example: in scene Testing 2 the string for mainMenu is Main_Menu
  public string levelSelect, mainMenu;

  public GameObject pauseScreen;
  public bool isPaused;

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
    }
  }

  public void LevelSelect()
  {
    SceneManager.LoadScene(levelSelect);
  }

  public void MainMenu()
  {
    SceneManager.LoadScene(mainMenu);
  }

  private void FreezeGame()
  {
    Time.timeScale = 0f;
  }

  private void ResumeGame()
  {
    Time.timeScale = 1f;
  }
}
