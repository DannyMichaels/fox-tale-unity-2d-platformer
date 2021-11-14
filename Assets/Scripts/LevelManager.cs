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

  public void OnPlayerDeath()
  {
    // will deactivate player, wait a couple seconds, then respawn.
    StartCoroutine(RespawnCoroutine());
  }

  public void RespawnPlayer()
  {
    PlayerController.instance.transform.position = CheckpointController.instance.spawnPoint;    // respawn player in the checkpoint spawnPoint Vector3
    PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth; // reset health
    UIController.instance.UpdateHealthDisplay(); // reset the hearts UI
  }


  // https://docs.unity3d.com/Manual/Coroutines.html
  private IEnumerator RespawnCoroutine()
  {
    DeActivatePlayer();

    AudioManager.instance.PlaySFX("PLAYER_DEATH"); // play the player dead SFX

    // wait a certain amount of time and then respawn player
    yield return new WaitForSeconds(waitToRespawn);

    ActivatePlayer();

    RespawnPlayer();
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
