using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  public static LevelManager instance;

  public float waitToRespawn; // time to wait before respawing

  public int gemsCollected;

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

  }

  public void StartPlayerRespawn()
  {
    // will deactivate player, wait a couple seconds, then respawn.
    StartCoroutine(RespawnCoroutine());
  }

  // https://docs.unity3d.com/Manual/Coroutines.html
  private IEnumerator RespawnCoroutine()
  {
    DeActivatePlayer();

    AudioManager.instance.PlaySFX("PLAYER_DEATH"); // play the player dead SFX

    float fadeWaitTime = 1f / UIController.instance.fadeSpeed; // the amount of time it would take the screen to fade


    // if no fade effects exist, just keep waitToRespawn and remove the subtraction of fadeWaitTime 
    yield return new WaitForSeconds(waitToRespawn - fadeWaitTime);  // wait a certain amount of time and then continue the function 

    UIController.instance.FadeToBlack(); // fade to black

    // then wait the amount that it should take to fade.
    yield return new WaitForSeconds(fadeWaitTime + .2f); // add a bit more time with .2f (fraction of a section) so it stays fully black

    // continue the function and respawn the player

    UIController.instance.FadeFromBlack(); // fade out from black back to normal

    ActivatePlayer();
    RespawnPlayer();
  }

  private void RespawnPlayer()
  {
    PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;    // respawn player in the checkpoint spawnPoint Vector3
    PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth; // reset health
    UIController.instance.UpdateHealthDisplay(); // reset the hearts UI
  }


  private void DeActivatePlayer()
  {
    PlayerController.instance.gameObject.SetActive(false); // make player dissapear (deactivate)
  }

  private void ActivatePlayer()
  {
    PlayerController.instance.gameObject.SetActive(true); // make player appear (activate)
  }
}
