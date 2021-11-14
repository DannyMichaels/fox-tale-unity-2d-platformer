using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager instance;

  public AudioSource[] soundEffects;

  // Dictionary: like a hashmap 
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


  /* 
    @method PlaySFX
    @param {string} SOUND_NAME
    @desc plays a soundEffect based on the string passed
  */
  public void PlaySFX(string SOUND_NAME)
  {
    AudioSource soundToPlay = GetSound(SOUND_NAME);

    soundToPlay.Stop(); // if it's already playing stop it from playing.

    ChangeSFXPitch(soundToPlay); // give the same sound some variety by giving it a new random pitch from .9f to 1.1f

    soundToPlay.Play(); // play the sound
  }


  /* 
    @method GetSound
    @param {string} SOUND_NAME
    @desc takes the name of sound string, then uses the SOUNDS Dictionary/HashMap to get the index.
    @returns AudioSource result: soundEffects[index].
  */
  private AudioSource GetSound(string SOUND_NAME)
  {
    int soundToPlayIndex = SOUNDS[SOUND_NAME];
    AudioSource foundSoundResult = soundEffects[soundToPlayIndex];

    return foundSoundResult;
  }

  // @method ChangeSFXPitch
  // @desc give the selected SFX some variety by giving it a new random pitch from .9f to 1.1f
  // example: collecting multiple gems at same time, change pitch every time
  private void ChangeSFXPitch(AudioSource sfx)
  {
    sfx.pitch = Random.Range(.9f, 1.1f);
  }
}
