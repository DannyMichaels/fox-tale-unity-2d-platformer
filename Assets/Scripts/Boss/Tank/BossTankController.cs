using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
  public Transform theBoss;
  public Animator animator;

  // [Header("string")] @desc: Categorize for Unity UI.
  [Header("Movement")]
  public float moveSpeed;
  public Transform leftPoint, rightPoint;
  private bool shouldMoveRight;


  [Header("Shooting")]
  public GameObject bullet;
  public Transform firePoint; // where the bullets are going to be fired from
  public float timeBetweenShots; // time to wait between each shot
  private float shotCounter;

  [Header("Hurt")]
  public float hurtTime; // time to wait after gets hurt before continuing
  private float hurtCounter; // counter to start after gets hurt

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
