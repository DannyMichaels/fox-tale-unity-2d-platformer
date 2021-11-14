using UnityEngine;

public class CheckpointController : MonoBehaviour
{
  public static CheckpointController instance;

  private Checkpoint[] checkpoints; // Checkpoint type from Checkpoint.cs

  public Vector3 spawnPoint; // we make it a Vector3 because it's going to be a position in the world (x,y,z) which is what a vector3 gives.

  private void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    FindCheckpoints();
    InitializeSpawnPoint();
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
    // ONLY FINDS OBJECTS THAT ARE ACTIVATED
    checkpoints = FindObjectsOfType<Checkpoint>();
  }


  public void DeactivateCheckpoints()
  {
    foreach (Checkpoint checkpoint in checkpoints)
    {
      checkpoint.ResetCheckPoint(); // ResetCheckPoint: that method exists in Checkpoint.cs script file
    }
  }

  public void SetSpawnPoint(Vector3 newSpawnPoint)
  {
    spawnPoint = newSpawnPoint;
  }

  private void InitializeSpawnPoint()
  {
    SetSpawnPoint(PlayerController.instance.transform.position); // when the level starts, make sure the spawn point is initialized
  }
}
