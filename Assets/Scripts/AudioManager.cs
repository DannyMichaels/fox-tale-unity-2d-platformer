using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager instance;

  public AudioSource[] soundEffects;

  //D Dictionary: like a hashmap 
  public Dictionary<string, int> SOUNDS = new Dictionary<string, int>
  {
    { "BOSS_HIT", 0 },
    { "BOSS_IMPACT", 1 },
    { "BOSS_SHOT", 2 },
    { "ENEMY_EXPLODE", 3 },
    { "LEVEL_SELECTED", 4 },
    { "MAP_MOVEMENT", 5 },
    { "PICKUP_GEM", 6 },
    { "PICKUP_HEALTH", 7 },
    { "PLAYER_DEATH", 8 },
    { "PLAYER_HURT", 9 },
    { "PLAYER_JUMP", 10 },
    { "WARP_JINGLE", 11 },
  };


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

  public void PlaySFX(string SOUND_NAME)
  {
    int soundToPlayIndex = SOUNDS[SOUND_NAME];

    soundEffects[soundToPlayIndex].Stop(); // if it's already playing stop it from playing.

    soundEffects[soundToPlayIndex].Play(); // play the sound
  }
}
