using UnityEngine;
using UnityEngine.UI; // use the UI elements of the unity engine

public class UIController : MonoBehaviour
{
  public static UIController instance;
  public Image heart1, heart2, heart3;
  public Sprite heartFull, heartEmpty;

  private void Awake()
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


  public void UpdateHealthDisplay()
  {
    int currentHealth = PlayerHealthController.instance.currentHealth;
    switch (currentHealth)
    {
      case 3:
        heart1.sprite = heartFull;
        heart2.sprite = heartFull;
        heart3.sprite = heartFull;
        break;
      case 2:
        heart1.sprite = heartFull;
        heart2.sprite = heartFull;
        heart3.sprite = heartEmpty;
        break;
      case 1:
        heart1.sprite = heartFull;
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
}
