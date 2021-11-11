using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
  public static CheckpointController instance;

  private Checkpoint[] checkpoints;

  private void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    FindCheckpoints();
  }

  // Update is called once per frame
  void Update()
  {

  }

  /* 
  @method FindCheckpoints
  @desc find all checkpoints in the scene. 
  will set checkpoints to equal all checkpoints in the scene
  */
  private void FindCheckpoints()
  {
    checkpoints = FindObjectsOfType<Checkpoint>();
  }
}
