using UnityEngine;
using UnityEngine.UI; // use the UI elements of the unity engine

public class UIController : MonoBehaviour
{
  public static UIController instance;

  public Image heart1, heart2, heart3;

  public Sprite heartFull, heartHalf, heartEmpty;

  public Text gemText;

  public Image fadeScreen;
  public float fadeSpeed;
  private bool shouldFadeToBlack, shouldFadeFromBlack;

  private void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    UpdateGemCount(); // technically "initializing" gem count here.
  }

  // Update is called once per frame
  void Update()
  {
    HandleFadeScreen();
  }

  public void UpdateHealthDisplay()
  {
    int currentHealth = PlayerHealthController.instance.currentHealth;
    // renderHeartSprites(currentHealth);

    switch (currentHealth)
    {
      case 6:
        heart1.sprite = heartFull;
        heart2.sprite = heartFull;
        heart3.sprite = heartFull;
        break;

      case 5:
        heart1.sprite = heartFull;
        heart2.sprite = heartFull;
        heart3.sprite = heartHalf;
        break;

      case 4:
        heart1.sprite = heartFull;
        heart2.sprite = heartFull;
        heart3.sprite = heartEmpty;
        break;

      case 3:
        heart1.sprite = heartFull;
        heart2.sprite = heartHalf;
        heart3.sprite = heartEmpty;
        break;

      case 2:
        heart1.sprite = heartFull;
        heart2.sprite = heartEmpty;
        heart3.sprite = heartEmpty;
        break;

      case 1:
        heart1.sprite = heartHalf;
        heart2.sprite = heartEmpty;
        heart3.sprite = heartEmpty;
        break;

      case 0:
        heart1.sprite = heartEmpty;
        heart2.sprite = heartEmpty;
        heart3.sprite = heartEmpty;
        break;

      default:
        heart1.sprite = heartEmpty;
        heart2.sprite = heartEmpty;
        heart3.sprite = heartEmpty;
        break;
    }
  }


  public void UpdateGemCount()
  {
    // this method is used in Start() and also used in Pickup.cs 
    gemText.text = LevelManager.instance.gemsCollected.ToString(); // convert to string because gemsCollected is an int and .text expects a string
  }


  private void HandleFadeScreen()
  {

    if (shouldFadeToBlack)
    // fade into black screen
    {
      HandleFadeToBlack();
    }


    if (shouldFadeFromBlack)
    {
      HandleFadeFromBlack();
    }
  }


  private void HandleFadeToBlack()
  {
    // multiply fadeSpeed by how long it takes each update frame to go by (every update frame make it move a fraction torwards that)
    float newAlphaValue = Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime); // change alpha value (opacity)

    fadeScreen.color = new Color(
      fadeScreen.color.r,
      fadeScreen.color.g,
      fadeScreen.color.b,
      newAlphaValue
    );


    // if faded all the way in to black
    if (fadeScreen.color.a == 1f)
    {
      shouldFadeToBlack = false;
    }
  }

  private void HandleFadeFromBlack()
  {
    // fade away from black screen back to normal
    float newAlphaValue = Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime); // change alpha value (opacity)

    fadeScreen.color = new Color(
      fadeScreen.color.r,
      fadeScreen.color.g,
      fadeScreen.color.b,
      newAlphaValue
    );


    // if faded all the way in to black
    if (fadeScreen.color.a == 0f)
    {
      shouldFadeFromBlack = false;
    }
  }

  public void FadeToBlack()
  {
    shouldFadeToBlack = true;
    shouldFadeFromBlack = false;
  }

  public void FadeFromBlack()
  {
    shouldFadeToBlack = false;
    shouldFadeFromBlack = true;
  }
}
