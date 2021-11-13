using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public float moveSpeed;

  public Transform leftPoint, rightPoint;

  private bool movingRight;

  private Rigidbody2D theRB;

  // Start is called before the first frame update
  void Start()
  {
    initializeRigidBody();
    initializeRightAndLeftPoints();
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void initializeRightAndLeftPoints()
  {
    // remove the parent so when the frog moves they stay in the same position.
    leftPoint.parent = null;
    rightPoint.parent = null;
  }

  private void initializeRigidBody()
  {
    theRB = GetComponent<Rigidbody2D>();
  }
}
