using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BckpPlayerController : MonoBehaviour
{
  public GameObject planet;
  public float moveSpeed = 5f;

  private Rigidbody rigidBody;
  private Rigidbody planetRigidBody;

  private Vector3 moveDir;
  private float rotSpeed = 50f;

  void Start()
  {
    rigidBody = GetComponent<Rigidbody>();
    planetRigidBody = planet.GetComponent<Rigidbody>();
  }

  void Update()
  {

    Gravity();
    moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

    if (Input.GetKeyDown(KeyCode.Space))
    {
      Debug.Log("jump");
      float jump = 500f;
      Vector3 toCenter = planet.transform.position - transform.position;
      toCenter.Normalize();

      rigidBody.AddForce(-toCenter * jump, ForceMode.Acceleration);
    }
  }

  void FixedUpdate()
  {
    rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
  }

  void Gravity()
  {
    float gravity = 10; //@TODO read from planet
    Vector3 toCenter = planet.transform.position - transform.position;
    toCenter.Normalize();

    rigidBody.AddForce(toCenter * gravity, ForceMode.Acceleration);

    Vector3 playerUp = transform.up;
    Quaternion q = Quaternion.FromToRotation(playerUp, -toCenter) * transform.rotation;
    transform.rotation = Quaternion.Slerp(transform.rotation, q, rotSpeed * Time.deltaTime);
  }
}
