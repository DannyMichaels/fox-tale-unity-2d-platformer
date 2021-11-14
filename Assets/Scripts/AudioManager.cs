using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager instance;

  public AudioSource[] soundEffects;

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

  public void PlaySFX(int soundToPlayIndex)
  {
    soundEffects[soundToPlayIndex].Stop(); // if it's already playing stop it from playing.

    soundEffects[soundToPlayIndex].Play(); // play the sound
  }
}
