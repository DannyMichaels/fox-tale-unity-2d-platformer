using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthContainer : MonoBehaviour
{
  public static HealthContainer instance;
  //import three states of the heart sprite
  public Sprite fullHeart, halfHeart, emptyHeart;


  //create a private list to store all the heart game objects
  List<GameObject> heartList = new List<GameObject>();

  //define constant size and margin variables
  const float heartSize = 80; // the size of each heart sprite
  const float heartMargin = heartSize + 10f; // the margin between the sprites

  private void Awake()
  {
    instance = this;
  }

  void Start()
  {
    GenerateHealthUI();
    UpdateHealthUI();
  }


  public void GenerateHealthUI()
  {
    int maxHealth = PlayerHealthController.instance.maxHealth;

    for (int i = 0; i < maxHealth / 2; i++)
    {
      GameObject heartGO = generateHeart(i, transform);
      heartList.Add(heartGO);
    }
  }

  GameObject generateHeart(int idx, Transform p)
  {

    GameObject heartGO = new GameObject();
    heartGO.name = $"Heart{idx}";
    Image heartImage = heartGO.AddComponent<Image>();
    heartImage.sprite = fullHeart;
    heartImage.preserveAspect = true;
    heartGO.transform.SetParent(p, false);
    heartGO.GetComponent<RectTransform>().sizeDelta = new Vector2(heartSize, heartSize);

    heartGO.GetComponent<RectTransform>().localPosition = new Vector3(idx * heartMargin, 0, 0);

    return heartGO;
  }

  public void UpdateHealthUI()
  {
    int currentHealth = PlayerHealthController.instance.currentHealth;

    for (int i = 0; i < heartList.Count; i++)
    {
      if (currentHealth >= (i + 1) * 2)
      {
        heartList[i].GetComponent<Image>().sprite = fullHeart;
      }
      else if (currentHealth == (i + 1) * 2 - 1)
      {
        heartList[i].GetComponent<Image>().sprite = halfHeart;
      }
      else
      {
        heartList[i].GetComponent<Image>().sprite = emptyHeart;
      }
    }
  }
}