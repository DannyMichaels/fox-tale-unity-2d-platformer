using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  public static LevelManager instance;

  public float waitToRespawn; // time to wait before respawing

  public int gemsCollected;

  public string levelToLoad;

  public float timeInLevel;

  void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    timeInLevel = 0f;
  }

  // Update is called once per frame
  void Update()
  {
    IncrementTimeInLevel();
  }

  public void RespawnPlayer()
  {
    // will deactivate player, wait a couple seconds, then respawn.
    StartCoroutine(RespawnCoroutine());
  }

  // https://docs.unity3d.com/Manual/Coroutines.html
  private IEnumerator RespawnCoroutine()
  {
    PlayerController.instance.gameObject.SetActive(false); // make player dissapear (deactivate)

    AudioManager.instance.PlaySFX("PLAYER_DEATH"); // play the player dead SFX

    float fadeWaitTime = 1f / UIController.instance.fadeSpeed; // the amount of time it would take the screen to fade


    // if no fade effects exist, just keep waitToRespawn and remove the subtraction of fadeWaitTime 
    yield return new WaitForSeconds(waitToRespawn - fadeWaitTime);  // wait a certain amount of time and then continue the function 

    UIController.instance.FadeToBlack(); // fade to black

    // then wait the amount that it should take to fade.
    yield return new WaitForSeconds(fadeWaitTime + .2f); // add a bit more time with .2f (fraction of a section) so it stays fully black

    // continue the function and respawn the player

    UIController.instance.FadeFromBlack(); // fade out from black back to normal

    PlayerController.instance.gameObject.SetActive(true); // make player appear (activate)

    PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;    // respawn player in the checkpoint spawnPoint Vector3
    PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth; // reset health

    UIController.instance.UpdateHealthDisplay(); // reset the hearts UI
  }


  public void EndLevel()
  {
    StartCoroutine(EndLevelCoroutine());
  }

  public IEnumerator EndLevelCoroutine()
  {
    PlayerController.instance.stopInput = true; // stop player input
    CameraController.instance.stopFollowingTarget = true; // stop following the player

    UIController.instance.levelCompleteText.SetActive(true); // show lvl complete text

    yield return new WaitForSeconds(1.5f);

    UIController.instance.FadeToBlack();

    yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed) + .25f);  // wait for it to fade, then wait another quarter of a second

    UnlockCurrentAndNextLevel();
    UpdateLevelStats(); // update level stats so we can see them in lvl select

    // load into the next level
    SceneManager.LoadScene(levelToLoad);
  }


  private void IncrementTimeInLevel()
  {
    timeInLevel += Time.deltaTime;
  }

  private void UnlockCurrentAndNextLevel()
  {
    // unlock the completed level which also unlocks next lvl due to levelToCheck var (ex: level1-1_unlocked), SetInt(key, value)  
    PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
  }

  private void UpdateLevelStats()
  {
    PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected); // set gems collected for that level that was just completed so we can see in lvl select
    PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel); // set time in level for level select to see
  }
}
