using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassPlayerController : MonoBehaviour
{
  public GameObject planet;
  public Transform north;
  public Transform south;

  public float moveSpeed = 10f;

  private Rigidbody rigidBody;
  private Rigidbody planetRigidBody;

  private Vector3 moveDir;

  void Start()
  {
    rigidBody = GetComponent<Rigidbody>();
    planetRigidBody = planet.GetComponent<Rigidbody>();

    rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
    rigidBody.useGravity = false;
  }

  void Update()
  {
    compass.transform.position = transform.position;

    float offset = 10f;
    Vector3 upDirection = compass.transform.rotation * Vector3.forward;
    Vector3 upPosition = compass.transform.position + upDirection * offset;
    Debug.DrawLine(compass.transform.position, upPosition, Color.blue);

    Vector3 downDirection = compass.transform.rotation * -Vector3.forward;
    Vector3 downPosition = compass.transform.position + downDirection * offset;
    Debug.DrawLine(compass.transform.position, downPosition, Color.white);

    Vector3 rightDirection = compass.transform.rotation * Vector3.right;
    Vector3 rightPosition = compass.transform.position + rightDirection * offset;
    Debug.DrawLine(compass.transform.position, rightPosition, Color.green);

    Vector3 leftDirection = compass.transform.rotation * -Vector3.right;
    Vector3 leftPosition = compass.transform.position + leftDirection * offset;
    Debug.DrawLine(compass.transform.position, leftPosition, Color.red);


    moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));


    //MovePlayer();
    // Direction(transform.position);
    // if (Input.GetKey(KeyCode.W))
    // {
    //   rigidBody.MovePosition(rigidBody.position + upDirection * moveSpeed * Time.deltaTime);
    // }
    // else if (Input.GetKey(KeyCode.S))
    // {
    //   rigidBody.MovePosition(rigidBody.position + downDirection * moveSpeed * Time.deltaTime);
    // }

    // if (Input.GetKey(KeyCode.A))
    // {
    //   rigidBody.MovePosition(rigidBody.position + rightDirection * moveSpeed * Time.deltaTime);
    // }
    // else if (Input.GetKey(KeyCode.D))
    // {
    //   rigidBody.MovePosition(rigidBody.position + leftDirection * moveSpeed * Time.deltaTime);
    // }


    // if (Input.GetKeyDown(KeyCode.Space))
    // {
    //   Debug.Log("jump");
    //   float jump = 500f;
    //   Vector3 toCenter = planet.transform.position - transform.position;
    //   toCenter.Normalize();

    //   rigidBody.AddForce(-toCenter * jump, ForceMode.Acceleration);
    // }
  }

  void FixedUpdate()
  {
    rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(moveDir * moveSpeed * Time.deltaTime));
  }

  public GameObject compass;
  Vector2 Direction(Vector3 fromPosition)
  {
    float planetRadius = 5f;
    // Step 1: Calculate the normal vector of point A and the perpendicular vector
    Vector3 center = planet.transform.position;
    Vector3 normalA = (transform.position - center).normalized;
    Vector3 perpendicular = Vector3.Cross(Vector3.up, normalA);

    // Step 2: Calculate the angle between the positive Y-axis and the normal vector of point A
    float angle = Mathf.Acos(Vector3.Dot(Vector3.up, normalA));

    // Step 3: Rotate the positive Y-axis by the angle around the perpendicular vector
    Vector3 rotatedUp;
    if (normalA.y < 0)
    {
      // Handle the case when normalA is parallel to the negative Y-axis
      rotatedUp = -Vector3.up;
    }
    else
    {
      rotatedUp = Vector3.up * Mathf.Cos(angle) + Vector3.Cross(perpendicular, Vector3.up) * Mathf.Sin(angle) + perpendicular * Vector3.Dot(perpendicular, Vector3.up) * (1 - Mathf.Cos(angle));
    }

    // Step 4: Calculate the position vector of point B on the surface of the sphere
    Vector3 targetPosition = center + (fromPosition - center).normalized * planetRadius;

    // Step 5: Calculate the direction vector from point A to the target position
    Vector3 direction = (targetPosition - transform.position).normalized;

    // Step 6: Calculate the compass rotation to point to the target position
    Quaternion compassRotation = Quaternion.LookRotation(direction, rotatedUp);
    // Step 7: Set the rotation of the compass transform
    compass.transform.rotation = compassRotation;
    compass.transform.position = transform.position;

    Quaternion targetRotation = Quaternion.LookRotation(direction, rigidBody.transform.forward);

    // Step 8: Calculate the horizontal and vertical components of the direction vector
    Vector3 flatDirection = Vector3.ProjectOnPlane(direction, rotatedUp);
    Vector2 horizontalVertical = new Vector2(Vector3.Dot(flatDirection, Vector3.forward), Vector3.Dot(flatDirection, Vector3.right));

    return horizontalVertical;
  }

  void MovePlayer()
  {
    float planetRadius = 5f;

    // Get the position of the player and the planet
    Vector3 playerPos = transform.position;
    Vector3 planetPos = planet.transform.position;

    // Calculate the direction from the player to the north pole of the planet
    Vector3 northPole = planetPos + planet.transform.up * planetRadius + new Vector3(0, 0.5f, 0);
    Vector3 northDir = northPole - playerPos;
    northDir = Vector3.ProjectOnPlane(northDir, planet.transform.up).normalized;


    Vector3 pos = transform.position + northDir * 10f;
    //Debug.DrawLine(transform.position, pos, Color.magenta);

    // Calculate the compass rotation based on the north direction
    Quaternion compassRotation = Quaternion.LookRotation(northDir, planet.transform.up);
    compass.transform.rotation = compassRotation;
  }


}
